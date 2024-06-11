using Silk.NET.OpenGL;

namespace GameEngine.Graphics.OpenGL
{
    public class OpenGLShaderProgram : IDisposable
    {
        private readonly GL gl;

        private readonly uint m_Handle;

        public uint Handle => m_Handle;

        public OpenGLShaderProgram(GL gl, params OpenGLShader[] shaders)
        {
            this.gl = gl;

            m_Handle = gl.CreateProgram();

            foreach (OpenGLShader shader in shaders)
            {
                AttachShader(shader);
            }
        }

        public OpenGLShaderProgram(GLWrapper wrapper, params OpenGLShader[] shaders) : this(wrapper.Gl, shaders) { }

        public void Link()
        {
            gl.LinkProgram(m_Handle);
        }

        public void Use()
        {
            gl.UseProgram(m_Handle);
        }

        public void AttachShader(OpenGLShader shader)
        {
            gl.AttachShader(m_Handle, shader.Handle);
        }

        public void DetachShader(OpenGLShader shader)
        {
            gl.DetachShader(m_Handle, shader.Handle);
        }

        public void Dispose()
        {
            gl.DeleteProgram(m_Handle);
            GC.SuppressFinalize(this);
        }
    }
}
