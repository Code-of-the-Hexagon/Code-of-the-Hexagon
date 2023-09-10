using System;
using UnityEngine;
using UnityEngine.UIElements;

public class HexCoordinateSystem
{
    private readonly double sqrt3 = System.Math.Sqrt(3);
    public static (int q, int r, int s)[] DirectionVectorListTuple { get; } = new (int q, int r, int s)[6] // cube direction vector list for q,r,s coordinate system
    {
        (1, 0, -1),
        (1, -1, 0),
        (0, -1, 1),
        (-1, 0, 1),
        (-1, 1, 0),
        (0, 1, -1)
    };

    public (float x, float y) GetXYCoordinates((int q, int r, int s) coordinates)
    {
        return ((coordinates.q * (float)sqrt3 + coordinates.r * (float)sqrt3 / 2f)
            , (3f / 2f * coordinates.r));
    }
    private (int q, int r, int s) GetDirection(int direction) // retrieves direction vector.     0 is +q -s (right)      1 is +q -r (top right)   and so on counter clockwise.
    {
        return DirectionVectorListTuple[direction];
    }
    private (int q, int r, int s) AddVector((int q,int r,int s) vector, (int q, int r, int s) add) // adds vector to coordinate or vector and returns coordinate or vector 
    {
        return ( vector.q + add.q, 
                 vector.r + add.r, 
                 vector.s + add.s );
    }
    private (int q, int r, int s) SubtractVector((int q, int r, int s) vectorA, (int q, int r, int s) vectorB) // subtracts two vectors
    {
        return ( vectorA.q - vectorB.q, 
                 vectorA.r - vectorB.r, 
                 vectorA.s - vectorB.s );
    }

    private (int q, int r, int s) GetNeighbor(int direction, (int q, int r, int s) hex) // gets coordinates of neighbor of the specified direction
    {
        return AddVector(GetDirection(direction), hex);
    }

    public int GetDistance((int q, int r, int s) vectorA, (int q, int r, int s) vectorB)
    {
        var vector = SubtractVector(vectorA, vectorB);
        return (System.Math.Abs(vector.q) + System.Math.Abs(vector.r) + System.Math.Abs(vector.s)) / 2;
    }
}