using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace Program;

public class Model : SpatialObject {

    float[] vertices;

    uint[] indices;

    Buffer vertexBuffer;
    VertexArray vertexArray;

    public Model(Engine engine, string modelName) : base(engine) {

        ModelData modelData = Engine.Models.Load(modelName);
        vertices = modelData.Vertices.ToArray();
        indices = modelData.Indices.ToArray();

        vertexBuffer = new Buffer();
        vertexBuffer.Data(
            vertices,
            BufferUsageHint.StaticDraw
        );

        vertexArray = new VertexArray(vertexBuffer);
        vertexArray.Attribute(0, 3, VertexAttribType.Float, false, 6, 0);
        vertexArray.Attribute(1, 3, VertexAttribType.Float, false, 6, 3);
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