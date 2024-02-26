using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace Program;

public class Rectangle : SpatialObject {

    static float[] vertices = [
        0.5f,  0.5f, 0.0f,
        0.5f, -0.5f, 0.0f,
        -0.5f, -0.5f, 0.0f,
        -0.5f,  0.5f, 0.0f
    ];

    static uint[] indices = {
        0, 1, 3,
        1, 2, 3
    };

    Buffer vertexBuffer;
    VertexArray vertexArray;

    public Rectangle(Engine engine) : base(engine) {
        vertexBuffer = new Buffer();
        vertexBuffer.Data(
            vertices,
            BufferUsageHint.StaticDraw
        );

        vertexArray = new VertexArray(vertexBuffer);
        vertexArray.Attribute(0, 3, VertexAttribType.Float, false, 3, 0);
    }

    public override void Render()
    {
        Engine.shader.Use();
        vertexArray.Use();
        vertexArray.RenderIndexed(indices);
    }

    public override void Release()
    {
        vertexArray.Release();
        vertexBuffer.Release();
        base.Release();
    }
}