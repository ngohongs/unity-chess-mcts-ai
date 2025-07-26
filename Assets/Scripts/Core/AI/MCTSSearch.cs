namespace Chess
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Threading;
    using Unity.VisualScripting;
    using UnityEngine;
    using static System.Math;

    class MCTSSearch : ISearch
    {
        public event System.Action<Move> onSearchComplete;

        MoveGenerator moveGenerator;

        Move bestMove;
        int bestEval;
        bool abortSearch;

        MCTSSettings settings;
        Board board;
        Evaluation evaluation;

        System.Random rand;

        float C = 1;

        // Diagnostics
        public SearchDiagnostics Diagnostics { get; set; }
        System.Diagnostics.Stopwatch searchStopwatch;

        public MCTSSearch(Board board, MCTSSettings settings)
        {
            this.board = board;
            this.settings = settings;
            evaluation = new Evaluation();
            moveGenerator = new MoveGenerator();
            rand = new System.Random();
        }

        public void StartSearch()
        {
            InitDebugInfo();

            // Initialize search settings
            bestEval = 0;
            bestMove = Move.InvalidMove;

            moveGenerator.promotionsToGenerate = settings.promotionsToSearch;
            abortSearch = false;
            Diagnostics = new SearchDiagnostics();

            SearchMoves();

            onSearchComplete?.Invoke(bestMove);

            if (!settings.useThreading)
            {
                LogDebugInfo();
            }
        }

        public void EndSearch()
        {
            if (settings.useTimeLimit)
            {
                abortSearch = true;
            }
        }

        void SearchMoves()
        {
            int playouts = 0;
            var root = new ChessNode(null, board, Move.InvalidMove);

            while (!abortSearch)
            {
                if (settings.limitNumOfPlayouts && playouts > settings.maxNumOfPlayouts)
                {
                    break;
                }

                var node = Select(root);
                node = Expand(node);
                var value = Playout(node);
                Backpropagate(node, value);

                playouts++;
            }
            
            var bestNode = BestNode(root);
            bestMove = bestNode.Action;
            bestEval = evaluation.Evaluate(bestNode.State);
        }

        void LogDebugInfo()
        {
            // Optional
        }

        void InitDebugInfo()
        {
            searchStopwatch = System.Diagnostics.Stopwatch.StartNew();
            // Optional
        }

        ChessNode Select(ChessNode node)
        {
            while (node.IsFullyExpanded() && !node.IsTerminal())
            {
                node = node.Children.OrderByDescending(n => UCB1(n)).First();
            }

            return node;
        }

        ChessNode Expand(ChessNode node)
        {
            if (node.IsTerminal())
            {
                return node;
            }

            // Expand with actions beginning from the end of generated moves to comply with assignment presentation
            var toChildAction = node.PossibleActions[node.PossibleActions.Count - 1 - node.Children.Count];
            var childState = node.State.Clone();
            childState.MakeMove(toChildAction);

            var childNode = new ChessNode(node, childState, toChildAction);

            node.AddChild(childNode);

            return childNode;
        }

        float Playout(ChessNode node)
        {
            // evaluate for the player who made the move 
            // not the player who is to move
            var evaluateForPlayer = !node.State.WhiteToMove;

            if (node.IsTerminal())
            {
                return 1.0f;
            }

            var simBoard = node.State.GetLightweightClone();
            var teamToPlay = node.State.WhiteToMove;

            for (int i = 0; i < settings.playoutDepthLimit; i++)
            { 
                SimMove? move = BaseStrategy(simBoard, teamToPlay);
                if (move == null)
                {
                    break;
                }

                var takenPiece = MakeSimMove(simBoard, move.Value);
                if (takenPiece != null && takenPiece.type == SimPieceType.King)
                {
                    return takenPiece.team == evaluateForPlayer ? 0.0f : 1.0f;
                }

                teamToPlay = !teamToPlay;
            }

            return evaluation.EvaluateSimBoard(simBoard, evaluateForPlayer);
        }

        void Backpropagate(ChessNode node, float value)
        {
            while (node != null)
            {
                node.Update(value);
                value = 1.0f - value;
                node = node.Parent;
            }
        }

        ChessNode BestNode(ChessNode node)
        {
            return node.Children.OrderByDescending(n => n.Visits).First();
        }

        SimPiece MakeSimMove(SimPiece[,] simBoard, SimMove move)
        {
            var piece = simBoard[move.startCoord1, move.startCoord2];
            var targetPiece = simBoard[move.endCoord1, move.endCoord2];

            simBoard[move.startCoord1, move.startCoord2] = null;
            simBoard[move.endCoord1, move.endCoord2] = piece;

            return targetPiece;
        }

        SimMove? BaseStrategy(SimPiece[,] state, bool teamToPlay)
        {
            var moves = moveGenerator.GetSimMoves(state, teamToPlay);
            
            if (moves.Count < 1)
                return null;

            return moves[rand.Next(moves.Count)];
        }

        float UCB1(ChessNode node)
        {
            if (node.Visits == 0)
                return float.PositiveInfinity;

            float value = node.Value / node.Visits + C * Mathf.Sqrt(Mathf.Log(node.Parent.Visits) / node.Visits);
            return value;
        }

    }
}