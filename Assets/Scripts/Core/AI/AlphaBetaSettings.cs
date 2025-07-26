using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Chess
{
    [CreateAssetMenu(menuName = "AI/AlphaBetaSettings")]
    public class AlphaBetaSettings : AISettings
    {
        public int depth;
        public bool useIterativeDeepening;
        public bool useTranspositionTable;

        public bool useFixedDepthSearch;
        public bool clearTTEachMove;
    }
}
