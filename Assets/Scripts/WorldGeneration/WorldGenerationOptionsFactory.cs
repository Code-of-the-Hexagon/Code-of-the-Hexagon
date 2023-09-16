
using System;
using UnityEngine;

public static class WorldGenerationOptionsFactory
{

    public static void AddShapeToHexGrid(
        this WorldGenerationOptions options,
        Shape shape,
        int shapeSize1,
        ArrayJoinTypes additionType,
        HexGridCoordinates shapePosition = null,
        int? shapeSize2 = null,
        bool inverted = false)
    {
        options.ShapeOptions.HexGridShape = 
            options.ShapeOptions.HexGridShape
                .AddShape(
                    shape,
                    shapeSize1,
                    additionType,
                    shapePosition,
                    shapeSize2,
                    inverted);
    }

    public static CoordinatesArray AddShape(
        this CoordinatesArray currentShape,
        Shape shape,
        int shapeSize1,
        ArrayJoinTypes additionType,
        HexGridCoordinates shapePosition = null,
        int? shapeSize2 = null,
        bool inverted = false)
    {
        CoordinatesArray shapeFunction = _ => false;

        shapePosition ??= new HexGridCoordinates(0, 0, 0);

        // You can experiment and add new shapes
        switch (shape)
        {
            // triangle edge is not equal to shapeSize1
            // TODO: fix triangle edge size
            case Shape.Triangle:
                shapeFunction = coordinates =>
                {
                    coordinates += -1 * shapePosition;
                    return coordinates.Q < shapeSize1 &&
                           coordinates.Q > -shapeSize1 / 2 &&
                           coordinates.R < shapeSize1 &&
                           coordinates.R > -shapeSize1 / 2 &&
                           coordinates.S < shapeSize1 &&
                           coordinates.S > -shapeSize1 / 2;
                };
                break;

            // rectangle shapes are different, they use both shapeSize variables
            // and their position is bottom left corner
            case Shape.Rectangle:
            {
                var sizeX = shapeSize1;

                // if sizeY is null, square shape is generated
                var sizeY = shapeSize2 ?? shapeSize1;

                shapeFunction = coordinates =>
                {
                    coordinates += -1 * shapePosition;

                    // four linear inequalities define a sizeX x sizeY rectangle
                    return coordinates.X < sizeX &&
                           coordinates.Y < sizeY &&
                           coordinates.X >= 0 &&
                           coordinates.Y >= 0;
                };
                break;
            }

            // creates a hexagon at ShapePosition with edge size of shapeSize1
            case Shape.Hexagon:
                shapeFunction = coordinates =>
                {
                    coordinates += -1 * shapePosition;

                    // this inequality produces a nice hexagon shape
                    return coordinates.Q > -shapeSize1 && coordinates.Q < shapeSize1 &&
                           coordinates.R > -shapeSize1 && coordinates.R < shapeSize1 &&
                           coordinates.S > -shapeSize1 && coordinates.S < shapeSize1;
                };
                break;

            case Shape.Star:
                shapeFunction = coordinates =>
                {
                    coordinates += -1 * shapePosition;

                    // this weird inequality produces funny star shape
                    return Math.Sqrt(Math.Abs(coordinates.Q)) +
                           Math.Sqrt(Math.Abs(coordinates.R)) +
                           Math.Sqrt(Math.Abs(coordinates.S)) <=
                           3 * Math.Sqrt(shapeSize1);
                };
                break;

            // circle radius is not equal to shapeSize1
            // TODO: fix circle radius size
            case Shape.Circle:
                shapeFunction = coordinates =>
                {
                    coordinates += -1 * shapePosition;

                    // circle inequality somehow works in QRS system as well
                    return Math.Pow(coordinates.Q, 2) +
                           Math.Pow(coordinates.R, 2) +
                           Math.Pow(coordinates.S, 2) <=
                           shapeSize1 * shapeSize1;
                };
                break;
        }

        // inverts the shape (everything except the shape)
        if (inverted)
        {
            var function = shapeFunction;
            shapeFunction = (coordinates) => !function(coordinates);
        }

        var currentShapeFunction = new CoordinatesArray(currentShape);

        // union of two sets
        if (additionType == ArrayJoinTypes.Union)
        {
            currentShape = (coordinates) => 
                currentShapeFunction(coordinates) || 
                shapeFunction(coordinates);
        }

        // intersection of two sets
        else if (additionType == ArrayJoinTypes.Intersection)
        {
            currentShape = (coordinates) => 
                currentShapeFunction(coordinates) && 
                shapeFunction(coordinates);
        }

        return currentShape;
    }
}
