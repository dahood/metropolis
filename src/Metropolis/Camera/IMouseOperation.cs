using System.Windows.Input;

namespace Metropolis.Camera
{
    internal interface IMouseOperation
    {
        void Execute(MouseEventArgs mouseEventArgs);
        void PreExecute(MouseEventArgs mouseEventArgs);
        void PostExecute(MouseEventArgs mouseEventArgs);
        void Reset  ();
    }
}