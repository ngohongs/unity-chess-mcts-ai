namespace Chess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class ChessNode
    {
        // State is the state of the game at this node
        public Board State { get; private set; }
        // Action is the action that was taken to get to this state
        public Move Action { get; private set; }
        // UntriedActions are the actions that have not been tried from this node
        public List<Move> PossibleActions { get; private set; }
        // Parent is the parent node of this node
        public ChessNode Parent { get; private set; }
        public List<ChessNode> Children { get; private set; }
        public float Value { get; private set; } = 0;
        public uint Visits { get; private set; } = 0;

        public ChessNode(ChessNode parent, Board state, Move action)
        {
            State = state;
            Action = action;
            Parent = parent;
            PossibleActions = (new MoveGenerator()).GenerateMoves(State, Parent == null);
            Children = new List<ChessNode>();
        }

        public void AddChild(ChessNode child)
        {
            Children.Add(child);
        }

        public void Update(float value)
        {
            Visits++;
            Value += value;
        }

        public bool IsFullyExpanded()
        {
            return PossibleActions.Count == Children.Count;
        }

        public bool IsTerminal()
        {
            return PossibleActions.Count <= 0;
        }
    }
}