using System;

namespace Features.Input.App
{
    /// <summary>
    /// Commands received from a physical input, used as a mask in case of multiple buttons pressed
    /// </summary>
    [Flags]
    public enum InboundCommands
    {
        None = 0,
        Left = 1,
        Right = 2,
        Rotate = 4,
        Down = 8,
    }
}