using GameEngine.Graphics;
using GameEngine.Graphics.OpenGL;
using GameEngine.Windowing;
using GameEngine.Windowing.GLFW;
using Microsoft.Extensions.DependencyInjection;

namespace Sandbox
{

    internal class Program
    {
        public static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IWindow, GlfwWindow>();
            services.AddSingleton<IEventProcessor, GlfwEventProcessor>();

            services.AddSingleton<IRenderContext>((x) =>
            {
                return new OpenGLRenderContext(((GlfwWindow)x.GetRequiredService<IWindow>()).GLContext);
            });

            services.AddSingleton(x =>
            {
                return new GLWrapper(((GlfwWindow)x.GetRequiredService<IWindow>()).GLContext);
            });

            services.AddSingleton<IRenderer, OpenGLRenderer>();

            IServiceProvider provider = services.BuildServiceProvider();

            using (TestApplication application = new TestApplication(provider))
            {
                application.Run();
            }
        }
    }
}
