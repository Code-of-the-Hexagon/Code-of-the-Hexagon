
using System;
using UnityEngine;

public static class WorldGenerationOptionsFactory
{
    // Čia nelabai gerai su tais shapeSize,
    // nes kai kuriems reikia 1 parametro,
    // kai kuriems 2, bet idk kaip geriau

    public static CoordinatesArray AddShape(
        this CoordinatesArray currentShape,
        Shape shape,
        int shapeSize1,
        ArrayJoinTypes additionType,
        int? shapeSize2 = null,
        bool inverted = false)
    {
        CoordinatesArray shapeFunction = _ => false;

        // Čia galima paeksperimentuot ir pridėt naujų shapes
        switch (shape)
        {
            case Shape.Rectangle:
            {
                var sizeX = shapeSize1;
                var sizeY = shapeSize2 ?? shapeSize1;

                shapeFunction = coordinates =>
                    coordinates.X < sizeX &&
                    coordinates.Y < sizeY &&
                    coordinates.X >= 0 &&
                    coordinates.Y >= 0;
                break;
            }
            case Shape.Hexagon:
                shapeFunction = coordinates =>
                    coordinates.Q > -shapeSize1 && coordinates.Q < shapeSize1 &&
                    coordinates.R > -shapeSize1 && coordinates.R < shapeSize1 &&
                    coordinates.S > -shapeSize1 && coordinates.S < shapeSize1;
                break;
            case Shape.Circle:
                shapeFunction = coordinates =>
                    coordinates.Q * coordinates.Q + coordinates.R * coordinates.R + coordinates.S * coordinates.S <=
                    shapeSize1 * shapeSize1;
                break;
            case Shape.Triangle:
                shapeFunction = coordinates =>
                    coordinates.Q < shapeSize1 * 2 &&
                    coordinates.Q > -shapeSize1 &&
                    coordinates.R < shapeSize1 * 2 &&
                    coordinates.R > -shapeSize1 &&
                    coordinates.S < shapeSize1 * 2 &&
                    coordinates.S > -shapeSize1;
                break;
            case Shape.Star:
                shapeFunction = coordinates =>
                    Math.Sqrt(Math.Abs(coordinates.Q)) + Math.Sqrt(Math.Abs(coordinates.R)) + Math.Sqrt(Math.Abs(coordinates.S)) <= 3 * Math.Sqrt(shapeSize1);
                break;
            default:
                shapeFunction = _ => false;
                break;
        }

        if (inverted)
        {
            var function = shapeFunction;
            shapeFunction = (coordinates) => !function(coordinates);
        }

        var currentShapeFunction = new CoordinatesArray(currentShape);

        if (additionType == ArrayJoinTypes.Union)
        {
            currentShape = (coordinates) => currentShapeFunction(coordinates) || shapeFunction(coordinates);
        }
        else if (additionType == ArrayJoinTypes.Intersection)
        {
            currentShape = (coordinates) => currentShapeFunction(coordinates) && shapeFunction(coordinates);
        }

        return currentShape;
    }
}
