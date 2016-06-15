using System.Collections.Generic;
﻿using System.IO;
using Metropolis.Api.Domain;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services
{
    public interface ICodebaseService
    {
        void Save(CodeBase workspace, string fileName);
        CodeBase Load(string projectFileName);
        CodeBase LoadDefault();
        ProjectBuildResult BuildSolution(ProjectBuildArguments buildArgs);
        string ProjectBuildFolder { get; }
        void WriteIgnoreFile(string projectName, string projectFolder, IEnumerable<FileDto> filesToIgnore);
        CodeBase Get(TextReader stream, ParseType parseType);
        CodeBase GetToxicity(TextReader openFileStream);
        CodeBase GetVisualStudioMetrics(TextReader openFileStream);
    }
}
