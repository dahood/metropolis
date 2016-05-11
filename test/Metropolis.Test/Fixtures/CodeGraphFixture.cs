using System.Collections.Generic;
using Metropolis.Api.Core.Domain;

namespace Metropolis.Test.Fixtures
{
    public static class CodeGraphFixture
    {
        public static CodeGraph Metropolis
        {
            get
            {
                var listOfClasses = new List<Instance>();
                listOfClasses.AddRange(new List<Instance> { CodeBase });
                listOfClasses.AddRange(new List<Instance> { Canvas });

                var codeGraph = new CodeGraph(listOfClasses);

                return codeGraph;
            }
        }
        public static Instance CodeBase
        {
            get
            {
                var classOne = new Instance("Metropolis.Domain", "CodeBase", 5, 200, 6);

                classOne.AddMeta(new InstanceVersionInfo("CodeBase.cs", "commit message 1"));

                return classOne;
            }
        }

        public static Instance Canvas
        {
            get
            {
                var classOne = new Instance("Metropolis", "Canvas", 5, 300, 10);

                var members = new[]
                {
                    new Member("Foo()", 31,0,0)
                };
                classOne.AddMember(members);

                classOne.AddMeta(new InstanceVersionInfo("Canvas.cs", "commit message 2"));

                return classOne;
            }
        }
    }
}