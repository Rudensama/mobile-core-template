using System;
using System.Collections.Generic;
using DG.Tweening;
using Managers;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class HexNodeGroup : MonoBehaviour
{
    public GameObject hexNodePrefab;
    public UnityAction<int> OnNodeGroupPlaced;
    public int type = 1;

    private Vector3 _slotPos;
    private int _slotIndex;
    private List<HexNode> _hexNodes = new List<HexNode>();
    private bool _canPlace;

    

    public void Initialize(Vector3 slotPos, int slotIndex)
    {
        _slotPos = slotPos;
        _slotIndex = slotIndex;
        HNGData data = GameManager.Instance.hexNodeGroupDatas[Random.Range(0, GameManager.Instance.hexNodeGroupDatas.Length)];
        foreach (Vector3 pos in data.tilePositions)
        { SpawnHexNode(pos); }
        

        DOVirtual.Float(0.0f, 1.0f, 0.2f, value =>
        {
            float scale = Mathf.Lerp(0.0f, 0.5f, value);
            transform.localScale = new Vector3(scale, scale, scale);
        }).SetEase(Ease.OutBack);
    }
    
    private void SpawnHexNode(Vector3 pos)
    {
        if (hexNodePrefab == null)
        {
            Debug.LogError("hexNodePrefab is not assigned!", this);
            return;
        }

        GameObject go = Instantiate(hexNodePrefab, transform);
        go.transform.localPosition = pos;

        HexNode hexNode = go.GetComponent<HexNode>();
        if (hexNode == null) return;

        hexNode.Initialize(type);
        _hexNodes.Add(hexNode);
    }

    public void StartDrag()
    {
        DOVirtual.Float(0.0f, 1.0f, 0.2f, value =>
        {
            float scale = Mathf.Lerp(0.5f, 1.0f, value);
            transform.localScale = new Vector3(scale, scale, scale);
        }).SetEase(Ease.OutBack);
    }

    public void UpdateDrag(Vector3 worldPos)
    {
        foreach (HexNode hexNode in _hexNodes)
        {
            _canPlace = hexNode.CanPlaced();
            if (!_canPlace) break;
        }
        
        foreach (HexNode hexNode in _hexNodes)
        {
            hexNode.ShowShadow(_canPlace);
        }
        transform.position = worldPos;
    }

    public void EndDrag()
    {
        if (!_canPlace) ReturnToSlot();
        else PlaceOnGrid();
    }

    private void ReturnToSlot()
    {
        Vector3 startPos = transform.position;
        
        DOVirtual.Float(0.0f, 1.0f, 0.2f, value =>
        {
            Vector3 pos = Vector3.Lerp(startPos, _slotPos, value);
            transform.position = pos;
            float scale = Mathf.Lerp(1.0f, 0.5f, value);
            transform.localScale = new Vector3(scale, scale, scale);
        }).SetEase(Ease.InBack);
    }

    private void PlaceOnGrid()
    {
        foreach (HexNode hexNode in _hexNodes) hexNode.PlaceOnGrid();
        OnNodeGroupPlaced?.Invoke(_slotIndex);
        GridManager.OnNodePlaced?.Invoke(_slotIndex);
        Destroy(gameObject);
    }
    
    
    
}
