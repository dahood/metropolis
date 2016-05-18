using System.Collections.Generic;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services.Tasks.Commands
{
    public class EcmaMetricsCommand : CompositeMetricsCommand
    {
        public EcmaMetricsCommand() : base(new List<IMetricsCommand> {new EsLintMetricsCommand(), new SlocMetricsCommand(ParseType.SlocEcma) }, true)
        {
        }
    }
}