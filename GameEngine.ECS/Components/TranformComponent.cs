using GameEngine.ECS.First;
using GameEngine.Utils;
using System.Numerics;

namespace GameEngine.ECS.Components
{
    public class TranformComponent : IComponent
    {
        private Vector3 m_Position;
        private Vector3 m_Rotation;
        private Vector3 m_Scale;

        public Vector3 Position
        {
            get => m_Position;
            set
            {
                m_Position = value;
                IsDirty = true;
            }
        }
        public Vector3 Rotation
        {
            get => m_Rotation;
            set
            {
                m_Rotation = value;
                IsDirty = true;
            }
        }
        public Vector3 Scale
        {
            get => m_Scale;
            set
            {
                m_Scale = value;
                IsDirty = true;
            }
        }

        public Matrix4x4 ModelMatrix { get; private set; }
        public bool IsDirty { get; private set; }

        public TranformComponent(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;

            ComputeModelMatrix();
            IsDirty = false;
        }

        public TranformComponent(Vector3 position) : this(position, Vector3.Zero, Vector3.One) { }

        public TranformComponent() : this(Vector3.Zero, Vector3.Zero, Vector3.One) { }

        public void ComputeModelMatrix()
        {
            ModelMatrix = Matrix4x4.CreateScale(Scale)
                * Matrix4x4.CreateRotationX(MathHelper.DegreesToRadians(Rotation.X))
                * Matrix4x4.CreateRotationY(MathHelper.DegreesToRadians(Rotation.Y))
                * Matrix4x4.CreateRotationZ(MathHelper.DegreesToRadians(Rotation.Z))
                * Matrix4x4.CreateTranslation(Position);

            IsDirty = false;
        }
    }
}
