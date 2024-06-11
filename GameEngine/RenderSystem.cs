using GameEngine.ECS.Components;
using GameEngine.ECS.First;
using GameEngine.Graphics;

namespace GameEngine
{
    public class RenderSystem : ISystem
    {
        private readonly IRenderer m_Renderer;
        private readonly ICamera m_Camera;

        public RenderSystem(IRenderer renderer, ICamera camera)
        {
            m_Renderer = renderer;
            m_Camera = camera;
        }

        public void Process(IEnumerable<Entity> entities)
        {
            m_Renderer.SetUniform("view_matrix", m_Camera.ViewMatrix);
            m_Renderer.SetUniform("proj_matrix", m_Camera.ProjectionMatrix);
            
            foreach(Entity entity in entities)
            {
                TranformComponent tranformComponent = entity.GetComponent<TranformComponent>();
                MeshComponent meshComponent = entity.GetComponent<MeshComponent>();

                m_Renderer.SetUniform("model_matrix", tranformComponent.ModelMatrix);
                m_Renderer.Draw(PrimitiveType.Triangles, meshComponent.Mesh);
            }
        }
    }
}
