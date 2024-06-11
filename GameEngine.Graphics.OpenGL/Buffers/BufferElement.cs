using Silk.NET.OpenGL;

namespace GameEngine.Graphics.OpenGL.Buffers
{
    public class BufferElement<TData> : IBufferElement where TData : unmanaged
    {
        public uint Location { get; }

        public int Size { get; }

        public bool Normalized { get; }

        public unsafe int SizeInBytes => Size * sizeof(TData);

        public BufferElement(uint location, int size, bool normalized)
        {
            Location = location;
            Size = size;
            Normalized = normalized;
        }

        public BufferElement(uint location, int size) : this(location, size, false) { }

        public GLEnum ToGLType()
        {
            if(typeof(TData) == typeof(int))
            {
                return GLEnum.Int;
            }
            else if (typeof(TData) == typeof(float))
            {
                return GLEnum.Float;
            }
            else if (typeof(TData) == typeof(uint))
            {
                return GLEnum.UnsignedInt;
            }

            return GLEnum.None;
        }
    }
}
