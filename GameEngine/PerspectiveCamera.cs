using System.Numerics;
using GameEngine.Utils;

namespace GameEngine
{
    public class PerspectiveCamera : ICamera
    {
        public float Width { get; }

        public float Height {  get; }

        public float Near { get; }

        public float Far { get; }

        public float Fov { get; }

        public Vector3 Position { get; }

        public Vector3 Direction { get; }

        public Vector3 Up { get; }

        public Matrix4x4 ViewMatrix { get; private set; }

        public Matrix4x4 ProjectionMatrix { get; private set; }

        public PerspectiveCamera(float width, float height, float near, float far, float fov, Vector3 position)
        {
            this.Width = width;
            this.Height = height;
            this.Near = near;
            this.Far = far;
            this.Fov = fov;

            this.Position = position;
            this.Up = Vector3.UnitY;

            Vector3 direction = Vector3.Zero;
            direction.X = MathF.Cos(MathHelper.DegreesToRadians(90)) * MathF.Cos(MathHelper.DegreesToRadians(0));
            direction.Y = MathF.Sin(MathHelper.DegreesToRadians(0));
            direction.Z = MathF.Sin(MathHelper.DegreesToRadians(90)) * MathF.Cos(MathHelper.DegreesToRadians(0));
            this.Direction = Vector3.Normalize(direction);

            ComputeViewMatrix();
            ComputeProjectionMatrix();
        }

        public PerspectiveCamera(float width, float height, float fov, Vector3 position) : this(width, height, 0.1f, 100.0f, fov, position) { }
        public PerspectiveCamera(float width, float height) : this(width, height, 0.1f, 100.0f, 45, Vector3.Zero) { }

        public void ComputeViewMatrix()
        {
            ViewMatrix = Matrix4x4.CreateLookAt(Position, Position + Direction, Up);
        }

        public void ComputeProjectionMatrix()
        {
            ProjectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(Fov), Width / Height, Near, Far);
        }
    }
}
