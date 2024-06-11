using Silk.NET.Core.Contexts;
using Silk.NET.GLFW;
using System.Drawing;

namespace GameEngine.Windowing.GLFW
{
    public class GlfwWindow : IWindow
    {
        private unsafe readonly WindowHandle* m_Handle;

        public string Title { get; }

        public Size Size {  get; }

        public IGLContext GLContext { get; }

        public unsafe GlfwWindow(string title, int width, int height)
        {
            this.Title = title;
            Size = new Size(width, height);

            GlfwProvider.GLFW.Value.WindowHint(WindowHintOpenGlProfile.OpenGlProfile, OpenGlProfile.Core);
            GlfwProvider.GLFW.Value.WindowHint(WindowHintClientApi.ClientApi, ClientApi.OpenGL);
            GlfwProvider.GLFW.Value.WindowHint(WindowHintInt.ContextVersionMajor, 4);
            GlfwProvider.GLFW.Value.WindowHint(WindowHintInt.ContextVersionMinor, 6);

            m_Handle = GlfwProvider.GLFW.Value.CreateWindow(width, height, title, null, null);
            GLContext = new GlfwContext(GlfwProvider.GLFW.Value, m_Handle);
        }

        public GlfwWindow() : this("Shit Engine", 1280, 720) { }

        public void Dispose()
        {
            GlfwProvider.Unload();
            GC.SuppressFinalize(this);
        }
    }
}
