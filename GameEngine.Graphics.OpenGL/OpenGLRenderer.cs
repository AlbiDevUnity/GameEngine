using GameEngine.Graphics.OpenGL.Buffers;
using Silk.NET.OpenGL;
using System.Drawing;
using System.Numerics;

namespace GameEngine.Graphics.OpenGL
{
    public class OpenGLRenderer : IRenderer
    {
        private readonly GL gl;

        private OpenGLShaderProgram? program;

        private readonly Dictionary<IMesh, OpenGLVertexArray> meshes = new Dictionary<IMesh, OpenGLVertexArray>();

        public OpenGLRenderer(GLWrapper wrapper)
        {
            gl = wrapper.Gl;
        }

        public unsafe void Init()
        {
            //shaders
            OpenGLShader vertexShader = new OpenGLShader(gl, ShaderType.VertexShader);
            string vertexSource = File.ReadAllText(@"C:\Users\albir\Desktop\Dev\Progetti C#\Engine\GameEngine.Graphics\Resources\vert.glsl");
            vertexShader.Source(vertexSource);
            vertexShader.Compile();

            OpenGLShader fragmentShader = new OpenGLShader(gl, ShaderType.FragmentShader);
            string fragmentSource = File.ReadAllText(@"C:\Users\albir\Desktop\Dev\Progetti C#\Engine\GameEngine.Graphics\Resources\frag.glsl");
            fragmentShader.Source(fragmentSource);
            fragmentShader.Compile();

            program = new OpenGLShaderProgram(gl, vertexShader, fragmentShader);

            program.Link();
            program.Use();

            //buffers

            gl.ClearColor(Color.DarkSlateGray);
            //output merger compito i think
            gl.Enable(EnableCap.DepthTest);
            gl.Disable(EnableCap.CullFace);
            gl.PolygonMode(GLEnum.FrontAndBack, GLEnum.Fill);
        }

        public unsafe void LoadMesh(IMesh mesh)
        {
            if (meshes.ContainsKey(mesh)) { return; }

            OpenGLVertexArray vao = new OpenGLVertexArray(gl);
            vao.Bind();

            OpenGLVertexBuffer vertexBo = new OpenGLVertexBuffer(gl);
            vertexBo.Bind();
            vertexBo.SetData(mesh.Vertices.ToArray());

            OpenGLBufferLayout vertexLayout = new OpenGLBufferLayout(gl, new BufferElement<float>(0, 3));
            vertexLayout.Bind();

            OpenGLVertexBuffer uvsBo = new OpenGLVertexBuffer(gl);
            uvsBo.Bind();
            uvsBo.SetData(mesh.Uvs.ToArray());

            OpenGLBufferLayout uvsLayout = new OpenGLBufferLayout(gl, new BufferElement<float>(1, 2));
            uvsLayout.Bind();

            //OpenGLIndexBuffer indexBuffer = new OpenGLIndexBuffer(gl);
            //indexBuffer.Bind();
            //indexBuffer.SetData(mesh.Indices.ToArray());

            meshes.Add(mesh, vao);
            vao.Unbind();
        }

        public void Clear()
        {
            gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        public unsafe void Draw(PrimitiveType primitiveType, IMesh mesh)
        {
            if(!meshes.TryGetValue(mesh, out var vao)) { return; }
            vao.Bind();

            gl.DrawArrays(GLEnum.Triangles, 0, (uint)mesh.Vertices.Count);

            //gl.DrawElements(GLEnum.Triangles, (uint)mesh.Indices.Count, GLEnum.UnsignedInt, null);

            vao.Unbind();
        }

        public unsafe void SetUniform(string name, Matrix4x4 matrix)
        {
            int location = gl.GetUniformLocation(program!.Handle, name);
            if (location == -1)
            {
                throw new Exception($"{name} uniform not found on shader.");
            }

            gl.UniformMatrix4(location, 1, false, (float*)&matrix);
        }

        public void Dispose()
        {
            gl.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
