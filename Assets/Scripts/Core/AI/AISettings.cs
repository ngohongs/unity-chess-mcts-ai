namespace Chess {
	using System.Collections.Generic;
	using System.Collections;
	using UnityEngine;

	public abstract class AISettings : ScriptableObject {

		public event System.Action requestAbortSearch;

		public bool useThreading;
		public bool useTimeLimit;
		public int searchTimeMillis = 1000;
		public bool endlessSearchMode;
		
		public bool useBook;
		public TextAsset book;
		public int maxBookPly = 10;
		
		public MoveGenerator.PromotionMode promotionsToSearch;

		public SearchDiagnostics diagnostics;

		public void RequestAbortSearch () {
			requestAbortSearch?.Invoke ();
		}
	}
}