using UnityEngine;
public class AliveCellsPatternLibrary
{
    public delegate void CreatePattern(Vector3Int position);
    public enum AliveCellsPatternName { Block3D, Tub, Tube, UpTetrisBlock }

    static readonly CreatePattern[] createPatternFunctions = { CreateBlock3D, CreateTub, CreateTube, CreateUpTetrisBlock };
    public static void CreateNewPattern(AliveCellsPatternName name, Vector3Int position) => createPatternFunctions[(int)name](position);

    static void CreateBlock3D(Vector3Int position)
    {
        GridCellManager.GetCellAtPosition(position)?.Live();
        GridCellManager.GetCellAtPosition(new(position.x + 1, position.y, position.z))?.Live();
        GridCellManager.GetCellAtPosition(new(position.x, position.y + 1, position.z))?.Live();
        GridCellManager.GetCellAtPosition(new(position.x + 1, position.y + 1, position.z))?.Live();
        GridCellManager.GetCellAtPosition(new(position.x, position.y, position.z + 1))?.Live();
        GridCellManager.GetCellAtPosition(new(position.x + 1, position.y, position.z + 1))?.Live();
        GridCellManager.GetCellAtPosition(new(position.x, position.y + 1, position.z + 1))?.Live();
        GridCellManager.GetCellAtPosition(new(position.x + 1, position.y + 1, position.z + 1))?.Live();
    }

    static void CreateTub(Vector3Int position)
    {
        GridCellManager.GetCellAtPosition(new(position.x + 1, position.y, position.z))?.Live();
        GridCellManager.GetCellAtPosition(new(position.x - 1, position.y, position.z))?.Live();
        GridCellManager.GetCellAtPosition(new(position.x, position.y + 1, position.z))?.Live();
        GridCellManager.GetCellAtPosition(new(position.x, position.y - 1, position.z))?.Live();
    }

    static void CreateTube(Vector3Int position)
    {
        GridCellManager.GetCellAtPosition(new(position.x + 2, position.y, position.z))?.Live();
        GridCellManager.GetCellAtPosition(new(position.x + 2, position.y - 1, position.z))?.Live();
        GridCellManager.GetCellAtPosition(new(position.x + 2, position.y + 1, position.z))?.Live();

        GridCellManager.GetCellAtPosition(new(position.x - 2, position.y, position.z))?.Live();
        GridCellManager.GetCellAtPosition(new(position.x - 2, position.y - 1, position.z))?.Live();
        GridCellManager.GetCellAtPosition(new(position.x - 2, position.y + 1, position.z))?.Live();

        GridCellManager.GetCellAtPosition(new(position.x, position.y + 2, position.z))?.Live();
        GridCellManager.GetCellAtPosition(new(position.x - 1, position.y + 2, position.z))?.Live();
        GridCellManager.GetCellAtPosition(new(position.x + 1, position.y + 2, position.z))?.Live();

        GridCellManager.GetCellAtPosition(new(position.x, position.y - 2, position.z))?.Live();
        GridCellManager.GetCellAtPosition(new(position.x - 1, position.y - 2, position.z))?.Live();
        GridCellManager.GetCellAtPosition(new(position.x + 1, position.y - 2, position.z))?.Live();
    }

    static void CreateUpTetrisBlock(Vector3Int position)
    {
        GridCellManager.GetCellAtPosition(new(position.x, position.y, position.z))?.Live();
        GridCellManager.GetCellAtPosition(new(position.x + 1, position.y, position.z))?.Live();
        GridCellManager.GetCellAtPosition(new(position.x + 2, position.y, position.z))?.Live();
        GridCellManager.GetCellAtPosition(new(position.x + 1, position.y + 1, position.z))?.Live();
    }

    static void CreateBeehive(Vector3Int position)
    {
        GridCellManager.GetCellAtPosition(new(position.x, position.y, position.z))?.Live();
        GridCellManager.GetCellAtPosition(new(position.x + 1, position.y, position.z))?.Live();
        GridCellManager.GetCellAtPosition(new(position.x + 2, position.y, position.z))?.Live();
        GridCellManager.GetCellAtPosition(new(position.x + 1, position.y + 1, position.z))?.Live();
    }
}
