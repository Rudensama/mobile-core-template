using System;
using UnityEngine;
using System.Collections.Generic;
using Managers;

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider2D))]
public class Line : MonoBehaviour
{
    public int cellAmount = 1;
    public float cellSize = 0.8659765f;
    
    private BoxCollider2D _collider;
    
    private void OnValidate()
    {
        UnityEditor.EditorApplication.delayCall += UpdateColliderSize;
        _collider = GetComponent<BoxCollider2D>();
    }
    
    private void OnEnable()
    {
        GridManager.OnNodePlaced += OnNodePlaced;
    }

    private void OnDisable()
    {
        GridManager.OnNodePlaced -= OnNodePlaced;
    }

    [ContextMenu("Generate")]
    private void UpdateColliderSize()
    {
        _collider.size = new Vector2(0.4f, cellSize *  cellAmount);
    }

    private void OnNodePlaced(int slotIndex)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, _collider.size, _collider.transform.eulerAngles.z);
        int count = 0;
        foreach (Collider2D collider in colliders)
        { if (collider.gameObject.CompareTag("Node")) count++; }

        if (count == cellAmount)
        {
            Debug.Log("Complete Line: " + gameObject.name);
        }
    }
}
