using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class Other_HNGD_Generator : MonoBehaviour
{
    public Grid grid;
    public Tilemap tilemap;
    public int hexBoundAmount = 5;
    public List<String> ignoreTileNames;
    public List<Vector3> tilePositions;
    public String createDataDir = "Assets/Datas/HexNodeGroups";

    [ContextMenu("Caluclate")]
    public void CalculatePositions()
    {
        tilePositions.Clear();
        for (int x = -hexBoundAmount; x <= hexBoundAmount; x++)
        {
            for (int y = -hexBoundAmount; y <= hexBoundAmount; y++)
            {
                TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                if (tile != null && !ignoreTileNames.Contains(tile.name))
                {
                    Debug.Log($"Tile: {tile.name} | Position x:{x} y:{y}");
                    tilePositions.Add(grid.CellToWorld(new Vector3Int(x, y, 0)));
                }
            }
        }
    }

    [ContextMenu("Generate")]
    public void Generate()
    {
        CalculatePositions();
        
        int index = 0;
        while (System.IO.File.Exists($"{createDataDir}/HNGD_{index}.asset"))
        { index++; }
        
        var so = ScriptableObject.CreateInstance<HNGData>();
        so.tilePositions = tilePositions;
        
        AssetDatabase.CreateAsset(so, $"{createDataDir}/HNGD_{index}.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        
        Debug.Log($"Generated Data [{index}] at {createDataDir}/HNGD_{index}.asset");
    }
}
