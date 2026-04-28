using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Grid))]
public class GridManager : Singleton<GridManager>
{
    public int hexBoundAmount = 6;
    public GameObject Test;
    
    private Grid _grid;
    private Dictionary<Vector3Hex, HexCell> _hexMap;

    protected override void Awake()
    {
        base.Awake();
        _grid = GetComponent<Grid>();
        
    }

    private void Start()
    {
        GenerateHexMap();
    }

    public Grid GetGrid() => _grid;

    public Vector3Hex WorldToHex(Vector3 worldPos)
    {
        Vector3Hex hexPos = new Vector3Hex(0, 0, 0);
        Vector3Int cellPos = _grid.WorldToCell(worldPos);
        hexPos.q = cellPos.y;
        hexPos.r = cellPos.x - (cellPos.y - (cellPos.y & 1)) / 2;
        hexPos.s = -hexPos.q - hexPos.r;

        return IsHexInBound(hexPos) 
            ? hexPos 
            : new Vector3Hex(-1, -1, -1);
    }

    public Vector3 HexToWorld(Vector3Hex hexPos)
    {
        Vector3Int cellPos = new Vector3Int();
        cellPos.y = hexPos.q;
        cellPos.x = hexPos.r + (hexPos.q - (hexPos.q & 1)) / 2;
        return _grid.CellToWorld(cellPos);;
    }
    
    public HexCell PositionToCell(Vector3Hex hexPos)
    {
        return _hexMap.ContainsKey(hexPos) ? _hexMap[hexPos] : null;
    }

    public bool IsHexInBound(Vector3Hex hexPos)
    {
        return Math.Abs(hexPos.q) <= hexBoundAmount && 
               Math.Abs(hexPos.r) <= hexBoundAmount && 
               Math.Abs(hexPos.s) <= hexBoundAmount;
    }

    private void GenerateHexMap()
    {
        _hexMap = new Dictionary<Vector3Hex, HexCell>();
        for (int q = -hexBoundAmount; q <= hexBoundAmount; q++)
        {
            for (int r = -hexBoundAmount; r <= hexBoundAmount; r++)
            {
                for (int s = -hexBoundAmount; s <= hexBoundAmount; s++)
                {
                    if (q + r + s != 0) continue;
                    Vector3Hex hexPos = new Vector3Hex(q, r, s);
                    Vector3 worldPos = HexToWorld(hexPos);
                    Vector3Int cellPos = _grid.WorldToCell(worldPos);
                    
                    _hexMap.Add(hexPos, new HexCell(hexPos, cellPos, worldPos, 0));

                    
                }
            }
        }
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mouseScreen = Mouse.current.position.ReadValue();
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreen.x, mouseScreen.y, 0f));
            worldPos.z = 0f;
            Vector3Hex hexPos = WorldToHex(worldPos);
            
            HexCell hexCell = PositionToCell(hexPos);
            if (hexCell != null && hexCell.type == 0)
            {
                hexCell.type = 1;
                GameObject test = Instantiate(Test, hexCell.worldPosition, Quaternion.identity);
                
                HexCell testCell = PositionToCell(hexPos);
                
                
                Debug.Log(testCell.type);
            }
            
        }
    }
    
}
