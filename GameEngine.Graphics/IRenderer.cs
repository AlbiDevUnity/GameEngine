using System.Numerics;

namespace GameEngine.Graphics
{
    public enum PrimitiveType
    {
        Triangles,
        TriangleStrips,
        TriangleFans,
    }

    public interface IRenderer : IDisposable
    {
        //load all meshes
        void Init();

        void LoadMesh(IMesh mesh);

        void Clear();

        void Draw(PrimitiveType primitiveType, IMesh mesh);

        void SetUniform(string name, Matrix4x4 matrix);
    }
}
