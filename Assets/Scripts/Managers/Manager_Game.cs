using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public List<HNGData> hexNodeGroupDatas = new List<HNGData>();
    }
}