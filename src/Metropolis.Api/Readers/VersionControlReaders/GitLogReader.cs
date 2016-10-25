using System;
using System.IO;
using Metropolis.Api.Domain;

namespace Metropolis.Api.Readers.VersionControlReaders
{
    /// <summary>
    ///     Uses output from:
    ///       git log --all --numstat --date=short --pretty=format:'--%h--%ad--%aN' --no-renames > gitlog.log
    /// </summary>
    public class GitLogReader : IInstanceReader
    {
        /*
         *  --commitHash--date--author
         *  each change in an automic commit
         *  additions     deleitions       filePath
         *  2             1                src/Metropolis.UI.MVVM.Core/App.cs
         *  8	          0	               src/Metropolis.UI.MVVM.Core/ViewModels/MainViewModel.cs
         *  
         *  end with '\n' in unix or '\r\n' in windows
         *  
            --12ad430--2016-09-30--Jonathan McCracken
            2	1	src/Metropolis.UI.MVVM.Core/App.cs
            8	0	src/Metropolis.UI.MVVM.Core/ViewModels/MainViewModel.cs
            4	3	src/Metropolis.UI.MVVM.WPF/Metropolis.UI.MVVM.WPF.csproj
            0	16	src/Metropolis.UI.MVVM.WPF/Views/FirstView.xaml
            0	12	src/Metropolis.UI.MVVM.WPF/Views/FirstView.xaml.cs
            133	0	src/Metropolis.UI.MVVM.WPF/Views/MainView.xaml
            12	0	src/Metropolis.UI.MVVM.WPF/Views/MainView.xaml.cs 
        */

        public CodeBase Parse(TextReader textReader)
        {
            throw new NotImplementedException();

            // step 1 - parse into CommitEntries
            // add these to CodeGraph.Commits.Add(range from gitlog.log)


            // step 2 - from the list of all instances & files in git history
            // make a list of instances
            // make a list of artifacts


            // step 3 - associate a reference for each occurance in CodeGraph.Commits to CodeGraph.Instances & CodeGraph.Artifacts
            // add for Instances
            // add for Artifacts


            // don't return an empty codebase
            return CodeBase.Empty();
        }
    }
}