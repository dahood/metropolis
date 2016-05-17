using System.Collections.Generic;

namespace Metropolis.Api.Services.Tasks.Commands
{
    public class EcmaMetricsCommand : CompositeMetricsCommand
    {
        public EcmaMetricsCommand() : base(new List<IMetricsCommand> {new EsLintMetricsCommand(), new SlocMetricsCommand()}, true)
        {
        }
    }
}