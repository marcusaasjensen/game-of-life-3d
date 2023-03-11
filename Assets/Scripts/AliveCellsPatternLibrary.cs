using System.Linq;
using UnityEngine;
public class AliveCellsPatternLibrary
{
    delegate Vector3Int[] PatternCoordinates(Vector3Int position);
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

    static readonly PatternCoordinates[] PatternCoordinatesList = { 
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
        Vector3Int[] patternCoordinates = GetPatternCoordinatesAtPosition(name, position);

        foreach (Vector3Int coordinate in patternCoordinates)
        { 
            Cell cell = GridCellManager.GetCellAtPosition(coordinate);
            if(cell) cell.Live();
        }
    }

    public static Vector3Int[] GetPatternCoordinatesAtPosition(AliveCellsPatternName name, Vector3Int position) => PatternCoordinatesList[(int)name](position);

    static Vector3Int[] CellCoordinates(Vector3Int position)
    {
        Vector3Int[] cellCoordinates = new Vector3Int[1];
        cellCoordinates[0] = position;
        return cellCoordinates;
    }

    static Vector3Int[] TubCoordinates(Vector3Int position)
    {
        Vector3Int[] cellCoordinates = new Vector3Int[4];

        int i = 0;

        cellCoordinates[i++] = new(position.x + 1, position.y, position.z);
        cellCoordinates[i++] = new(position.x - 1, position.y, position.z);
        cellCoordinates[i++] = new(position.x, position.y + 1, position.z);
        cellCoordinates[i++] = new(position.x, position.y - 1, position.z);

        return cellCoordinates;
    }

    static Vector3Int [] CubeCoordinates(Vector3Int position)
    {
        Vector3Int [] cellCoordinates = new Vector3Int[8];

        int i = 0;

        cellCoordinates[i++] = new(position.x, position.y, position.z);
        cellCoordinates[i++] = new(position.x + 1, position.y, position.z);
        cellCoordinates[i++] = new(position.x, position.y + 1, position.z);
        cellCoordinates[i++] = new(position.x + 1, position.y + 1, position.z);
        cellCoordinates[i++] = new(position.x, position.y, position.z + 1);
        cellCoordinates[i++] = new(position.x + 1, position.y, position.z + 1);
        cellCoordinates[i++] = new(position.x, position.y + 1, position.z + 1);
        cellCoordinates[i++] = new(position.x + 1, position.y + 1, position.z + 1);

        return cellCoordinates;
    }

    static Vector3Int [] TubeCoordinates(Vector3Int position) 
    {
        Vector3Int[] cellCoordinates = new Vector3Int[12];

        int i = 0;

        cellCoordinates[i++] = new(position.x + 2, position.y, position.z);
        cellCoordinates[i++] = new(position.x + 2, position.y - 1, position.z);
        cellCoordinates[i++] = new(position.x + 2, position.y + 1, position.z);

        cellCoordinates[i++] = new(position.x - 2, position.y, position.z);
        cellCoordinates[i++] = new(position.x - 2, position.y - 1, position.z);
        cellCoordinates[i++] = new(position.x - 2, position.y + 1, position.z);

        cellCoordinates[i++] = new(position.x, position.y + 2, position.z);
        cellCoordinates[i++] = new(position.x - 1, position.y + 2, position.z);
        cellCoordinates[i++] = new(position.x + 1, position.y + 2, position.z);

        cellCoordinates[i++] = new(position.x, position.y - 2, position.z);
        cellCoordinates[i++] = new(position.x - 1, position.y - 2, position.z);
        cellCoordinates[i++] = new(position.x + 1, position.y - 2, position.z);

        return cellCoordinates;
    }

    static Vector3Int[] BeehiveCoordinates(Vector3Int position)
    {
        Vector3Int[] cellCoordinates = new Vector3Int[6];

        int i = 0;

        cellCoordinates[i++] = new(position.x + 2, position.y, position.z);
        cellCoordinates[i++] = new(position.x - 1, position.y, position.z);
        cellCoordinates[i++] = new(position.x, position.y + 1, position.z);
        cellCoordinates[i++] = new(position.x, position.y - 1, position.z);
        cellCoordinates[i++] = new(position.x + 1, position.y + 1, position.z);
        cellCoordinates[i++] = new(position.x + 1, position.y - 1, position.z);

        return cellCoordinates;
    }

    static Vector3Int[] BoatCoordinates(Vector3Int position)
    {
        Vector3Int[] cellCoordinates = new Vector3Int[5];

        int i = 0;

        cellCoordinates[i++] = new(position.x + 1, position.y, position.z);
        cellCoordinates[i++] = new(position.x - 1, position.y, position.z);
        cellCoordinates[i++] = new(position.x, position.y + 1, position.z);
        cellCoordinates[i++] = new(position.x, position.y - 1, position.z);
        cellCoordinates[i++] = new(position.x - 1, position.y + 1, position.z);

        return cellCoordinates;
    }

    static Vector3Int[] LoafCoordinates(Vector3Int position)
    {
        Vector3Int[] cellCoordinates = new Vector3Int[7];

        int i = 0;

        cellCoordinates[i++] = new(position.x + 1, position.y, position.z);
        cellCoordinates[i++] = new(position.x + 1, position.y - 1, position.z);
        cellCoordinates[i++] = new(position.x, position.y + 1, position.z);
        cellCoordinates[i++] = new(position.x - 1, position.y + 1, position.z);
        cellCoordinates[i++] = new(position.x, position.y - 2, position.z);
        cellCoordinates[i++] = new(position.x - 1, position.y - 1, position.z);
        cellCoordinates[i++] = new(position.x - 2, position.y, position.z);

        return cellCoordinates;
    }

    static Vector3Int[] GliderCoordinates(Vector3Int position)
    {
        Vector3Int[] cellCoordinates = new Vector3Int[5];

        int i = 0;

        cellCoordinates[i++] = new(position.x + 1, position.y, position.z);
        cellCoordinates[i++] = new(position.x, position.y + 1, position.z);
        cellCoordinates[i++] = new(position.x, position.y - 1, position.z);
        cellCoordinates[i++] = new(position.x + 1, position.y - 1, position.z);
        cellCoordinates[i++] = new(position.x - 1, position.y - 1, position.z);

        return cellCoordinates;
    }


}
