using System.Windows.Input;

namespace Metropolis.Camera
{
    internal class NoOperation : IMouseOperation
    {
        public void Execute(MouseEventArgs mouseEventArgs)
        {
            // I do nothing 'cause it is my job
        }

        public void PreExecute(MouseEventArgs mouseEventArgs)
        {
        }

        public void PostExecute(MouseEventArgs mouseEventArgs)
        {
        }

        public void Reset()
        {
        }
    }
}