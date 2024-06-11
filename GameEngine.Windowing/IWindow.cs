using System.Drawing;

namespace GameEngine.Windowing
{
    public interface IWindow : IDisposable
    {
        string Title { get; }

        Size Size { get; }
        
    }
}
