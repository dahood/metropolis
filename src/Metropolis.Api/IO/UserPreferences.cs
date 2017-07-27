using System;
using System.IO;
using System.Linq;
using Metropolis.Api.Properties;

namespace Metropolis.Api.IO
{
    public class UserPreferences : IUserPreferences
    {
        string _msBuildPath;

        public bool ShowTipOfTheDay
        {
            get { return (bool)Settings.Default["ShowTips"]; }
            set
            {
                Settings.Default["ShowTips"] = value;
                Settings.Default.Save();
            }
        }

        public string FxCopPath
        {
            get { return (string)Settings.Default["FxCopPath"]; }
            set
            {
                Settings.Default["FxCopPath"] = value;
                Settings.Default.Save();
            }
        }

        public string MsBuildPath
        {
            get
            {
                if (_msBuildPath == null)
                    InitMsBuildPath();

                return _msBuildPath ?? Settings.Default.MSBuildPathFallback;
            }

            set { _msBuildPath = value; }
        }

        void InitMsBuildPath()
        {
            var paths = Environment.GetEnvironmentVariable("PATH")?.Split(';');
            var msbuildExe = "msbuild.exe";

            var msBuildDirectory = paths?.FirstOrDefault(path => FileExists(path, msbuildExe));
            _msBuildPath = Path.Combine(msBuildDirectory, msbuildExe);
        }

        bool FileExists(string path, string file) => File.Exists(Path.Combine(path.Trim(), file));
    }
}
