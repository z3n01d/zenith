
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Program;

public class Shader : GLObject {
    public Shader(string vertexShaderPath = "./shaders/default.vert", string fragmentShaderPath = "./shaders/default.frag") {
        string vertexShaderSource = File.ReadAllText(vertexShaderPath);
        string fragmentShaderSource = File.ReadAllText(fragmentShaderPath);

        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexShaderSource);

        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, fragmentShaderSource);

        // Vertex Shader compilation
        GL.CompileShader(vertexShader);

        GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int success);
        if (success == 0) {
            string infoLog = GL.GetShaderInfoLog(vertexShader);
            Console.WriteLine("ERROR in {0} : \n{1}", vertexShaderPath, infoLog);
        }

        // Fragment Shader compilation
        GL.CompileShader(fragmentShader);

        GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out success);
        if (success == 0)
        {
            string infoLog = GL.GetShaderInfoLog(fragmentShader);
            Console.WriteLine("ERROR in {0} : \n{1}", fragmentShaderPath, infoLog);
        }

        Handle = GL.CreateProgram();

        GL.AttachShader(Handle, vertexShader);
        GL.AttachShader(Handle, fragmentShader);

        GL.LinkProgram(Handle);

        GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out success);
        if (success == 0)
        {
            string infoLog = GL.GetProgramInfoLog(Handle);
            Console.WriteLine(infoLog);
        }

        GL.DetachShader(Handle, vertexShader);
        GL.DetachShader(Handle, fragmentShader);
        GL.DeleteShader(fragmentShader);
        GL.DeleteShader(vertexShader);
    }
    
    // Uniform methods for Vectors
    public void Uniform(string Name,double Value) {
        GL.Uniform1(GL.GetUniformLocation(Handle, Name), Value);
    }

    public void Uniform(string Name,Vector2 Value) {
        GL.Uniform2(GL.GetUniformLocation(Handle, Name), Value);
    }

    public void Uniform(string Name,Vector3 Value) {
        GL.Uniform3(GL.GetUniformLocation(Handle, Name), Value);
    }

    public void Uniform(string Name,Vector4 Value) {
        GL.Uniform4(GL.GetUniformLocation(Handle, Name), Value);
    }

    // Uniform methods for Matrices
    public void Uniform(string Name,Matrix2 Value) {
        GL.UniformMatrix2(GL.GetUniformLocation(Handle, Name),false, ref Value);
    }

    public void Uniform(string Name,Matrix3 Value) {
        GL.UniformMatrix3(GL.GetUniformLocation(Handle, Name), false, ref Value);
    }

    public void Uniform(string Name,Matrix4 Value) {
        GL.UniformMatrix4(GL.GetUniformLocation(Handle, Name),false, ref Value);
    }

    public void Use() {
        GL.UseProgram(Handle);
    }

    public override void Release() {
        GL.DeleteProgram(Handle);
        base.Release();
    }
}