
using OpenTK.Mathematics;

namespace Program;

public struct Vertex {
    public Vector3 Position;
    public Vector2 TextureCoordinate;

    public Vertex(Vector3 position,Vector2 textureCoordinate) {
        Position = position;
        TextureCoordinate = textureCoordinate;
    }
}