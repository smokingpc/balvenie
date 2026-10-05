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
        public readonly Direction _Direction;
        public readonly byte[] _Data;
        public readonly string _Text;      // used by Direction.Info only
        public readonly DateTime _Time;

        public LogChunk(Direction direction, byte[] data)
        {
            _Direction = direction;
            _Data = data;
            _Text = null;
            _Time = DateTime.Now;
        }

        public LogChunk(string info)
        {
            _Direction = Direction.Info;
            _Data = null;
            _Text = info;
            _Time = DateTime.Now;
        }
    }
}
