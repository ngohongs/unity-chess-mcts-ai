using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Chess
{
    [CreateAssetMenu(menuName = "AI/MCTSSettings")]
    public class MCTSSettings : AISettings
    {
        public bool limitNumOfPlayouts;
        public int maxNumOfPlayouts;
        public int playoutDepthLimit;
    }
}
