namespace Chess
{ 
	[System.Serializable]
	public class SearchDiagnostics
	{
		public int lastCompletedDepth;
		public string moveVal;
		public string move;
		public int eval;
		public bool isBook;
		public int numPositionsEvaluated;
	}
}
