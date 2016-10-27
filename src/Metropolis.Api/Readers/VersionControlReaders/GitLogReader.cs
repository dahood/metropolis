using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Metropolis.Api.Domain;
using Metropolis.Common.Extensions;

namespace Metropolis.Api.Readers.VersionControlReaders
{
    /// <summary>
    ///     Uses output from:
    ///       git log --all --numstat --date=short --pretty=format:'--%h--%ad--%aN' --no-renames > gitlog.log
    /// </summary>
    public class GitLogReader : ISourceControlReader
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

        public CodeBase Parse(TextReader textReader, CodeBase codeBase)
        {
            codeBase.Commits = ParseCommits(textReader);

            // add these to CodeGraph.Commits.Add(range from gitlog.log)

            // step 2 - foreach CommitEntry apply the resulting Instance & Artifacts associate hash reference
            // add for Instances.VersionHistory.Add()
            // add for Artifacts.VersionHistory.Add()

            return codeBase;
        }

        private static List<CommitEntry> ParseCommits(TextReader textReader)
        {
            List<CommitEntry> entries = new List<CommitEntry>();
            string line;

            while ((line = textReader.ReadLine()) != null)
            {
                if (!line.StartsWith("--")) continue;

                var entry = new CommitEntry();
                entry.CommitHash = line.Substring(2, 7);
                entry.CommitTime = new DateTime(
                    int.Parse(line.Substring(11, 4)), // year
                    int.Parse(line.Substring(16, 2)), // month
                    int.Parse(line.Substring(19, 2))); // day
                entry.AuthorName = line.Substring(23);

                ParseCommitDetails(textReader, entry);

                entries.Add(entry);
            }
            return entries;
        }

        private static void ParseCommitDetails(TextReader textReader, CommitEntry entry)
        {
            string line;
            while ((line = textReader.ReadLine()) != null)
            {
                if (line == string.Empty) break;
                var commitDetail = new CommitDetail();
                var split = line.Split(Convert.ToChar(9));
                if (line.StartsWith("-"))
                    commitDetail.IsBinary = true;
                else
                {
                    commitDetail.AddedLines = int.Parse(split[0]);
                    commitDetail.DeletedLines = int.Parse(split[1]);
                }
                commitDetail.Path = new Location(split[2]);

                entry.AdditionsAndDeletions.Add(commitDetail);
            }
        }
    }
}