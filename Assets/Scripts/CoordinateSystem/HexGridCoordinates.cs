using System;

// HexGridCoordinates contains coordinates
// in QRS (cube) or in XY (offset) format
public class HexGridCoordinates
{
    public readonly int Q;
    public readonly int R;
    public readonly int S;

    public int X => Q + (R - (R & 1)) / 2;

    public int Y => R;

    public HexGridCoordinates(HexGridCoordinates other)
    {
        Q = other.Q;
        R = other.R;
        S = other.S;
    }

    public HexGridCoordinates(int q, int r, int s)
    {
        (Q, R, S) = (q, r, s);
        Validate();
    }

    public HexGridCoordinates(int x, int y)
    {
        Q = x - (y - (y & 1)) / 2;
        R = y;
        S = -Q - R;
    }

    public static HexGridCoordinates operator +(HexGridCoordinates a, HexGridCoordinates b) =>
        new(a.Q + b.Q, a.R + b.R, a.S + b.S);

    public static HexGridCoordinates operator *(int a, HexGridCoordinates b) =>
        new(a * b.Q, a * b.R, a * b.S);

    public override string ToString() =>
        $"(Q = {Q}, R = {R}, S = {S}) - (X = {X}, Y = {Y})";

    public void Validate()
    {
        if (Q + R + S != 0)
        {
            throw new ArgumentException($"Hex coordinates {ToString()} are not valid");
        }
    }

    public override bool Equals(object obj)
    {
        if (obj is not HexGridCoordinates coordinates) return false;
        return Q == coordinates.Q && R == coordinates.R;
    }

    public override int GetHashCode() =>
        HashCode.Combine(Q, R, S);
}
