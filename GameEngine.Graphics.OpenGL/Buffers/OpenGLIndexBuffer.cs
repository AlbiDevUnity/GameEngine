using Silk.NET.OpenGL;

namespace GameEngine.Graphics.OpenGL.Buffers
{
    public class OpenGLIndexBuffer : IDisposable
    {
        private readonly GL gl;
        private readonly uint m_Handle;

        public OpenGLIndexBuffer(GL gl)
        {
            this.gl = gl;
            m_Handle = gl.GenBuffer();
        }

        public OpenGLIndexBuffer(GLWrapper wrapper) : this(wrapper.Gl) { }

        public void Bind()
        {
            gl.BindBuffer(GLEnum.ElementArrayBuffer, m_Handle);
        }

        public void Unbind()
        {
            gl.BindBuffer(GLEnum.ArrayBuffer, 0);
        }

        public unsafe void SetData(uint[] data)
        {
            fixed (void* ptr = &data[0])
            {
                gl.BufferData(GLEnum.ElementArrayBuffer, (uint)(data.Length * sizeof(uint)), ptr, GLEnum.StaticDraw);
            }
        }

        public void Dispose()
        {
            gl.DeleteBuffer(m_Handle);
            GC.SuppressFinalize(this);
        }
    }
}
