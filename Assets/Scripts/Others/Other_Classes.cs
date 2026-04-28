using UnityEngine;

public class HexCell
{
    public int type;
    public Vector3Hex position;
    public Vector3Int cellPosition;
    public Vector3 worldPosition;
    
    public HexCell(Vector3Hex pos, Vector3Int cellPos, Vector3 worldPos, int type = -1)
    {
        this.type = type;
        this.position = pos;
        this.cellPosition = cellPos;
        this.worldPosition = worldPos;
    }
}