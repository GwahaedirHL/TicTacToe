public struct Cell
{
    public int X;
    public int Y;
    public Cell(int x, int y)
    {
        X = x;
        Y = y;
    }
    public static bool operator ==(Cell a, Cell b)
    {
        return a.X == b.X && a.Y == b.Y;
    }

    public static bool operator !=(Cell a, Cell b)
    {
        return a.X == b.X && a.Y == b.Y;
    }
}

