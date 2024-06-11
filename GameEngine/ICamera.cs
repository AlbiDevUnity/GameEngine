using System.Numerics;

namespace GameEngine
{
    public interface ICamera
    {
        float Width { get; }

        float Height { get; }

        float Near { get; }

        float Far {  get; }

        Vector3 Position { get; }

        Vector3 Direction { get; }

        Vector3 Up { get; }

        Matrix4x4 ViewMatrix { get; }

        Matrix4x4 ProjectionMatrix { get; }
    }
    
}
