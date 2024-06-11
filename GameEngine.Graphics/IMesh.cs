namespace GameEngine.Graphics
{
    public interface IMesh
    {
        List<float> Vertices { get; }
        List<float> Normals { get; }
        List<float> Uvs { get; }
        List<uint> Indices { get; }
    }
}
