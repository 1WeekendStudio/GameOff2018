using UnityEngine;

public struct Position
{
    public static readonly Position Invalid = new Position(-1, -1);

    public int X;
    public int Y;

    public Position(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public static Position Random(int maxX, int maxY)
    {
        return new Position(UnityEngine.Random.Range(0, maxX), UnityEngine.Random.Range(0, maxY));
    }

    public static explicit operator Vector2(Position position)
    {
        return new Vector2(position.X, position.Y);
    }

    public override string ToString()
    {
        return $"({this.X},{this.Y})";
    }
}
