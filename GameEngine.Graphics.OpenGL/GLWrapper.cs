using Silk.NET.Core.Contexts;
using Silk.NET.OpenGL;

namespace GameEngine.Graphics.OpenGL
{
    public class GLWrapper : IDisposable
    {
        private readonly GL gl;

        public GL Gl => gl;

        public GLWrapper(IGLContext context)
        {
            gl = GL.GetApi(context);
        }

        public void Dispose()
        {
            gl.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
