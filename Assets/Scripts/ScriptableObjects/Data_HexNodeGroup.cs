using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "HexNodeGroupData", menuName = "Hex", order = 0)]
    public class HNGData : ScriptableObject
    {
        public List<Vector3> tilePositions = new List<Vector3>();
    }
}