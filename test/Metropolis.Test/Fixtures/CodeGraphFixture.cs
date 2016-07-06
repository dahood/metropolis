using System.Collections.Generic;
using Metropolis.Api.Domain;

namespace Metropolis.Test.Fixtures
{
    public static class CodeGraphFixture
    {
        public static CodeBag MetroCodeBagApi => new CodeBag("Metropolis.Api.Domain", CodeBagType.Namespace, @"C:\dev\Metropolis.Api\Domain");
        public static CodeBag MetroCodeBag => new CodeBag("Metropolis.Views", CodeBagType.Namespace, @"C:\dev\Metropolis\Views");

        public static CodeBase Metropolis => new CodeBase(MetropolisGraph);

        public static CodeGraph MetropolisGraph
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
                var path = @"C:\dev\Metropolis.Api\Domain\CodeBase.cs";
                var classOne = InstanceBuilder.Build(MetroCodeBagApi, "CodeBase", path, 50, 200, 10,10, new List<Member> {});

                classOne.AddMeta(new InstanceVersionInfo("CodeBase.cs", "commit message 1"));

                classOne.Duplicates.Add(new Duplicate(1, 10, new Location(path)));
                classOne.Duplicates.Add(new Duplicate(1, 10, new Location("fileB.cs")));

                return classOne;
            }
        }

        public static Instance Canvas
        {
            get
            {
                var path = @"C:\dev\Metropolis\Views\Canvas.xaml.cs";
                var classOne = InstanceBuilder.Build(MetroCodeBag, "Canvas", path, 50, 200, 6, 10, new List<Member> { });

                var members = new[]
                {
                    new Member("Foo()", 31, 0, 0)
                };
                classOne.AddMembers(members);

                classOne.Duplicates.Add(new Duplicate(1, 10, new Location(path)));
                classOne.Duplicates.Add(new Duplicate(1, 10, new Location("fileB.cs")));

                return classOne;
            }
        }
    }
}