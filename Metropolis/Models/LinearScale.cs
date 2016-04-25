namespace Metropolis.Models
{
    public static class LinearScale
    {
        private const int Min = 1;
        private const int Max = 100;

        public static int Apply(int toScale, int scaleMin, int scaleMax)
        {
            return (Max - Min) * (toScale - scaleMin) / (scaleMax - scaleMin) + Min;
        }
    }
}