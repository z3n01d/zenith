using OpenTK.Mathematics;

namespace Program;

public class Object {
    public Engine Engine;
    public Object(Engine engine) {
        engine.Add(this);
        Engine = engine;
    }

    public virtual void Update(double frameTime) {

    }

    public virtual void Render() {

    }

    public virtual void Release() {
        
    }
}

public class SpatialObject : Object {

    public Vector3 Position = new Vector3();
    public Vector3 Rotation = new Vector3();
    public Vector3 Scale = new Vector3(1,1,1);

    public SpatialObject(Engine engine) : base(engine) {

    }

    public Matrix4 ModelMatrix {
        get {

            Matrix4 model = Matrix4.Identity;

            model *= Matrix4.CreateTranslation(Position);
            model *= Matrix4.CreateScale(Scale);
            model *= Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Rotation.Z));
            model *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(Rotation.Y));
            model *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(Rotation.X));

            return model;

        }
    }
}