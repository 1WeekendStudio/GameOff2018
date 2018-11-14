﻿using UnityEngine;

public struct Position
{
    public int X;
    public int Y;

    public Position(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public static explicit operator Vector2(Position position)
    {
        return new Vector2(position.X, position.Y);
    }
}