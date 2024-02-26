
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Program;

public class Camera : SpatialObject {

    public Vector3 Forward = new Vector3(0f, 0f, -1f);
    public Vector3 Up = new Vector3(0f, 1f, 0);
    public Vector3 Right = new Vector3(1f,0f,0f);
    public float FieldOfView = 90f;
    public float DepthNear = 0.1f;
    public float DepthFar = 100f;

    public Camera(Engine engine) : base(engine) {
        Rotation.Y = -90f;
    }

    public override void Update(double frameTime)
    {
        base.Update(frameTime);

        Forward.X = MathF.Cos(MathHelper.DegreesToRadians(Rotation.X)) * MathF.Cos(MathHelper.DegreesToRadians(Rotation.Y));
        Forward.Y = MathF.Sin(MathHelper.DegreesToRadians(Rotation.X));
        Forward.Z = MathF.Cos(MathHelper.DegreesToRadians(Rotation.X)) * MathF.Sin(MathHelper.DegreesToRadians(Rotation.Y));
        Forward = Vector3.Normalize(Forward);
        Right = Vector3.Normalize(Vector3.Cross(Forward, Vector3.UnitY));
        Up = Vector3.Normalize(Vector3.Cross(Right, Forward));
    }

    public Matrix4 ViewMatrix {
        get { return Matrix4.LookAt(Position, Position + Forward, Up); }
    }

    public Matrix4 ProjectionMatrix {
        get { return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(FieldOfView), (float)Engine.ClientSize.X / (float)Engine.ClientSize.Y, DepthNear, DepthFar); }
    }
}

public class FPSCamera : Camera {

    Vector2 LastMousePosition = Vector2.Zero;

    public float Speed = 1.5f;
    public float Sensitivity = 0.3f;

    public FPSCamera(Engine engine) : base(engine) {
        LastMousePosition = Engine.MousePosition;
    }

    public override void Update(double frameTime)
    {
        base.Update(frameTime);

        if (!Engine.IsFocused) return;

        KeyboardState input = Engine.KeyboardState;

        float DeltaSpeed = Speed * (float)frameTime;

        if (input.IsKeyDown(Keys.W)) {
            Position += Forward * DeltaSpeed;
        }

        if (input.IsKeyDown(Keys.S)) {
            Position -= Forward * DeltaSpeed;
        }

        if (input.IsKeyDown(Keys.A)) {
            Position -= Right * DeltaSpeed;
        }

        if (input.IsKeyDown(Keys.D)) {
            Position += Right * DeltaSpeed;
        }


        float deltaX = Engine.MousePosition.X - LastMousePosition.X;
        float deltaY = Engine.MousePosition.Y - LastMousePosition.Y;

        Rotation.Y += deltaX * Sensitivity;
        Rotation.X -= deltaY * Sensitivity;

        if (Rotation.X > 89f) {
            Rotation.X = 89f;
        }

        if (Rotation.X < -89f) {
            Rotation.X = -89f;
        }

        LastMousePosition = Engine.MousePosition;
    }
}