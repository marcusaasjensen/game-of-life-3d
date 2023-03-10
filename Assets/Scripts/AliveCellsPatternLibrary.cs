using System.Linq;
using UnityEngine;
public class AliveCellsPatternLibrary
{
    delegate Vector3Int[] PatternCoordinates(Vector3Int position);
    public enum AliveCellsPatternName { Cell, Cube, Tub, Tube, Beehive, UpTetrisBlock }

    static readonly PatternCoordinates[] PatternCoordinatesList = { CellCoordinates, CubeCoordinates, TubCoordinates, TubeCoordinates, BeehiveCoordinates, UpTetrisBlockCoordinates };
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

        cellCoordinates[0] = new(position.x + 1, position.y, position.z);
        cellCoordinates[1] = new(position.x - 1, position.y, position.z);
        cellCoordinates[2] = new(position.x, position.y + 1, position.z);
        cellCoordinates[3] = new(position.x, position.y - 1, position.z);

        return cellCoordinates;
    }

    static Vector3Int [] CubeCoordinates(Vector3Int position)
    {
        Vector3Int [] cellCoordinates = new Vector3Int[8];

        cellCoordinates[0] = new(position.x, position.y, position.z);
        cellCoordinates[1] = new(position.x + 1, position.y, position.z);
        cellCoordinates[2] = new(position.x, position.y + 1, position.z);
        cellCoordinates[3] = new(position.x + 1, position.y + 1, position.z);
        cellCoordinates[4] = new(position.x, position.y, position.z + 1);
        cellCoordinates[5] = new(position.x + 1, position.y, position.z + 1);
        cellCoordinates[6] = new(position.x, position.y + 1, position.z + 1);
        cellCoordinates[7] = new(position.x + 1, position.y + 1, position.z + 1);

        return cellCoordinates;
    }

    static Vector3Int [] TubeCoordinates(Vector3Int position) 
    {
        Vector3Int[] cellCoordinates = new Vector3Int[12];
        
        cellCoordinates[0] = new(position.x + 2, position.y, position.z);
        cellCoordinates[1] = new(position.x + 2, position.y - 1, position.z);
        cellCoordinates[2] = new(position.x + 2, position.y + 1, position.z);

        cellCoordinates[3] = new(position.x - 2, position.y, position.z);
        cellCoordinates[4] = new(position.x - 2, position.y - 1, position.z);
        cellCoordinates[5] = new(position.x - 2, position.y + 1, position.z);

        cellCoordinates[6] = new(position.x, position.y + 2, position.z);
        cellCoordinates[7] = new(position.x - 1, position.y + 2, position.z);
        cellCoordinates[8] = new(position.x + 1, position.y + 2, position.z);

        cellCoordinates[9] = new(position.x, position.y - 2, position.z);
        cellCoordinates[10] = new(position.x - 1, position.y - 2, position.z);
        cellCoordinates[11] = new(position.x + 1, position.y - 2, position.z);

        return cellCoordinates;
    }

    static Vector3Int[] BeehiveCoordinates(Vector3Int position)
    {
        Vector3Int[] cellCoordinates = new Vector3Int[6];

        cellCoordinates[0] = new(position.x + 2, position.y, position.z);
        cellCoordinates[1] = new(position.x - 1, position.y, position.z);
        cellCoordinates[2] = new(position.x, position.y + 1, position.z);
        cellCoordinates[3] = new(position.x, position.y - 1, position.z);
        cellCoordinates[4] = new(position.x + 1, position.y + 1, position.z);
        cellCoordinates[5] = new(position.x + 1, position.y - 1, position.z);

        return cellCoordinates;
    }

    static Vector3Int[] UpTetrisBlockCoordinates(Vector3Int position)
    {
        Vector3Int[] cellCoordinates = new Vector3Int[4];

        cellCoordinates[0] = new(position.x, position.y, position.z);
        cellCoordinates[1] = new(position.x + 1, position.y, position.z);
        cellCoordinates[2] = new(position.x + 2, position.y, position.z);
        cellCoordinates[3] = new(position.x + 1, position.y + 1, position.z);

        return cellCoordinates;
    }
}
