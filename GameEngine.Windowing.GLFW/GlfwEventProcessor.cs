using Silk.NET.GLFW;

namespace GameEngine.Windowing.GLFW
{
    public class GlfwEventProcessor : IEventProcessor
    {
        public void ProcessEvents()
        {
            GlfwProvider.GLFW.Value.PollEvents();
        }
    }
}
