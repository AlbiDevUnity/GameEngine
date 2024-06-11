using Silk.NET.OpenGL;

namespace GameEngine.Graphics.OpenGL.Buffers
{
    public class OpenGLVertexBuffer : IDisposable
    {
        private readonly GL gl;
        private readonly uint m_Handle;

        public OpenGLVertexBuffer(GL gl)
        {
            this.gl = gl;
            m_Handle = gl.GenBuffer();
        }

        public OpenGLVertexBuffer(GLWrapper wrapper) : this(wrapper.Gl) { }

        public void Bind()
        {
            gl.BindBuffer(GLEnum.ArrayBuffer, m_Handle);
        }

        public void Unbind()
        {
            gl.BindBuffer(GLEnum.ArrayBuffer, 0);
        }

        public unsafe void SetData<TData>(TData[] data) where TData : unmanaged
        {
            fixed (void* ptr = &data[0])
            {
                gl.BufferData(GLEnum.ArrayBuffer, (uint)(data.Length * sizeof(TData)), ptr, GLEnum.StaticDraw);
            }
        }

        public void Dispose()
        {
            gl.DeleteBuffer(m_Handle);
            GC.SuppressFinalize(this);
        }
    }
}
