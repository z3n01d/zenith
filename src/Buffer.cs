
using OpenTK.Graphics.OpenGL4;
using System.Runtime.InteropServices;

namespace Program;

public class Buffer : GLObject {

    public Buffer() {
        GL.CreateBuffers(1, out Handle);
    }

    public void Data(float[] data, BufferUsageHint usageHint) {
        GL.NamedBufferData(Handle, data.Length * sizeof(float), data, usageHint);
    }

    public void Data(int[] data, BufferUsageHint usageHint) {
        GL.NamedBufferData(Handle, data.Length * sizeof(int), data, usageHint);
    }

    public void Data(uint[] data, BufferUsageHint usageHint) {
        GL.NamedBufferData(Handle, data.Length * sizeof(uint), data, usageHint);
    }

    public void Data(Vertex[] data, BufferUsageHint usageHint) {
        GL.NamedBufferData(Handle, data.Length * Marshal.SizeOf(typeof(Vertex)), data, usageHint);
    }

    public override void Release()
    {
        GL.DeleteBuffer(Handle);
        base.Release();
    }
}