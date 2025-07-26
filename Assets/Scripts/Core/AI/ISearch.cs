namespace Chess
{
    using System.Collections.Generic;
    using System.Threading;
    using UnityEngine;
    using static System.Math;

    public interface ISearch
    {
        event System.Action<Move> onSearchComplete;
        SearchDiagnostics Diagnostics { get; set; }
        void StartSearch();
        void EndSearch();
    }
}
