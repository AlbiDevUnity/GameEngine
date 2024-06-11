using Silk.NET.OpenGL;

namespace GameEngine.Graphics.OpenGL
{
    public class OpenGLShader : IShader
    {
        private readonly GL gl;

        private readonly uint m_Handle;

        public ShaderType Type { get; }

        public uint Handle => m_Handle;

        public OpenGLShader(GL gl, ShaderType type)
        {
            this.gl = gl;
            Type = type;

            GLEnum GLType = type switch
            {
                ShaderType.VertexShader => GLEnum.VertexShader,
                ShaderType.FragmentShader => GLEnum.FragmentShader,
                ShaderType.GeometryShader => GLEnum.GeometryShader,
                ShaderType.ComputeShader => GLEnum.ComputeShader,
                _ => throw new NotImplementedException(),
            };

            m_Handle = gl.CreateShader(GLType);
        }

        public OpenGLShader(GLWrapper wrapper, ShaderType type) : this(wrapper.Gl, type) { }

        public void Source(string source)
        {
            gl.ShaderSource(m_Handle, source);
        }

        public void Compile()
        {
            gl.CompileShader(m_Handle);
        }

        public void Dispose()
        {
            gl.DeleteShader(m_Handle);
            GC.SuppressFinalize(this);
        }
    }
}
