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

            //parse once for instances
            // instances have Touches or commit history - see InsanceVersionInfo
            //var oneCheckin = new VersionHistory("foo", "bar is so bar");

            //parse twice for artifacts
            //var anotherCheckin = new VersionHistory("foo", "bar is so bar");


            // don't return an empty codebase
            return CodeBase.Empty();
        }
    }
}