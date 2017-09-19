using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Metropolis.Api.Domain
{
    public static class InstanceBuilder
    {
        public static Instance Build(string physicalPath)
        {
            var filename = Path.GetFileName(physicalPath);
            var directory = Path.GetDirectoryName(physicalPath);
            return new Instance(new CodeBag(directory, CodeBagType.Directory, directory), filename, new Location(physicalPath));
        }


        public static Instance Build(CodeBag codeBag, string name, string location, int linesOfCode)
        {
            return new Instance(codeBag, name, new Location(location))
            {
                LinesOfCode = linesOfCode
            };
        }

        public static Instance Build(CodeBag codeBag, string name, string location, int linesOfCode, int complexity, int depthOfInheritance, int coupling, IEnumerable<Member> members)
        {
            return new Instance(codeBag, name, new Location(location))
            {
                LinesOfCode = linesOfCode,
                CyclomaticComplexity = complexity,
                DepthOfInheritance = depthOfInheritance,
                ClassCoupling = coupling,
                NumberOfMethods = members.Count(),
                Members = members.ToList()
            };
        }

        public static Instance Build(CodeBag codeBag, string name, Location location, IEnumerable<Member> members)
        {
            var type = new Instance(codeBag, name, location)
                {
                    NumberOfMethods = members.Count(),
                    Members = members.ToList()
                    };

            type.Members.ForEach(x =>
            {
                type.LinesOfCode += x.LinesOfCode;
                type.CyclomaticComplexity += x.CylomaticComplexity;
            });

            return type;
        }

    }
}