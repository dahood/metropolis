// Gulp Modules
var del = require('del');
var gulp = require('gulp');
var file = require('gulp-file');
var assemblyInfo = require('gulp-dotnet-assembly-info');
var nunit = require('gulp-nunit-runner');
var msbuild = require("gulp-msbuild");
var child_process = require('child_process');
var Nuget = require('nuget-runner');
var nugetRestore = require('gulp-nuget-restore');
var mkdirp = require('mkdirp');
var xmlTransformer = require("gulp-xml-transformer");
var zip = require('gulp-zip');
var globby = require('globby');

// Project variables
var projectName = 'Shaw.Digital.Skeleton';
var configuration = 'Release';
var assemblyVersion = "1.0.x";
var buildDir = __dirname + '/build/';
var buildDistDir = __dirname + '/build/dist';
var distDir = __dirname + '/dist/';
var buildexe = buildDir + '\\'+projectName+'.exe';
var dotCoverTemplate = 'coverage.xml';
 
// Gulp Variables
var nugetPath = globby.sync('node_modules\\gulp-nuget-restore\\nuget.exe')[0];
var nuget = Nuget({
    nugetPath: nugetPath,
    configFile: 'Nuget.config'
});
	
// Gulp Tasks

gulp.task('version', function(){
    return new Promise(doVersion);
	
	function doVersion(resolve) {
		var assemblyBuildNumber = process.env.BUILD_NUMBER ? process.env.BUILD_NUMBER : '0';
		assemblyVersion = assemblyVersion.replace('x', assemblyBuildNumber);
		resolve();
	}	
});

gulp.task('clean', function(){
  return new Promise((resolve) => {
    del([buildDir, distDir], {force: true}).then(() => {
      mkdirp(buildDistDir, () => mkdirp(distDir, resolve));
    });
  });
});


gulp.task('restore-nuget', function(){
  return gulp.src('*.sln')
    .pipe(nugetRestore({ additionalArgs: ['-ConfigFile', 'Nuget.Config']}));
});

gulp.task('assemblyInfo', function() {
    return gulp.src('**/AssemblyInfo.cs')
        .pipe(assemblyInfo({
            title: projectName,
            description: projectName, 
            configuration: 'Release', 
            company: 'Shaw Communications Inc.', 
            product: 'Skeleton Display', 
            copyright: 'Copyright © Shaw Communications Inc.', 
            trademark: 'Shaw', 
            version: assemblyVersion + '.0',
            fileVersion: assemblyVersion}))
        .pipe(gulp.dest('.'));
});

gulp.task('compile-solution', () => {
  return gulp.src('./'+projectName+'.sln')
    .pipe(msbuild({
      toolsVersion : 'auto',
      stdout : true,
      stderr : true,
      maxcpucount : 4,
      nologo : true,
      errorOnFail: true,
      configuration: configuration
    }))
});

gulp.task('copy-to-build', () => {
	return gulp.src(['./src/' + projectName + '.Tests/bin/' + configuration + '/**/*'])
		.pipe(gulp.dest(buildDir));
});

gulp.task('copy-to-dist', function(){
	console.log('copying to build/dist');
	file(projectName + '.nuspec', getNuspecContents())
		.pipe(gulp.dest(buildDistDir));
	gulp.src(['deploy.ps1']).pipe(gulp.dest(buildDistDir));	
	return gulp.src([`./src/${projectName}/bin/${configuration}/**/*`])
		.pipe(gulp.dest(buildDistDir + '/lib/net462'));
});

gulp.task('nunit-test', function () {
  return new Promise((resolve, error) => {
    gulp.src(['build/**/**.*Tests.dll'], {read: false})
      .pipe(nunit({
        executable: getNUnitConsole()
      })).on('error', (msg) => {
        error(msg);
      }).on('end', () => {
        resolve();
      });
  });
});

gulp.task('update-coverage-params', function () {
    return gulp.src(__dirname + '/' + dotCoverTemplate)
        .pipe(xmlTransformer([
            { path: '//AnalyzeParams//TargetExecutable', text: __dirname + '/' + getNUnitConsole() },
            { path: '//AnalyzeParams//TargetArguments', text: buildDir + projectName + '.Tests.dll' },
            { path: '//AnalyzeParams//TargetWorkingDir', text: buildDir },
            { path: '//AnalyzeParams//TempDir', text: buildDir },
            { path: '//AnalyzeParams//Output', text: buildDir + 'cc/codecoverage.html' },
            { path: '//AnalyzeParams//ReportType', text: 'HTML' },
        ]))
        .pipe(gulp.dest(buildDir));
});

gulp.task('exec-coverage', function () {
	return new Promise(doCoverage);
	
   function doCoverage(resolve) {
		console.log('Coverage ' + getDotCover() + ' ' + 'analyze '  + buildDir +  dotCoverTemplate);
		var child = child_process.spawn(getDotCover(),[ 'analyze' , buildDir +  dotCoverTemplate], {stdio:'inherit'} );
		
		child.on('close', function() {
			resolve();
		});
	}
});

gulp.task('zip-coverage', function () {
    return gulp.src('build/cc/**')
        .pipe(zip(assemblyVersion + '-prereleasecodecoverage.zip'))
        .pipe(gulp.dest('dist'))
});


gulp.task('package-nuget', function(){
  console.log('Version: ' + assemblyVersion + '-prerelease')
  var nuget = Nuget({
    nugetPath: nugetPath
  });

  return nuget.pack({
      spec: `${buildDistDir}\\${projectName}.nuspec`,
      version: `${assemblyVersion}-prerelease`, 
	  includeReferencedProjects: true,
      outputDirectory : `${distDir}nuget`,
	  properties: { Configuration: configuration }
    });
});

gulp.task('start-service', () => {
	return new Promise(doSpawn)
	
	function doSpawn(resolve) {
		var spawn = child_process.spawn(buildexe);
		spawn.on('close', function() {
			resolve();
		});
	}
});

gulp.task('default', gulp.series('nunit-test'));

gulp.task('ci', gulp.series('version', 'clean', 'restore-nuget', 'assemblyInfo', 'compile-solution', gulp.parallel('copy-to-build', 'copy-to-dist'), 'nunit-test', 'update-coverage-params', 'exec-coverage', 'zip-coverage', 'package-nuget'));

gulp.task('run', gulp.series('version', 'clean', 'restore-nuget', 'assemblyInfo', 'compile-solution', gulp.parallel('copy-to-build', 'copy-to-dist'), 'start-service'));

function getNUnitConsole(){
  return globby.sync('packages\\NUnit.ConsoleRunner.*\\tools\\nunit3-console.exe')[0];
}

function getDotCover() {
	return globby.sync('packages\\JetBrains.dotCover.CommandLineTools.*\\tools\\dotCover.exe')[0];
}

function getNuspecContents() {
    return `<?xml version="1.0"?>
<package >
  <metadata>
    <id>${projectName}</id>
    <version>${assemblyVersion}</version>
    <authors>Shaw Communications Inc.</authors>
    <owners>Shaw Communications Inc.</owners>
    <projectUrl>https://bitbucket.sjrb.ad:7443/projects/DSDS/repos/Shaw.Digital.Skeleton/browse</projectUrl>
    <requireLicenseAcceptance>false</requireLicenseAcceptance>
    <description>Shaw's Digital Service Library Framework</description>
    <copyright>Copyright © Shaw Communications Inc.</copyright>
  </metadata>
</package>`;
}