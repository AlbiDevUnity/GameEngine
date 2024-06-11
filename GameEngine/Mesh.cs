using GameEngine.Graphics;
using System.Globalization;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace GameEngine
{
    public class Mesh : IMesh
    {
        public List<float> Vertices { get; }
        public List<float> Normals { get; }
        public List<uint> Indices { get; }
        public List<float> Uvs { get; }

        public Mesh(float[] vertices, uint[] indices, float[] uvs)
        {
            Vertices = new List<float>(vertices);
            Normals = new List<float>();
            Uvs = new List<float>(uvs);
            Indices = new List<uint>(indices);
        }

        public Mesh(string path)
        {
            if (!File.Exists(path))
            {
                throw new Exception("File doesn't exist!");
            }

            this.Vertices = new List<float>();
            this.Normals = new List<float>();
            this.Uvs = new List<float>();
            this.Indices = new List<uint>();

            List<Vector3> verticesTemp = new List<Vector3>();
            List<Vector3> normalsTemp = new List<Vector3>();
            List<Vector2> uvsTemp = new List<Vector2>();

            List<uint> verticesIndices = new List<uint>();
            List<uint> normalsIndices = new List<uint>();
            List<uint> uvsIndices = new List<uint>();

            StreamReader reader = new StreamReader(path);
            string? line = reader.ReadLine();

            while (line != null)
            {
                if(line.StartsWith("v "))
                {
                    string[] members = line.Split(' ');

                    float x = float.Parse(members[1], CultureInfo.InvariantCulture.NumberFormat);
                    float y = float.Parse(members[2], CultureInfo.InvariantCulture.NumberFormat);
                    float z = float.Parse(members[3], CultureInfo.InvariantCulture.NumberFormat);

                    verticesTemp.Add(new Vector3(x, y, z));
                }
                else if(line.StartsWith("vn "))
                {
                    string[] members = line.Split(' ');

                    float x = float.Parse(members[1], CultureInfo.InvariantCulture.NumberFormat);
                    float y = float.Parse(members[2], CultureInfo.InvariantCulture.NumberFormat);
                    float z = float.Parse(members[3], CultureInfo.InvariantCulture.NumberFormat);

                    normalsTemp.Add(new Vector3(x, y, z));
                }
                else if(line.StartsWith("vt "))
                {
                    string[] members = line.Split(' ');

                    float x = float.Parse(members[1], CultureInfo.InvariantCulture.NumberFormat);
                    float y = float.Parse(members[2], CultureInfo.InvariantCulture.NumberFormat);

                    uvsTemp.Add(new Vector2(x, y));
                }
                else if(line.StartsWith("f "))
                {
                    string[] members = line.Split(' ');

                    for(int i = 1; i < members.Length; i++)
                    {
                        string[] indices = members[i].Split('/');

                        verticesIndices.Add(uint.Parse(indices[0], CultureInfo.InvariantCulture.NumberFormat));
                        uvsIndices.Add(uint.Parse(indices[1], CultureInfo.InvariantCulture.NumberFormat));
                        normalsIndices.Add(uint.Parse(indices[2], CultureInfo.InvariantCulture.NumberFormat));
                    }
                }

                for(int i = 0; i < verticesIndices.Count; i++)
                {
                    int index = (int)verticesIndices[i] - 1;
                    Vector3 vertex = verticesTemp[index];

                    this.Vertices.Add(vertex.X);
                    this.Vertices.Add(vertex.Y);
                    this.Vertices.Add(vertex.Z);
                }

                for(int i = 0; i < normalsIndices.Count; i++)
                {
                    int index = (int)normalsIndices[i] - 1;
                    Vector3 normal = normalsTemp[index];

                    this.Normals.Add(normal.X);
                    this.Normals.Add(normal.Y);
                    this.Normals.Add(normal.Z);
                }

                for(int i = 0; i < uvsIndices.Count; i++)
                {
                    int index = (int)uvsIndices[i] - 1;
                    Vector2 uv = uvsTemp[index];

                    this.Uvs.Add(uv.X);
                    this.Uvs.Add(uv.Y);
                }

                line = reader.ReadLine();
            }

        }

        private void idk(string path)
        {
            if (!File.Exists(path))
            {
                throw new Exception("File doesn't exist!");
            }

            //this.Vertices = new List<float>();
            //this.Normals = new List<float>();
            //this.Uvs = new List<float>();
            //this.Indices = new List<uint>();


            List<Vector3> tempVertices = new List<Vector3>();
            List<Vector3> tempNormals = new List<Vector3>();
            List<Vector2> tempUvs = new List<Vector2>();

            uint count = 0;

            Regex vertexsRegex = new Regex("v ([+-]?[0-1]*[.]?[0-9]+) ([+-]?[0-1]*[.]?[0-9]+) ([+-]?[0-1]*[.]?[0-9]+)");
            Regex normalsRegex = new Regex("vn ([+-]?[0-1]*[.]?[0-9]+) ([+-]?[0-1]*[.]?[0-9]+) ([+-]?[0-1]*[.]?[0-9]+)");
            Regex uvsRegex = new Regex("vt ([0-1]*[.]?[0-9]+) ([0-1]*[.]?[0-9]+)");
            Regex facesReges = new Regex("f ([0-9]+/[0-9]+/[0-9]) ([0-9]+/[0-9]+/[0-9]) ([0-9]+/[0-9]+/[0-9])");

            string text = File.ReadAllText(path);

            MatchCollection vertexMatch = vertexsRegex.Matches(text);
            foreach (Match match in vertexMatch)
            {
                if (match.Success)
                {
                    foreach (Capture capture in match.Captures)
                    {
                        string[] strings = capture.Value.Split(" ");

                        float x = float.Parse(strings[1], CultureInfo.InvariantCulture.NumberFormat);
                        float y = float.Parse(strings[2], CultureInfo.InvariantCulture.NumberFormat);
                        float z = float.Parse(strings[3], CultureInfo.InvariantCulture.NumberFormat);

                        tempVertices.Add(new Vector3(x, y, z));
                    }
                }
            }

            MatchCollection normalMatch = normalsRegex.Matches(text);
            foreach (Match match in normalMatch)
            {
                if (match.Success)
                {
                    foreach (Capture capture in match.Captures)
                    {
                        string[] strings = capture.Value.Split(" ");

                        float x = float.Parse(strings[1], CultureInfo.InvariantCulture.NumberFormat);
                        float y = float.Parse(strings[2], CultureInfo.InvariantCulture.NumberFormat);
                        float z = float.Parse(strings[3], CultureInfo.InvariantCulture.NumberFormat);

                        tempNormals.Add(new Vector3(x, y, z));
                    }
                }
            }

            MatchCollection uvMatch = uvsRegex.Matches(text);
            foreach (Match match in uvMatch)
            {
                if (match.Success)
                {
                    foreach (Capture capture in match.Captures)
                    {
                        string[] strings = capture.Value.Split(" ");

                        float x = float.Parse(strings[1], CultureInfo.InvariantCulture.NumberFormat);
                        float y = float.Parse(strings[2], CultureInfo.InvariantCulture.NumberFormat);

                        tempUvs.Add(new Vector2(x, y));
                    }
                }
            }

            MatchCollection facesMatch = facesReges.Matches(text);
            foreach (Match match in facesMatch)
            {
                if (match.Success)
                {
                    foreach (Capture capture in match.Captures)
                    {
                        string[] strings = capture.Value.Split(" ");

                        for (int i = 1; i < strings.Length; i++)
                        {
                            string[] split = strings[i].Split("/");

                            int vertexIndex = int.Parse(split[0]);
                            int uvIndex = int.Parse(split[1]);
                            int normalIndex = int.Parse(split[2]);

                            this.Vertices.Add(tempVertices[vertexIndex - 1].X);
                            this.Vertices.Add(tempVertices[vertexIndex - 1].Y);
                            this.Vertices.Add(tempVertices[vertexIndex - 1].Z);

                            this.Normals.Add(tempNormals[normalIndex - 1].X);
                            this.Normals.Add(tempNormals[normalIndex - 1].Y);
                            this.Normals.Add(tempNormals[normalIndex - 1].Z);

                            this.Uvs.Add(tempUvs[uvIndex - 1].X);
                            this.Uvs.Add(tempUvs[uvIndex - 1].Y);

                            this.Indices.Add(count);
                            count++;
                        }
                    }
                }
            }
        }
    }
}
