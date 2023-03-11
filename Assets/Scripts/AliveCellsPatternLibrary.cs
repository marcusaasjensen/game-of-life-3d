using System.Collections.Generic;
using UnityEngine;
public abstract class AliveCellsPatternLibrary
{
    private delegate Vector3Int[] PatternCoordinates(Vector3Int position);
    public enum AliveCellsPatternName {
        Cell, 
        Cube, 
        Tub, 
        Tube, 
        Beehive, 
        Boat, 
        Loaf,
        Glider    
    }

    private static readonly PatternCoordinates[] PatternCoordinatesList = { 
        CellCoordinates, 
        CubeCoordinates, 
        TubCoordinates, 
        TubeCoordinates, 
        BeehiveCoordinates, 
        BoatCoordinates, 
        LoafCoordinates, 
        GliderCoordinates 
    };
    public static void SetAliveCellsPattern(AliveCellsPatternName name, Vector3Int position) 
    { 
        var patternCoordinates = GetPatternCoordinatesAtPosition(name, position);

        foreach (var coordinate in patternCoordinates)
        { 
            var cell = GridCellManager.GetCellAtPosition(coordinate);
            if(cell) cell.Live();
        }
    }

    public static IEnumerable<Vector3Int> GetPatternCoordinatesAtPosition(AliveCellsPatternName name, Vector3Int position) => PatternCoordinatesList[(int)name](position);

    private static Vector3Int[] CellCoordinates(Vector3Int position)
    {
        var cellCoordinates = new Vector3Int[1];
        cellCoordinates[0] = position;
        return cellCoordinates;
    }

    private static Vector3Int[] TubCoordinates(Vector3Int position)
    {
        var cellCoordinates = new Vector3Int[4];

        var i = 0;

        cellCoordinates[i++] = new Vector3Int(position.x + 1, position.y, position.z);
        cellCoordinates[i++] = new Vector3Int(position.x - 1, position.y, position.z);
        cellCoordinates[i++] = new Vector3Int(position.x, position.y + 1, position.z);
        cellCoordinates[i] = new Vector3Int(position.x, position.y - 1, position.z);

        return cellCoordinates;
    }

    private static Vector3Int[] CubeCoordinates(Vector3Int position)
    {
        var cellCoordinates = new Vector3Int[8];

        var i = 0;

        cellCoordinates[i++] = new Vector3Int(position.x, position.y, position.z);
        cellCoordinates[i++] = new Vector3Int(position.x + 1, position.y, position.z);
        cellCoordinates[i++] = new Vector3Int(position.x, position.y + 1, position.z);
        cellCoordinates[i++] = new Vector3Int(position.x + 1, position.y + 1, position.z);
        cellCoordinates[i++] = new Vector3Int(position.x, position.y, position.z + 1);
        cellCoordinates[i++] = new Vector3Int(position.x + 1, position.y, position.z + 1);
        cellCoordinates[i++] = new Vector3Int(position.x, position.y + 1, position.z + 1);
        cellCoordinates[i] = new Vector3Int(position.x + 1, position.y + 1, position.z + 1);

        return cellCoordinates;
    }

    private static Vector3Int[] TubeCoordinates(Vector3Int position) 
    {
        var cellCoordinates = new Vector3Int[12];

        var i = 0;

        cellCoordinates[i++] = new Vector3Int(position.x + 2, position.y, position.z);
        cellCoordinates[i++] = new Vector3Int(position.x + 2, position.y - 1, position.z);
        cellCoordinates[i++] = new Vector3Int(position.x + 2, position.y + 1, position.z);

        cellCoordinates[i++] = new Vector3Int(position.x - 2, position.y, position.z);
        cellCoordinates[i++] = new Vector3Int(position.x - 2, position.y - 1, position.z);
        cellCoordinates[i++] = new Vector3Int(position.x - 2, position.y + 1, position.z);

        cellCoordinates[i++] = new Vector3Int(position.x, position.y + 2, position.z);
        cellCoordinates[i++] = new Vector3Int(position.x - 1, position.y + 2, position.z);
        cellCoordinates[i++] = new Vector3Int(position.x + 1, position.y + 2, position.z);

        cellCoordinates[i++] = new Vector3Int(position.x, position.y - 2, position.z);
        cellCoordinates[i++] = new Vector3Int(position.x - 1, position.y - 2, position.z);
        cellCoordinates[i] = new Vector3Int(position.x + 1, position.y - 2, position.z);

        return cellCoordinates;
    }

    private static Vector3Int[] BeehiveCoordinates(Vector3Int position)
    {
        var cellCoordinates = new Vector3Int[6];

        var i = 0;

        cellCoordinates[i++] = new Vector3Int(position.x + 2, position.y, position.z);
        cellCoordinates[i++] = new Vector3Int(position.x - 1, position.y, position.z);
        cellCoordinates[i++] = new Vector3Int(position.x, position.y + 1, position.z);
        cellCoordinates[i++] = new Vector3Int(position.x, position.y - 1, position.z);
        cellCoordinates[i++] = new Vector3Int(position.x + 1, position.y + 1, position.z);
        cellCoordinates[i] = new Vector3Int(position.x + 1, position.y - 1, position.z);

        return cellCoordinates;
    }

    private static Vector3Int[] BoatCoordinates(Vector3Int position)
    {
        var cellCoordinates = new Vector3Int[5];

        var i = 0;

        cellCoordinates[i++] = new Vector3Int(position.x + 1, position.y, position.z);
        cellCoordinates[i++] = new Vector3Int(position.x - 1, position.y, position.z);
        cellCoordinates[i++] = new Vector3Int(position.x, position.y + 1, position.z);
        cellCoordinates[i++] = new Vector3Int(position.x, position.y - 1, position.z);
        cellCoordinates[i] = new Vector3Int(position.x - 1, position.y + 1, position.z);

        return cellCoordinates;
    }

    private static Vector3Int[] LoafCoordinates(Vector3Int position)
    {
        var cellCoordinates = new Vector3Int[7];

        var i = 0;

        cellCoordinates[i++] = new Vector3Int(position.x + 1, position.y, position.z);
        cellCoordinates[i++] = new Vector3Int(position.x + 1, position.y - 1, position.z);
        cellCoordinates[i++] = new Vector3Int(position.x, position.y + 1, position.z);
        cellCoordinates[i++] = new Vector3Int(position.x - 1, position.y + 1, position.z);
        cellCoordinates[i++] = new Vector3Int(position.x, position.y - 2, position.z);
        cellCoordinates[i++] = new Vector3Int(position.x - 1, position.y - 1, position.z);
        cellCoordinates[i] = new Vector3Int(position.x - 2, position.y, position.z);

        return cellCoordinates;
    }

    private static Vector3Int[] GliderCoordinates(Vector3Int position)
    {
        var cellCoordinates = new Vector3Int[5];

        var i = 0;

        cellCoordinates[i++] = new Vector3Int(position.x + 1, position.y, position.z);
        cellCoordinates[i++] = new Vector3Int(position.x, position.y + 1, position.z);
        cellCoordinates[i++] = new Vector3Int(position.x, position.y - 1, position.z);
        cellCoordinates[i++] = new Vector3Int(position.x + 1, position.y - 1, position.z);
        cellCoordinates[i] = new Vector3Int(position.x - 1, position.y - 1, position.z);

        return cellCoordinates;
    }


}
