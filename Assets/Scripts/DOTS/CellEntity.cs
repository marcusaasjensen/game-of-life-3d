using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public struct CellEntity : IComponentData
{
    public bool IsAlive { get; set; }
    public int3 Position;
}

public class CellBaker : Baker<CellAuthoring>
{
    public override void Bake(CellAuthoring authoring)
    {
        var transform = GetComponent<Transform>();
        AddComponent(new CellEntity
        {
            IsAlive = authoring.IsAlive,
            Position = new int3(transform.position)
        });
    }
}