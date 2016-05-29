using System;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace Metropolis.Camera
{
    public class MouseMovement
    {
        private readonly ISceneProvider provider;
        private IMouseOperation operation;

        private readonly IMouseOperation noOperation;
        private readonly IMouseOperation translationalOperation;
        private readonly IMouseOperation rotationOperation;

        public MouseMovement(ISceneProvider provider)
        {
            this.provider = provider;
            provider.Model.Transform = new Transform3DGroup();
            
            noOperation = new NoOperation();
            translationalOperation = new TranslateOperation(provider);
            rotationOperation = new RotationalOperation(provider);
            operation = noOperation;

            ListenForEvents();
        }

        private void ListenForEvents()
        {
            provider.MouseLeftButtonDown += SelectOperation;
            provider.MouseLeftButtonUp += (o,e) => ChangeOperation(noOperation, e);
            provider.MouseMove += HandleMouseMove;
        }

        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            operation.Execute(e);
        }

        private void ChangeOperation(IMouseOperation next, MouseEventArgs e)
        {
            operation.PostExecute(e);
            next.PreExecute(e);
            operation = next;
        }

        private void SelectOperation(object sender, MouseEventArgs e)
        {
            var next = Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt) ? rotationOperation : translationalOperation;
            ChangeOperation(next, e);
        }

        public void Reset()
        {
            rotationOperation.Reset();
            translationalOperation.Reset();
        }
    }
}
