using Silk.NET.OpenGL;

namespace GameEngine.Graphics.OpenGL.Buffers
{
    public class OpenGLBufferLayout
    {
        private readonly GL gl;

        private readonly List<IBufferElement> m_Elements;

        public IReadOnlyCollection<IBufferElement> Elements => m_Elements;

        public OpenGLBufferLayout(GL gl, params IBufferElement[] elements)
        {
            this.gl = gl;
            m_Elements = elements.ToList();
        }

        public void Bind()
        {
            nint offset = 0;
            uint stride = (uint)m_Elements.Select(e => e.SizeInBytes).Sum();
            foreach(IBufferElement element in m_Elements)
            {
                gl.VertexAttribPointer(element.Location, element.Size, element.ToGLType(), element.Normalized, stride, offset);
                gl.EnableVertexAttribArray(element.Location);

                offset += element.SizeInBytes;
            }
        }
    }
}
