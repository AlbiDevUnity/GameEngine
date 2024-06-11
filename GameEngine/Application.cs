using GameEngine.Graphics;
using GameEngine.Windowing;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System.Numerics;

namespace GameEngine
{
    public abstract class Application : IDisposable
    {
        //window
        //renderer

        private readonly IWindow m_Window;
        private readonly IEventProcessor m_EventProcessor;
        private readonly IRenderContext m_RenderContext;
        private readonly IRenderer m_Renderer;

        private bool m_IsRunning;
        private readonly Stopwatch m_Stopwatch;

        private List<TempEntity> m_Entities = new List<TempEntity>();

        public unsafe Application(IServiceProvider provider)
        {
            m_Window = provider.GetRequiredService<IWindow>();
            m_EventProcessor = provider.GetRequiredService<IEventProcessor>();
            m_RenderContext = provider.GetRequiredService<IRenderContext>();
            m_Renderer = provider.GetRequiredService<IRenderer>();

            m_IsRunning = false;
            m_Stopwatch = new Stopwatch();
        }


        public abstract void OnStart();
        public abstract void OnUpdate(float dt);

        public void Run()
        {
            m_IsRunning = true;
            m_RenderContext.Init();
            m_Renderer.Init();

            OnStart();

            double lastTime = 0;
            m_Stopwatch.Start();

            ICamera camera = new PerspectiveCamera(m_Window.Size.Width, m_Window.Size.Height, 45.0f, new Vector3(0, 0, -10.0f));
            m_Entities.Add(new TempEntity(new Mesh(@"C:\Users\albir\Desktop\Dev\Progetti C#\Engine\GameEngine.Graphics\Resources\suzanne.obj")));

            foreach (IMesh mesh in m_Entities.Select(e => e.Mesh).Distinct())
            {
                m_Renderer.LoadMesh(mesh);
            }

            while (m_IsRunning)
            {
                double currentTime = m_Stopwatch.Elapsed.TotalMilliseconds;
                float dt = (float)(currentTime - lastTime);
                lastTime = currentTime;

                OnUpdate(dt);

                foreach(TempEntity e in m_Entities)
                {
                    e.Rotation += new Vector3(0.0f, 1.0f, 0.0f) * 0.1f * dt;
                }

                m_Renderer.Clear();

                m_Renderer.SetUniform("view_matrix", camera.ViewMatrix);
                m_Renderer.SetUniform("proj_matrix", camera.ProjectionMatrix);

                foreach(TempEntity entity in m_Entities)
                {
                    m_Renderer.SetUniform("model_matrix", entity.ModelMatrix);
                    m_Renderer.Draw(PrimitiveType.Triangles, entity.Mesh);
                }

                m_EventProcessor.ProcessEvents();
                m_RenderContext.SwapBuffers();

                //event manager distach events
            }
        }

        public void ForceExit()
        {
            m_IsRunning = false;
        }

        public void Dispose()
        {
            
            m_Window.Dispose();
            m_Stopwatch.Stop();
            GC.SuppressFinalize(this);
        }
    }
}
