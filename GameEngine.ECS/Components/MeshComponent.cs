using GameEngine.ECS.First;
using GameEngine.Graphics;

namespace GameEngine.ECS.Components
{
    public class MeshComponent : IComponent
    {
        public IMesh Mesh { get; }

        public MeshComponent(IMesh mesh)
        {
            Mesh = mesh; 
        }
    }
}
