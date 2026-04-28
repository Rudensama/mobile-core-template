using UnityEngine;

public class HexNode : MonoBehaviour
{
   [SerializeField] private SpriteRenderer sprite;
   [SerializeField] private SpriteRenderer shadow;
   
   private int _type = -1;

   public void Initialize(int type, Color color)
   {
      _type = type;
      sprite.color = color;
   }

}
