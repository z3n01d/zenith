
using System.ComponentModel;
using Assimp;
using OpenTK.Mathematics;

namespace Program;

public struct ModelData {
    public List<float> Vertices;
    public List<float> TextureCoordinates;
    public List<uint> Indices;

    public ModelData() {
        Vertices = new List<float>();
        TextureCoordinates = new List<float>();
        Indices = new List<uint>();
    }
}

public class ModelLoader {

    AssimpContext Context = new AssimpContext();
    Dictionary<string, string> Paths = new Dictionary<string, string>();

    public ModelLoader(string modelsPath) {
        foreach (string filePath in Directory.EnumerateFiles(modelsPath,"*.*",SearchOption.AllDirectories)) {
            if (Path.GetExtension(filePath) == ".bin" || Path.GetExtension(filePath) == ".mtl") {
                continue;
            }
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            Paths[fileName] = filePath;
        }
    }

    public ModelData Load(string name) {
        ModelData modelData = new ModelData();
        Scene scene = Context.ImportFile(Paths[name], PostProcessSteps.Triangulate | PostProcessSteps.JoinIdenticalVertices);

        foreach (Mesh mesh in scene.Meshes.ToArray()) {
            Vector3D[] vertices = mesh.Vertices.ToArray();
            Vector3D[] uvs = mesh.TextureCoordinateChannels[0].ToArray();

            for (int i = 0; i < mesh.VertexCount; i += 2) {
                Vector3D vertex = vertices[i];
                Vector3D uv = uvs[i];

                modelData.Vertices.Append(vertex.X);
                modelData.Vertices.Append(vertex.Y);
                modelData.Vertices.Append(vertex.Z);

                modelData.Vertices.Append(uv.X);
                modelData.Vertices.Append(uv.Y);
                modelData.Vertices.Append(uv.Z);
            }

            for (int i = 0; i < mesh.FaceCount; i++) {
                Face face = mesh.Faces[i];

                for (int x = 0; x < face.IndexCount; x++) {
                    int index = face.Indices[x];

                    modelData.Indices.Append((uint)index);
                }
            }
        }

        return modelData;
    }
}