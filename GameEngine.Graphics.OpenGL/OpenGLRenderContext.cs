using Silk.NET.Core.Contexts;
using Silk.NET.GLFW;

namespace GameEngine.Graphics.OpenGL
{
    public class OpenGLRenderContext : IRenderContext
    {
        private readonly IGLContext m_Context;

        public IGLContext Context => m_Context;

        public OpenGLRenderContext(IGLContext context)
        {
            m_Context = context;
        }

        public unsafe OpenGLRenderContext(WindowHandle* windowHandle) : this(new GlfwContext(GlfwProvider.GLFW.Value, windowHandle)) { }

        public unsafe void Init()
        {
            m_Context.MakeCurrent();
        }

        public unsafe void SwapBuffers()
        {
            m_Context.SwapBuffers();
        }
    }
}
