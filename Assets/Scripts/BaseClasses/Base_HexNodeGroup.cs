using DG.Tweening;
using Managers;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace BaseClasses
{
    public class HexNodeGroup : MonoBehaviour
    {
        public GameObject hexNodePrefab;
        public UnityAction<int> OnNodeGroupPlaced;

        private Vector3 _slotPos;
        private int _slotIndex;
        
        public void Initialize(Vector3 slotPos, int slotIndex)
        {
            _slotPos =  slotPos;
            _slotIndex = slotIndex;
            HNGData data = GameManager.Instance.hexNodeGroupDatas[Random.Range(0, GameManager.Instance.hexNodeGroupDatas.Count)];
            foreach (Vector3 pos in data.tilePositions)
            { SpawnHexNode(pos); }
            

            DOVirtual.Float(0.0f, 1.0f, 0.2f, value =>
            {
                float scale = Mathf.Lerp(0.0f, 0.5f, value);
                transform.localScale = new Vector3(scale, scale, scale);
            }).SetEase(Ease.OutBack);
        }

        public void PlaceNodeGroup()
        {
            OnNodeGroupPlaced?.Invoke(_slotIndex);
        }

        private void SpawnHexNode(Vector3 pos)
        {
            GameObject go = Instantiate(hexNodePrefab, transform);
            go.transform.SetParent(transform);
            go.transform.localPosition = pos;
        }
    }
}