
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Program;

public class Window : GameWindow {

    private static DebugProc DebugMessageDelegate = OnDebugMessage;
    public float Time;

    public Window(int width, int height, string title) : base(
        GameWindowSettings.Default,
        new NativeWindowSettings() {
            Title = title,
            ClientSize = (width,height),
            MaximumClientSize = (width,height),
            MinimumClientSize = (width,height),
            Flags = ContextFlags.Default,
            Profile = ContextProfile.Core,
            APIVersion = new Version(4,6)

        }
    ) {}

    protected override void OnLoad() {
        base.OnLoad();
        GL.DebugMessageCallback(DebugMessageDelegate, IntPtr.Zero);
        GL.Enable(EnableCap.DebugOutput);
        Loaded();
    }

    protected override void OnUnload()
    {
        base.OnUnload();
        Unloaded();
    }

    protected override void OnUpdateFrame(FrameEventArgs args) {
        base.OnUpdateFrame(args);

        Time = (float)args.Time;

        if (KeyboardState.IsKeyDown(Keys.Escape)) {
            Close();
        }

        Update(args.Time);
    }

    protected override void OnRenderFrame(FrameEventArgs args) {
        base.OnRenderFrame(args);
        Render();
    }

    public virtual void Loaded() {
        
    }

    public virtual void Unloaded() {

    }

    public virtual void Update(double frameTime) {

    }

    public virtual void Render() {

    }

    private static void OnDebugMessage(
        DebugSource source,     // Source of the debugging message.
        DebugType type,         // Type of the debugging message.
        int id,                 // ID associated with the message.
        DebugSeverity severity, // Severity of the message.
        int length,             // Length of the string in pMessage.
        IntPtr pMessage,        // Pointer to message string.
        IntPtr pUserParam)      // The pointer you gave to OpenGL, explained later.
    {
        // In order to access the string pointed to by pMessage, you can use Marshal
        // class to copy its contents to a C# string without unsafe code. You can
        // also use the new function Marshal.PtrToStringUTF8 since .NET Core 1.1.
        string message = Marshal.PtrToStringAnsi(pMessage, length);

        // The rest of the function is up to you to implement, however a debug output
        // is always useful.
        Console.WriteLine("[{0} source={1} type={2} id={3}] {4}", severity, source, type, id, message);

        // Potentially, you may want to throw from the function for certain severity
        // messages.
        if (type == DebugType.DebugTypeError)
        {
            throw new Exception(message);
        }
    }
}