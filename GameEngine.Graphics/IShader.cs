
namespace GameEngine.Graphics
{
    public enum ShaderType
    {
        VertexShader,
        FragmentShader,
        GeometryShader,
        ComputeShader,
    }

    public interface IShader : IDisposable
    {
        ShaderType Type { get; }
    }
}
