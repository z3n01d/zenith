
using OpenTK.Mathematics;

namespace Program;

public class Program {
    static Engine engine;
    static Cube cube;
    static Cube floor;
    public static void Main(string[] args) {
        engine = new Engine(1280, 720, "test") {
            CursorState = OpenTK.Windowing.Common.CursorState.Grabbed
        };

        engine.OnUpdate += Update;

        cube = new Cube(engine)
        {
            Scale = (1, 2, 1),
            Rotation = (0,45,0),
            Position = (0,5,0)
        };

        floor = new Cube(engine)
        {
            Scale = (100f,0.01f,100f),
            Position = (0f,-10f,0f)
        };

        engine.Run();
    }

    public static void Update(double frameTime) {
        cube.Rotation.Y += (float)frameTime * 10f;
    }
}