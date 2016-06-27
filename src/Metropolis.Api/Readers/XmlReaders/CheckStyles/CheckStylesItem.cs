namespace Metropolis.Api.Readers.XmlReaders.CheckStyles
{
    public class CheckStylesItem
    {
        public string Name { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
    }
}