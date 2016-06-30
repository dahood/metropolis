using System.Collections.Generic;
using Metropolis.Api.Domain;

namespace Metropolis.Test.Fixtures
{
    public static class CodeGraphFixture
    {
        public static CodeBag MetroCodeBagApi => new CodeBag("Metropolis.Api.Domain", CodeBagType.Namespace, @"C:\dev\Metropolis.Api\Domain");
        public static CodeBag MetroCodeBag => new CodeBag("Metropolis.Views", CodeBagType.Namespace, @"C:\dev\Metropolis\Views");

        public static CodeGraph Metropolis
        {
            get
            {
                var listOfClasses = new List<Instance>();
                listOfClasses.AddRange(new List<Instance> {CodeBase});
                listOfClasses.AddRange(new List<Instance> {Canvas});

                var codeGraph = new CodeGraph(listOfClasses);

                return codeGraph;
            }
        }

        public static Instance CodeBase
        {
            get
            {
                var classOne = InstanceBuilder.Build(MetroCodeBagApi, "CodeBase", @"CC:\dev\Metropolis.Api\Domain\CodeBase.cs", 5, 200, 6,10, new List<Member> {});

                classOne.AddMeta(new InstanceVersionInfo("CodeBase.cs", "commit message 1"));

                return classOne;
            }
        }

        public static Instance Canvas
        {
            get
            {
                var classOne = InstanceBuilder.Build(MetroCodeBag, "Canvas", @"CC:\dev\Metropolis\Views\Canvas.xaml.cs", 5, 200, 6, 10, new List<Member> { });

                var members = new[]
                {
                    new Member("Foo()", 31, 0, 0)
                };
                classOne.AddMembers(members);

                return classOne;
            }
        }
    }
}