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
                var listOfClasses = new List<Class>();
                listOfClasses.AddRange(new List<Class> { CodeBase });
                listOfClasses.AddRange(new List<Class> { Canvas });

                var codeGraph = new CodeGraph(listOfClasses);

                return codeGraph;
            }
        }
        public static Class CodeBase
        {
            get
            {
                var classOne = new Class("Metropolis.Domain", "CodeBase", 5, 200, 6);

                classOne.AddMeta(new ClassVersionInfo("CodeBase.cs", "commit message 1"));

                return classOne;
            }
        }

        public static Class Canvas
        {
            get
            {
                var classOne = new Class("Metropolis", "Canvas", 5, 300, 10);

                var members = new[]
                {
                    new Member("Foo()", 31,0,0)
                };
                classOne.AddMember(members);

                classOne.AddMeta(new ClassVersionInfo("Canvas.cs", "commit message 2"));

                return classOne;
            }
        }
    }
}