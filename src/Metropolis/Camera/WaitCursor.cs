using System;
using System.Windows.Input;

namespace Metropolis.Camera
{
    public class WaitCursor : IDisposable
    {
        private readonly Cursor previousCursor;
        private bool disposed;

        public WaitCursor()
        {
            previousCursor = Mouse.OverrideCursor;

            Mouse.OverrideCursor = Cursors.Wait;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                Mouse.OverrideCursor = previousCursor;
            }
            disposed = true;
        }
    }
}