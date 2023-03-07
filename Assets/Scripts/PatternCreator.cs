using UnityEngine;

public class PatternCreator
{
    public delegate void CreatePattern(Vector3Int position);
    public enum AliveCellsPatternName { Block3D, Tub3D, UpTetrisBlock }

    static readonly CreatePattern[] createPatternFunctions = { CreateBlock3D, CreateTub3D, CreateUpTetrisBlock };
    public static void CreateNewPattern(AliveCellsPatternName name, Vector3Int position) => createPatternFunctions[(int)name](position);

    static void CreateBlock3D(Vector3Int position)
    {
        CellManager.GetCellAtPosition(position)?.Live();
        CellManager.GetCellAtPosition(new(position.x + 1, position.y, position.z))?.Live();
        CellManager.GetCellAtPosition(new(position.x, position.y + 1, position.z))?.Live();
        CellManager.GetCellAtPosition(new(position.x + 1, position.y + 1, position.z))?.Live();
        CellManager.GetCellAtPosition(new(position.x, position.y, position.z + 1))?.Live();
        CellManager.GetCellAtPosition(new(position.x + 1, position.y, position.z + 1))?.Live();
        CellManager.GetCellAtPosition(new(position.x, position.y + 1, position.z + 1))?.Live();
        CellManager.GetCellAtPosition(new(position.x + 1, position.y + 1, position.z + 1))?.Live();
    }

    static void CreateTub3D(Vector3Int position)
    {
        CellManager.GetCellAtPosition(new(position.x + 1, position.y + 1, position.z))?.Live();
        CellManager.GetCellAtPosition(new(position.x + 1, position.y, position.z + 1))?.Live();
        CellManager.GetCellAtPosition(new(position.x, position.y + 1, position.z + 1))?.Live();
        CellManager.GetCellAtPosition(new(position.x + 2, position.y + 1, position.z + 1))?.Live();
        CellManager.GetCellAtPosition(new(position.x + 1, position.y + 1, position.z + 2))?.Live();
        CellManager.GetCellAtPosition(new(position.x + 1, position.y + 2, position.z + 1))?.Live();
    }

    static void CreateUpTetrisBlock(Vector3Int position)
    {
        CellManager.GetCellAtPosition(new(position.x, position.y, position.z))?.Live();
        CellManager.GetCellAtPosition(new(position.x + 1, position.y, position.z))?.Live();
        CellManager.GetCellAtPosition(new(position.x + 2, position.y, position.z))?.Live();
        CellManager.GetCellAtPosition(new(position.x + 1, position.y + 1, position.z))?.Live();
    }


}
