using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public HNGData[] hexNodeGroupDatas =  new HNGData[6];
    }
}