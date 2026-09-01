using System;

namespace SerialTerminal.Core
{
    public enum Direction
    {
        None = 0,
        Rx = 1,
        Tx = 2,
        Info = 3
    }

    public enum DisplayMode
    {
        Text = 0,
        Hex = 1
    }

    /// <summary>One block of bytes as it crossed the wire, plus when it happened.</summary>
    public sealed class LogChunk
    {
        public readonly Direction Direction;
        public readonly byte[] Data;
        public readonly string Text;      // used by Direction.Info only
        public readonly DateTime Time;

        public LogChunk(Direction direction, byte[] data)
        {
            Direction = direction;
            Data = data;
            Text = null;
            Time = DateTime.Now;
        }

        public LogChunk(string info)
        {
            Direction = Direction.Info;
            Data = null;
            Text = info;
            Time = DateTime.Now;
        }
    }
}
