using Silk.NET.OpenGL;

namespace GameEngine.Graphics.OpenGL.Buffers
{
    public interface IBufferElement
    {
        uint Location { get; }

        int Size { get; }

        bool Normalized { get; }

        int SizeInBytes { get; }

        GLEnum ToGLType();
    }
}