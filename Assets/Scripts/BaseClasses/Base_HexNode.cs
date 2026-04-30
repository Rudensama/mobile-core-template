using Managers;
using UnityEngine;

public class HexNode : MonoBehaviour
{
   [SerializeField] private SpriteRenderer sprite;
   [SerializeField] private SpriteRenderer shadow;
   
   public int type = -1;
   public Vector3Int cellPosition = Vector3Int.one;
   
   public void Initialize(int t)
   {
      type = t;
   }
   
   public bool CanPlaced()
   {
      cellPosition = GridManager.Instance.WorldToCell(transform.position);
      return GridManager.Instance.IsEmptyCell(cellPosition) &&
             GridManager.Instance.IsCellInBound(cellPosition);
   }

   public void ShowShadow(bool show)
   {
      shadow.gameObject.SetActive(show);
      sprite.color = show 
         ? new Color(1.0f, 1.0f, 1.0f, 0.2f) 
         : new Color(1.0f, 1.0f, 1.0f, 1.0f);
      if (show && cellPosition.z != 1)
      {
         shadow.gameObject.transform.position = GridManager.Instance.CellToWorld(cellPosition);
      }
   }

   public void PlaceOnGrid()
   {
      transform.SetParent(GridManager.Instance.transform);
      transform.position = GridManager.Instance.CellToWorld(cellPosition);
      shadow.gameObject.SetActive(false);
      sprite.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
      GridManager.Instance.AddToMap(cellPosition, this);
   }
}
