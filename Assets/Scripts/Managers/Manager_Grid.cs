using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Managers
{
    [RequireComponent(typeof(Grid))]
    public class GridManager : Singleton <GridManager>
    {
        public int hexBoundAmount = 6;

        public static UnityAction<int> OnNodePlaced;

        private Grid _grid;
        private Dictionary<Vector3Int, HexNode> _cellMap = new Dictionary<Vector3Int, HexNode>();

        private readonly Vector3Int[] _ignorePositions = new[] 
        {
            new Vector3Int(5, -5, 0),
            new Vector3Int(5, -4, 0),
            new Vector3Int(5, -3, 0),
            new Vector3Int(5, -2, 0),
            new Vector3Int(5, -1, 0),
            new Vector3Int(5, 1, 0),
            new Vector3Int(5, 2, 0),
            new Vector3Int(5, 3, 0),
            new Vector3Int(5, 4, 0),
            new Vector3Int(5, 5, 0),
            new Vector3Int(4, -5, 0),
            new Vector3Int(4, -4, 0),
            new Vector3Int(4, -3, 0),
            new Vector3Int(4, 3, 0),
            new Vector3Int(4, 4, 0),
            new Vector3Int(4, 5, 0),
            new Vector3Int(3, -5, 0),
            new Vector3Int(3, 5, 0),
            new Vector3Int(-4, -5, 0),
            new Vector3Int(-4, -4, 0),
            new Vector3Int(-4, 4, 0),
            new Vector3Int(-4, 5, 0),
            new Vector3Int(-5, -5, 0),
            new Vector3Int(-5, -4, 0),
            new Vector3Int(-5, -3, 0),
            new Vector3Int(-5, -2, 0),
            new Vector3Int(-5, 2, 0),
            new Vector3Int(-5, 3, 0),
            new Vector3Int(-5, 4, 0),
            new Vector3Int(-5, 5, 0),
        };

        protected override void Awake()
        {
            base.Awake();
            _grid = GetComponent<Grid>();
        }
        
        public Grid GetGrid() => _grid;
        
        public Vector3 CellToWorld(Vector3Int cellPos)
        { return _grid.CellToWorld(cellPos); }
        
        public Vector3Int WorldToCell(Vector3 worldPos)
        { return _grid.WorldToCell(worldPos); }
        
        public bool IsEmptyCell(Vector3Int cellPos)
        { return !_cellMap.ContainsKey(cellPos); }

        public bool IsCellInBound(Vector3Int cellPos)
        {
            return Math.Abs(cellPos.x) <= hexBoundAmount &&
                   Math.Abs(cellPos.y) <= hexBoundAmount &&
                   !_ignorePositions.Contains(cellPos);
        }
        
        public bool AddToMap(Vector3Int cellPos, HexNode hexNode)
        {
            if(!_cellMap.ContainsKey(cellPos))
            {
                _cellMap.Add(cellPos, hexNode);
                return true;
            }
            return false;
        }

        public bool RemoveFromMap(Vector3Int cellPos)
        { return _cellMap.Remove(cellPos); }
    }
}

