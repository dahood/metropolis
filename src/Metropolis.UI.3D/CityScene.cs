using Urho;
using Urho.Gui;

namespace Metropolis.UI._3D
{
    public class CityScene : Application
    {
        public CityScene(ApplicationOptions options = null) : base(options)
        {
        }

        protected override void Start()
        {
            var cache = ResourceCache;
            var helloText = new Text
            {
                Value = "Hello World from UrhoSharp",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            helloText.SetColor(new Color(0f, 1f, 0f));
            helloText.SetFont(cache.GetFont("Fonts/Anonymous Pro.ttf"), 30);
            UI.Root.AddChild(helloText);
        }
    }
}