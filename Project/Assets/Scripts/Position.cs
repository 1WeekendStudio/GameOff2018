using System;

using UnityEngine;

public struct Position : IEquatable<Position>
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

    public static bool operator ==(Position left, Position right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Position left, Position right)
    {
        return !left.Equals(right);
    }

    public override string ToString()
    {
        return $"({this.X},{this.Y})";
    }

    public bool Equals(Position other)
    {
        return this.X == other.X && this.Y == other.Y;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        return obj is Position && this.Equals((Position)obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (this.X * 397) ^ this.Y;
        }
    }
}
