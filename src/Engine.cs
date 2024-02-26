
using System.Runtime.CompilerServices;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Program;

public delegate void OnUpdateEvent(double frameTime);

public class Engine : Window {

    public Shader shader;
    List<Object> objects = new List<Object>();

    public ModelLoader Models = new ModelLoader("./models");

    Camera camera;

    public event OnUpdateEvent OnUpdate;

    public Engine(int width, int height, string title) : base(width, height, title) {
        camera = new FPSCamera(this);
        camera.Position.Z = 3;
        shader = new Shader();
    }

    public void Add(Object obj) {
        objects.Add(obj);
    }

    public override void Loaded() {
        GL.ClearColor(0.2f,0.3f,0.3f,1.0f);
        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.CullFace);
        GL.DepthFunc(DepthFunction.Lequal);
        
        base.Loaded();
    }

    public override void Unloaded()
    {
        foreach (Object obj in objects)
        {
            obj.Release();
        }
        shader.Release();
        base.Unloaded();
    }

    public override void Update(double frameTime) {
        camera.Update(frameTime);
        foreach (Object obj in objects)
        {
            obj.Update(frameTime);
        }
        OnUpdate.Invoke(frameTime);
        base.Update(frameTime);
    }

    public override void Render() {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        shader.Use();

        foreach (Object obj in objects) {
            if (obj is SpatialObject spatialObject)
            {
                shader.Uniform("viewMatrix", spatialObject.ModelMatrix * camera.ViewMatrix * camera.ProjectionMatrix);
            } else {
                shader.Uniform("viewMatrix", camera.ViewMatrix * camera.ProjectionMatrix);
            }
            obj.Render();
        }

        SwapBuffers();
        base.Render();
    }

    protected override void OnResize(ResizeEventArgs e) {
        base.OnResize(e);
        GL.Viewport(0, 0, e.Width, e.Height);
    }
}