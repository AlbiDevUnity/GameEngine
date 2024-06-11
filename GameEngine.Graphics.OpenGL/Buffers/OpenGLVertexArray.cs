using Silk.NET.OpenGL;

namespace GameEngine.Graphics.OpenGL.Buffers
{
    public class OpenGLVertexArray : IDisposable
    {
        private readonly GL gl;
        private readonly uint m_Handle;

        public OpenGLVertexArray(GL gL)
        {
            this.gl = gL;
            m_Handle = gl.GenVertexArray();
        }

        public OpenGLVertexArray(GLWrapper wrapper) : this(wrapper.Gl) { }

        public void Bind()
        {
            gl.BindVertexArray(m_Handle);
        }

        public void Unbind()
        {
            gl.BindVertexArray(0);
        }

        public void Dispose()
        {
            gl.DeleteVertexArray(m_Handle);
            GC.SuppressFinalize(this);
        }
    }
}
