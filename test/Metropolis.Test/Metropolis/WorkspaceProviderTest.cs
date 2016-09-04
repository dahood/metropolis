using System.IO;
using FluentAssertions;
using Metropolis.Api.Domain;
using Metropolis.Api.IO;
using Metropolis.Api.IO.AutoSave;
using Metropolis.Api.Services;
using Metropolis.Common.Models;
using Metropolis.Test.Api.Services;
using Metropolis.Test.Extensions;
using Metropolis.Test.Utilities;
using Metropolis.ViewModels;
using Moq;
using NUnit.Framework;

namespace Metropolis.Test.Metropolis
{
    [TestFixture]
    public class WorkspaceProviderTest : StrictMockBaseTest
    {
        private WorkspaceProvider provider;
        private Mock<IAnalysisService> analysisService;
        private Mock<ICodebaseService> codebaseService;
        private Mock<IUserPreferences> userPreferences;
        private Mock<IAutoSaveService> autoSaveService;
        private Mock<IFileSystem> fileSystem;

        private const string ProjectName = "NHibernate";
        private const string ProjectFolder = @"c:\NHibernate";
        private readonly FileInfo autosaveOne = new FileInfo(@"c:\autosave1.project");
        private readonly FileInfo autosaveTwo = new FileInfo(@"c:\autosave2.project");

        [SetUp]
        public void SetUp()
        {
            App.ViewModel = new ProjectDetailsViewModel();
            codebaseService = CreateMock<ICodebaseService>();
            analysisService = CreateMock<IAnalysisService>();
            userPreferences = CreateMock<IUserPreferences>();
            fileSystem = CreateMock<IFileSystem>();
            autoSaveService = CreateMock<IAutoSaveService>();

            fileSystem.Setup(x => x.CreateMetropolisSpecialFolders());

            provider = new WorkspaceProvider(codebaseService.Object, analysisService.Object, userPreferences.Object, fileSystem.Object, autoSaveService.Object);
        }

        private void ExpectAutoLoad()
        {
            fileSystem.Setup(x => x.GetAutloadProjects()).Returns(new[] {autosaveOne, autosaveTwo});
        }

        [Test]
        public void Create()
        {
            provider.Create();
            provider.CodeBase.ReflectionEquals(CodeBase.Empty(), true);
        }

        [Test]
        public void Load()
        {
            const string fileName = @"c:\Hibernate.project";
            codebaseService.Setup(x => x.Load(fileName)).Returns(CodeBase.Empty);
            provider.Load(fileName);
        }

        [Test]
        public void Analyze()
        {
            var viewModel = new ProjectDetailsViewModel {RepositorySourceType = RepositorySourceType.ECMA, ProjectName = "React"};

            analysisService.Setup(x => x.Analyze(It.Is<MetricsCommandArguments>(a => ComparesTo(viewModel, a)))).Returns(CodeBase.Empty);
            provider.Analyze(viewModel);
        }
        private static bool ComparesTo(ProjectDetailsViewModel viewModel, MetricsCommandArguments actual)
        {
            Validate.Begin()
                    .IsNotNull(viewModel, "viewModel")
                    .IsNotNull(actual, "actual")
                    .IsEqual(viewModel.RepositorySourceType, actual.RepositorySourceType, "SourceType")
                    .IsEqual(viewModel.ProjectName, actual.ProjectName, "projectName")
                    .Check();
            return true;
        }

        [Test]
        public void ShowTips_Get()
        {
            userPreferences.SetupGet(x => x.ShowTipOfTheDay).Returns(true);
            provider.ShowTips.Should().BeTrue();
        }

        [Test]
        public void ShowTips_Set()
        {
            userPreferences.SetupSet(x => x.ShowTipOfTheDay = false);
            provider.ShowTips = false;
        }

        [Test]
        public void ScreenShotFolder()
        {
            const string screenshots = @"c:\screenshots\";
            fileSystem.Setup(x => x.ScreenShotFolder).Returns(screenshots);
            provider.ScreenShotFolder.Should().Be(screenshots);
        }

        [Test]
        public void DeriveProjectFolder()
        {
            provider.DeriveProjectFolder(@"c:\windows\myclass.cs").Should().Be(@"c:\windows");
            provider.DeriveProjectFolder(string.Empty, @"c:\Program Files").Should().Be(@"c:\Program Files"); //use default if empty
        }

        [Test]
        public void AutoloadLastProject_LoadedFirstFile()
        {
            var codeBase = GetCodeBase("MyProject");
            ExpectAutoLoad();
            codebaseService.Setup(x => x.Load(autosaveOne.FullName)).Returns(codeBase);
            fileSystem.Setup(x => x.GetProjectBuildFolder(codeBase.Name)).Returns($"c:\\MetropolisSandbox\\build\\{codeBase.Name}");
            provider.AutoloadLastProject().Should().BeTrue();
        }

        [Test]
        public void AutoloadLastProject_LoadNextFileWhenFirstFails()
        {
            var codeBase = GetCodeBase("MyProject");
            ExpectAutoLoad();
            codebaseService.Setup(x => x.Load(autosaveOne.FullName)).Throws<IOException>();
            codebaseService.Setup(x => x.Load(autosaveTwo.FullName)).Returns(codeBase);
            fileSystem.Setup(x => x.GetProjectBuildFolder(codeBase.Name)).Returns($"c:\\MetropolisSandbox\\build\\{codeBase.Name}");
            provider.AutoloadLastProject().Should().BeTrue();
        }

        private CodeBase GetCodeBase(string projectName)
        {
            var codeBase = CodeBase.Empty();
            codeBase.Name = "MyProject";
            return codeBase;
        }

        [Test]
        public void AutoloadLastProject_LoadDefaultProjectWhenAllOtherAttemptsFail()
        {
            ExpectAutoLoad();
            codebaseService.Setup(x => x.Load(autosaveOne.FullName)).Throws<IOException>();
            codebaseService.Setup(x => x.Load(autosaveTwo.FullName)).Throws<FileNotFoundException>();
            codebaseService.Setup(x => x.LoadDefault()).Returns(CodeBase.Empty);
            provider.AutoloadLastProject().Should().BeTrue();
        }

        [Test]
        public void BuildSolution()
        {
            var args = new ProjectBuildArguments("d","d",RepositorySourceType.CSharp, "cd");
            var result = new ProjectBuildResult();
            codebaseService.Setup(x => x.BuildSolution(args)).Returns(result);
            provider.BuildSolution(args);
        }

        [Test]
        public void DeriveProjectName()
        {
            const string path = @"c:\mysolution.sln";
            provider.DeriveProjectName(path).Should().Be("mysolution");
        }

        [Test]
        public void CreateIgnoreFile()
        {
            var ignoreFile = new FileDto {Name = "igmore.me"};
            codebaseService.Setup(x => x.WriteIgnoreFile(ProjectFolder, new[] {ignoreFile}));
            provider.CreateIgnoreFile(new ProjectDetailsViewModel {ProjectName = ProjectName, ProjectFolder = ProjectFolder, FilesToIgnore = new [] {ignoreFile}});
        }

        [Test]
        public void SetUpDotNetBuild()
        {
            var ignoreFile = new FileDto { Name = "igmore.me" };
            var solutionFile = $"{ProjectFolder}\\{ProjectName}.sln";
            var buildPath = @"c:\ProjectBuildPath\build";
            var ignorePath = $"{ProjectFolder}\\.metroignore";
            var projectDetails = new ProjectDetailsViewModel {ProjectFolder = ProjectFolder, ProjectName = ProjectName, IgnoreFile = ignorePath};

           
            fileSystem.Setup(x => x.GetProjectBuildFolder(projectDetails.ProjectName)).Returns(buildPath);
            fileSystem.Setup(x => x.GetProjectIgnoreFile(projectDetails.ProjectFolder)).Returns(ignorePath);
            codebaseService.Setup(x => x.GetIgnoreFilesForProject(projectDetails.IgnoreFile)).Returns(new [] { ignoreFile});

            provider.SetUpDotNetBuild(projectDetails, solutionFile);

            projectDetails.ProjectName.Should().Be(ProjectName);
            projectDetails.BuildOutputDirectory.Should().Be(buildPath);
            projectDetails.SourceDirectory.Should().Be(ProjectFolder);
            projectDetails.IgnoreFile.Should().Be(ignorePath);
        }

        [Test]
        public void GetFileContents()
        {
            var sourceFile = @"c:\nhibernate\startup.cs";
            codebaseService.Setup(x => x.GetFileContents(sourceFile)).Returns(new FileContentsResult {Data = "hi there"});
            var result = provider.GetFileContents(sourceFile);
            result.Should().NotBeNull();
            result.Data.Should().Be("hi there");
        }

        [Test]
        public void AutoSaveProject()
        {
            var details = new ProjectDetailsViewModel();
            autoSaveService.Setup(x => x.Save(It.IsNotNull<CodeBase>()));
            provider.AutoSaveProject(details);
        }
    }
}
