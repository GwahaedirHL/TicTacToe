using System;

public struct Cell : IEquatable<Cell>
{
    public int X;
    public int Y;

    public Cell(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool Equals(Cell other) => X == other.X && Y == other.Y;
    public override bool Equals(object obj) => obj is Cell other && Equals(other);
    public override int GetHashCode() { unchecked { return (X * 397) ^ Y; } }

    public static bool operator ==(Cell a, Cell b) => a.Equals(b);
    public static bool operator !=(Cell a, Cell b) => !a.Equals(b);
}
