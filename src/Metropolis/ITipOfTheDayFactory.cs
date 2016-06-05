using Metropolis.TipOfTheDay;

namespace Metropolis
{
    public interface ITipOfTheDayFactory
    {
        ITipOfTheDay Next { get; }
    }
}
