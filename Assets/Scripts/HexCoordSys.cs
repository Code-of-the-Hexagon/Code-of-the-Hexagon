using System;
using UnityEngine;
using UnityEngine.UIElements;

public class HexCoordinateSystem
{
    private readonly double _sqrt3 = System.Math.Sqrt(3);
    public static CubeCoordinates[] DirectionVectorListTuple { get; } = new CubeCoordinates[6] // cube direction vector list for q,r,s coordinate system
    {
        new CubeCoordinates(1, 0, -1),
        new CubeCoordinates(1, -1, 0),
        new CubeCoordinates(0, -1, 1),
        new CubeCoordinates(-1, 0, 1),
        new CubeCoordinates(-1, 1, 0),
        new CubeCoordinates(0, 1, -1)
    };

    public Vector3 GetXYCoordinates(CubeCoordinates coordinates)
    {
        return new Vector3(
            ((coordinates.Q * (float)_sqrt3 + coordinates.R * (float)_sqrt3) * 2f),
            0,
            (3f / 2f * coordinates.S));
    }
    private CubeCoordinates GetDirection(int direction) // retrieves direction vector.     0 is +q -s (right)      1 is +q -r (top right)   and so on counter clockwise.
    {
        return DirectionVectorListTuple[direction];
    }
    private CubeCoordinates AddVector(CubeCoordinates vector, CubeCoordinates add) // adds vector to coordinate or vector and returns coordinate or vector 
    {
        return new CubeCoordinates( vector.Q + add.Q, 
                 vector.R + add.R, 
                 vector.S + add.S );
    }
    private CubeCoordinates SubtractVector(CubeCoordinates vectorA, CubeCoordinates vectorB) // subtracts two vectors
    {
        return new CubeCoordinates( vectorA.Q - vectorB.Q, 
                 vectorA.R - vectorB.R, 
                 vectorA.S - vectorB.S );
    }

    private CubeCoordinates GetNeighbor(int direction, CubeCoordinates hex) // gets coordinates of neighbor of the specified direction
    {
        return AddVector(GetDirection(direction), hex);
    }

    public int GetDistance(CubeCoordinates vectorA, CubeCoordinates vectorB)
    {
        var vector = SubtractVector(vectorA, vectorB);
        return (System.Math.Abs(vector.Q) + System.Math.Abs(vector.R) + System.Math.Abs(vector.S)) / 2;
    }
}