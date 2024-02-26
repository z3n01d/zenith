using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using System.Runtime.InteropServices;

namespace Program;

public class Cube : SpatialObject {

    Vertex[] vertices = {
        new Vertex((-1, -1,  1),(0,  0)),
        new Vertex(( 1, -1,  1),(1,  0)),
        new Vertex(( 1,  1,  1),(1,  1)),
        new Vertex((-1,  1,  1),(0,  1)),
        new Vertex((-1,  1, -1),(0,  0)),
        new Vertex((-1, -1, -1),(1,  0)),
        new Vertex(( 1, -1, -1),(1,  1)),
        new Vertex(( 1,  1, -1),(0,  1)),
    };

    uint[] indices = {
        0,2,3,
        0,1,2,
        1,7,2,
        1,6,7,
        6,5,4,
        4,7,6,
        3,4,5,
        3,5,0,
        3,7,4,
        3,2,7,
        0,6,1,
        0,5,6
    };

    Buffer vertexBuffer;
    VertexArray vertexArray;

    public Cube(Engine engine) : base(engine) {

        vertexBuffer = new Buffer();
        vertexBuffer.Data(
            vertices,
            BufferUsageHint.StaticDraw
        );

        vertexArray = new VertexArray(vertexBuffer);

        vertexArray.Attribute(0, 3, VertexAttribType.Float, false, 5, (int)Marshal.OffsetOf(typeof(Vertex),"Position"));
        vertexArray.Attribute(1, 2, VertexAttribType.Float, false, 5, (int)Marshal.OffsetOf(typeof(Vertex),"TextureCoordinate"));
    }

    public override void Render()
    {
        Engine.shader.Use();
        vertexArray.Use();
        vertexArray.RenderIndexed(indices, PrimitiveType.Triangles);
    }

    public override void Release()
    {
        vertexArray.Release();
        vertexBuffer.Release();
        base.Release();
    }
}