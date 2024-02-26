using OpenTK.Graphics.OpenGL4;

namespace Program;

public class VertexArray : GLObject {
    Buffer vbo;
    public VertexArray(Buffer vertexBuffer) {
        vbo = vertexBuffer;
        GL.CreateVertexArrays(1, out Handle);
    }

    public void Use() {
        GL.BindVertexArray(Handle);
    }

    public void Attribute(int location,int size,VertexAttribType attribType,bool normalized,int stride,int offset) {
        GL.VertexArrayVertexBuffer(Handle, 0, vbo.Handle, 0, stride * sizeof(float));

        GL.EnableVertexArrayAttrib(Handle, location);

        GL.VertexArrayAttribFormat(Handle, location, size, attribType, normalized, offset * sizeof(float));
        GL.VertexArrayAttribBinding(Handle, location, 0);
    }

    public void Render(int vertexCount,PrimitiveType renderType = PrimitiveType.Triangles) {
        GL.DrawArrays(renderType, 0, vertexCount);
    }

    public void RenderIndexed(int indexCount, PrimitiveType renderType = PrimitiveType.TriangleStrip) {
        GL.DrawElements(renderType, indexCount, DrawElementsType.UnsignedInt, 0);
    }

    public void RenderIndexed(uint[] indices, PrimitiveType renderType = PrimitiveType.TriangleStrip) {
        GL.DrawElements(renderType, indices.Length, DrawElementsType.UnsignedInt, indices);
    }
}