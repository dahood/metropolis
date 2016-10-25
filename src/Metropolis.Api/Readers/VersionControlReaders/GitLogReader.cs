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
         *  additions     deletions        filePath
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

            // step 2 - foreach CommitEntry apply the resulting Instance & Artifacts
            // TODO: THIS CAN BE COMBINED WITH STEP 3...so we don't need to iterate as many times

            // create either an instance or an artifact based on file type - TODO: Where will this come from...all I have is a Reader...
            // apply an instance with FileHeadRevisionStatus.Unsure
            // apply an artifact with FileHeadRevisionStatus.Unsure

            // step 3 - associate a reference for each occurance in CodeGraph.Commits to CodeGraph.Instances & CodeGraph.Artifacts
            // becareful not to serialize this out twice since this could get huge
            // add for Instances.VersionHistory.Add()
            // add for Artifacts.VersionHistory.Add()

            // step 4 - this goes inside AbstractFile.Apply? but writting it here anyway...
            // NEED TO COMPARE/APPLY based on what Exists and what has been deleted...
            // if Foo.cs existed from a prior parser then we merge the history into the existing AbtractFile
            // if Bar.cs doesn't show up then we...a) toss it for now....or b) ... do something smart ...

            // return new CodeGraph/CodeBase();
        }
    }
}