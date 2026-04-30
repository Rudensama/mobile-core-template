using System;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class SlotManager : Singleton<SlotManager>
    {
        public GameObject hexNodeGroupPrefab;
        public UnityAction OnSlotRefreshed;
        public UnityAction<int> OnSlotFreed;
        
        [SerializeField] private Vector3Int[] slotCellPositions = new Vector3Int[3];
        [SerializeField] private Vector3[] _slotPositions =  new Vector3[3];
        private HexNodeGroup[] _hexNodeGroups = new HexNodeGroup[3];
        
        public Vector3 GetSlotPosition(int slotIndex) => _slotPositions[slotIndex];

        private void OnEnable()
        {
            GridManager.OnNodePlaced += OnNodePlaced;
        }

        private void OnDisable()
        {
            GridManager.OnNodePlaced -= OnNodePlaced;
        }

        private void Start()
        {
            for (int i = 0; i < slotCellPositions.Length; i++)
            { _slotPositions[i] = GridManager.Instance.GetGrid().CellToWorld(slotCellPositions[i]);}
            
            RefreshSlots();
        }

        private void RefreshSlots()
        {
            for (int i = 0; i < _slotPositions.Length; i++)
            { SpawnNodeGroup(_slotPositions[i], i); }
            
            OnSlotRefreshed?.Invoke();
        }

        private void SpawnNodeGroup(Vector3 worldPos, int slotIndex)
        {
            if (hexNodeGroupPrefab == null) return;
            
            GameObject go = Instantiate(hexNodeGroupPrefab, transform);
            go.transform.SetParent(transform);
            go.transform.position = worldPos;
            
            HexNodeGroup hexNodeGroup = go.GetComponent<HexNodeGroup>();
            hexNodeGroup.Initialize(worldPos, slotIndex);
            _hexNodeGroups[slotIndex] = hexNodeGroup;
        }

        private void OnNodePlaced(int slotIndex)
        {
            _hexNodeGroups[slotIndex] = null;
            OnSlotFreed?.Invoke(slotIndex);
            
            for (int i = 0; i < _hexNodeGroups.Length; i++)
            { if (_hexNodeGroups[i] != null) return; }
            
            RefreshSlots();
        }
    }
}