using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    /// <summary>
    /// A class that represents a single chess piece. (Used only when performing simulations.)
    /// </summary>
    public class SimPiece
    {
        /// <summary>
        /// true <=> white
        /// </summary>
        public readonly bool team;

        /// <summary>
        /// The type of the SimPiece (pawn, rook, etc.).
        /// </summary>
        public readonly SimPieceType type;

        /// <summary>
        /// A string that marks this piece when it is drawn to the console.
        /// </summary>
        public readonly string code;

        public readonly int value;

        public SimPiece(bool team, SimPieceType t)
        {
            this.team = team;
            type = t;
            code = GetCode();
            value = GetValue();
        }

        /// <summary>
        /// If the piece can be used to capture the enemy's king, a list with only this move
        /// is returned. Otherwise, all the moves that this piece can make are returned.
        /// </summary>
        /// <param name="board"> The state of the chess board. </param>
        /// <param name="startCoord1"> The first coordinate of the start position. </param>
        /// <param name="startCoord2"> The second coordinate of the start position. </param>
        public List<SimMove> GetMoves(SimPiece[,] board, int startCoord1, int startCoord2)
        {
            var result = new List<SimMove>();

            switch (type)
            {
                case SimPieceType.Pawn:
                    {
                        if (team)
                        {
                            // Add the forward moves.

                            if (startCoord1 != 7 && board[startCoord1 + 1, startCoord2] == null)
                            {
                                result.Add(new SimMove(startCoord1, startCoord2, startCoord1 + 1, startCoord2));
                            }

                            if (startCoord1 == 1 && board[startCoord1 + 1, startCoord2] == null && board[startCoord1 + 2, startCoord2] == null)
                            {
                                result.Add(new SimMove(startCoord1, startCoord2, startCoord1 + 2, startCoord2));
                            }

                            // Add the diagonal moves.

                            if (startCoord1 != 7 && startCoord2 != 7 &&
                                board[startCoord1 + 1, startCoord2 + 1] != null && board[startCoord1 + 1, startCoord2 + 1].team != team)
                            {
                                if (board[startCoord1 + 1, startCoord2 + 1].type == SimPieceType.King)
                                {
                                    return new List<SimMove>() { new SimMove(startCoord1, startCoord2, startCoord1 + 1, startCoord2 + 1) };
                                }

                                result.Add(new SimMove(startCoord1, startCoord2, startCoord1 + 1, startCoord2 + 1));
                            }

                            if (startCoord1 != 7 && startCoord2 != 0 &&
                                board[startCoord1 + 1, startCoord2 - 1] != null && board[startCoord1 + 1, startCoord2 - 1].team != team)
                            {
                                if (board[startCoord1 + 1, startCoord2 - 1].type == SimPieceType.King)
                                {
                                    return new List<SimMove>() { new SimMove(startCoord1, startCoord2, startCoord1 + 1, startCoord2 - 1) };
                                }

                                result.Add(new SimMove(startCoord1, startCoord2, startCoord1 + 1, startCoord2 - 1));
                            }
                        }
                        else
                        {
                            // Add the forward moves.

                            if (startCoord1 != 0 && board[startCoord1 - 1, startCoord2] == null)
                            {
                                result.Add(new SimMove(startCoord1, startCoord2, startCoord1 - 1, startCoord2));
                            }

                            if (startCoord1 == 6 && board[startCoord1 - 1, startCoord2] == null && board[startCoord1 - 2, startCoord2] == null)
                            {
                                result.Add(new SimMove(startCoord1, startCoord2, startCoord1 - 2, startCoord2));
                            }

                            // Add the diagonal moves.

                            if (startCoord1 != 0 && startCoord2 != 7 &&
                                board[startCoord1 - 1, startCoord2 + 1] != null && board[startCoord1 - 1, startCoord2 + 1].team != team)
                            {
                                if (board[startCoord1 - 1, startCoord2 + 1].type == SimPieceType.King)
                                {
                                    return new List<SimMove>() { new SimMove(startCoord1, startCoord2, startCoord1 - 1, startCoord2 + 1) };
                                }

                                result.Add(new SimMove(startCoord1, startCoord2, startCoord1 - 1, startCoord2 + 1));
                            }

                            if (startCoord1 != 0 && startCoord2 != 0 &&
                                board[startCoord1 - 1, startCoord2 - 1] != null && board[startCoord1 - 1, startCoord2 - 1].team != team)
                            {
                                if (board[startCoord1 - 1, startCoord2 - 1].type == SimPieceType.King)
                                {
                                    return new List<SimMove>() { new SimMove(startCoord1, startCoord2, startCoord1 - 1, startCoord2 - 1) };
                                }

                                result.Add(new SimMove(startCoord1, startCoord2, startCoord1 - 1, startCoord2 - 1));
                            }
                        }

                        return result;
                    }

                case SimPieceType.Rook:
                    {
                        // Check below.
                        for (int i = startCoord1 + 1; i < 8; ++i)
                        {
                            if (board[i, startCoord2] != null)
                            {
                                if (board[i, startCoord2].team != team)
                                {
                                    if (board[i, startCoord2].type == SimPieceType.King)
                                    {
                                        return new List<SimMove>() { new SimMove(startCoord1, startCoord2, i, startCoord2) };
                                    }

                                    result.Add(new SimMove(startCoord1, startCoord2, i, startCoord2));
                                }

                                break;
                            }
                            else
                            {
                                result.Add(new SimMove(startCoord1, startCoord2, i, startCoord2));
                            }
                        }

                        // Check to the right
                        for (int i = startCoord2 + 1; i < 8; ++i)
                        {
                            if (board[startCoord1, i] != null)
                            {
                                if (board[startCoord1, i].team != team)
                                {
                                    if (board[startCoord1, i].type == SimPieceType.King)
                                    {
                                        return new List<SimMove>() { new SimMove(startCoord1, startCoord2, startCoord1, i) };
                                    }

                                    result.Add(new SimMove(startCoord1, startCoord2, startCoord1, i));
                                }

                                break;
                            }
                            else
                            {
                                result.Add(new SimMove(startCoord1, startCoord2, startCoord1, i));
                            }
                        }

                        // Check above.
                        for (int i = startCoord1 - 1; i >= 0; --i)
                        {
                            if (board[i, startCoord2] != null)
                            {
                                if (board[i, startCoord2].team != team)
                                {
                                    if (board[i, startCoord2].type == SimPieceType.King)
                                    {
                                        return new List<SimMove>() { new SimMove(startCoord1, startCoord2, i, startCoord2) };
                                    }

                                    result.Add(new SimMove(startCoord1, startCoord2, i, startCoord2));
                                }

                                break;
                            }
                            else
                            {
                                result.Add(new SimMove(startCoord1, startCoord2, i, startCoord2));
                            }
                        }

                        // Check to the left.
                        for (int i = startCoord2 - 1; i >= 0; --i)
                        {
                            if (board[startCoord1, i] != null)
                            {
                                if (board[startCoord1, i].team != team)
                                {
                                    if (board[startCoord1, i].type == SimPieceType.King)
                                    {
                                        return new List<SimMove>() { new SimMove(startCoord1, startCoord2, startCoord1, i) };
                                    }

                                    result.Add(new SimMove(startCoord1, startCoord2, startCoord1, i));
                                }

                                break;
                            }
                            else
                            {
                                result.Add(new SimMove(startCoord1, startCoord2, startCoord1, i));
                            }
                        }

                        return result;
                    }

                case SimPieceType.Knight:
                    {
                        for (int i = -1; i <= 1; ++i)
                        {
                            for (int j = -2; j <= 2; j += 2)
                            {
                                if (i != 0 && j != 0)
                                {
                                    if (
                                        startCoord1 + i >= 0 && startCoord1 + i < 8 &&
                                        startCoord2 + j >= 0 && startCoord2 + j < 8 &&
                                        (board[startCoord1 + i, startCoord2 + j] == null || board[startCoord1 + i, startCoord2 + j].team != team)
                                        )
                                    {
                                        if (board[startCoord1 + i, startCoord2 + j] != null && board[startCoord1 + i, startCoord2 + j].type == SimPieceType.King)
                                        {
                                            return new List<SimMove>() { new SimMove(startCoord1, startCoord2, startCoord1 + i, startCoord2 + j) };
                                        }

                                        result.Add(new SimMove(startCoord1, startCoord2, startCoord1 + i, startCoord2 + j));
                                    }

                                    if (
                                        startCoord1 + j >= 0 && startCoord1 + j < 8 &&
                                        startCoord2 + i >= 0 && startCoord2 + i < 8 &&
                                        (board[startCoord1 + j, startCoord2 + i] == null || board[startCoord1 + j, startCoord2 + i].team != team)
                                        )
                                    {
                                        if (board[startCoord1 + j, startCoord2 + i] != null && board[startCoord1 + j, startCoord2 + i].type == SimPieceType.King)
                                        {
                                            return new List<SimMove>() { new SimMove(startCoord1, startCoord2, startCoord1 + j, startCoord2 + i) };
                                        }

                                        result.Add(new SimMove(startCoord1, startCoord2, startCoord1 + j, startCoord2 + i));
                                    }
                                }
                            }
                        }

                        return result;
                    }

                case SimPieceType.Bishop:
                    {
                        // Check down-right diagonal.
                        for (int i = 1; startCoord1 + i < 8 && startCoord2 + i < 8; ++i)
                        {
                            if (board[startCoord1 + i, startCoord2 + i] != null)
                            {
                                if (board[startCoord1 + i, startCoord2 + i].team != team)
                                {
                                    if (board[startCoord1 + i, startCoord2 + i].type == SimPieceType.King)
                                    {
                                        return new List<SimMove>() { new SimMove(startCoord1, startCoord2, startCoord1 + i, startCoord2 + i) };
                                    }

                                    result.Add(new SimMove(startCoord1, startCoord2, startCoord1 + i, startCoord2 + i));
                                }

                                break;
                            }
                            else
                            {
                                result.Add(new SimMove(startCoord1, startCoord2, startCoord1 + i, startCoord2 + i));
                            }
                        }

                        // Check down-left diagonal.
                        for (int i = 1; startCoord1 + i < 8 && startCoord2 - i >= 0; ++i)
                        {
                            if (board[startCoord1 + i, startCoord2 - i] != null)
                            {
                                if (board[startCoord1 + i, startCoord2 - i].team != team)
                                {
                                    if (board[startCoord1 + i, startCoord2 - i].type == SimPieceType.King)
                                    {
                                        return new List<SimMove>() { new SimMove(startCoord1, startCoord2, startCoord1 + i, startCoord2 - i) };
                                    }

                                    result.Add(new SimMove(startCoord1, startCoord2, startCoord1 + i, startCoord2 - i));
                                }

                                break;
                            }
                            else
                            {
                                result.Add(new SimMove(startCoord1, startCoord2, startCoord1 + i, startCoord2 - i));
                            }
                        }

                        // Check up-left diagonal.
                        for (int i = 1; startCoord1 - i >= 0 && startCoord2 - i >= 0; ++i)
                        {
                            if (board[startCoord1 - i, startCoord2 - i] != null)
                            {
                                if (board[startCoord1 - i, startCoord2 - i].team != team)
                                {
                                    if (board[startCoord1 - i, startCoord2 - i].type == SimPieceType.King)
                                    {
                                        return new List<SimMove>() { new SimMove(startCoord1, startCoord2, startCoord1 - i, startCoord2 - i) };
                                    }

                                    result.Add(new SimMove(startCoord1, startCoord2, startCoord1 - i, startCoord2 - i));
                                }

                                break;
                            }
                            else
                            {
                                result.Add(new SimMove(startCoord1, startCoord2, startCoord1 - i, startCoord2 - i));
                            }
                        }

                        // Check up-right diagonal.
                        for (int i = 1; startCoord1 - i >= 0 && startCoord2 + i < 8; ++i)
                        {
                            if (board[startCoord1 - i, startCoord2 + i] != null)
                            {
                                if (board[startCoord1 - i, startCoord2 + i].team != team)
                                {
                                    if (board[startCoord1 - i, startCoord2 + i].type == SimPieceType.King)
                                    {
                                        return new List<SimMove>() { new SimMove(startCoord1, startCoord2, startCoord1 - i, startCoord2 + i) };
                                    }

                                    result.Add(new SimMove(startCoord1, startCoord2, startCoord1 - i, startCoord2 + i));
                                }

                                break;
                            }
                            else
                            {
                                result.Add(new SimMove(startCoord1, startCoord2, startCoord1 - i, startCoord2 + i));
                            }
                        }

                        return result;
                    }

                case SimPieceType.Queen:
                    {
                        // Check below.
                        for (int i = startCoord1 + 1; i < 8; ++i)
                        {
                            if (board[i, startCoord2] != null)
                            {
                                if (board[i, startCoord2].team != team)
                                {
                                    if (board[i, startCoord2].type == SimPieceType.King)
                                    {
                                        return new List<SimMove>() { new SimMove(startCoord1, startCoord2, i, startCoord2) };
                                    }

                                    result.Add(new SimMove(startCoord1, startCoord2, i, startCoord2));
                                }

                                break;
                            }
                            else
                            {
                                result.Add(new SimMove(startCoord1, startCoord2, i, startCoord2));
                            }
                        }

                        // Check down-right diagonal.
                        for (int i = 1; startCoord1 + i < 8 && startCoord2 + i < 8; ++i)
                        {
                            if (board[startCoord1 + i, startCoord2 + i] != null)
                            {
                                if (board[startCoord1 + i, startCoord2 + i].team != team)
                                {
                                    if (board[startCoord1 + i, startCoord2 + i].type == SimPieceType.King)
                                    {
                                        return new List<SimMove>() { new SimMove(startCoord1, startCoord2, startCoord1 + i, startCoord2 + i) };
                                    }

                                    result.Add(new SimMove(startCoord1, startCoord2, startCoord1 + i, startCoord2 + i));
                                }

                                break;
                            }
                            else
                            {
                                result.Add(new SimMove(startCoord1, startCoord2, startCoord1 + i, startCoord2 + i));
                            }
                        }

                        // Check to the right
                        for (int i = startCoord2 + 1; i < 8; ++i)
                        {
                            if (board[startCoord1, i] != null)
                            {
                                if (board[startCoord1, i].team != team)
                                {
                                    if (board[startCoord1, i].type == SimPieceType.King)
                                    {
                                        return new List<SimMove>() { new SimMove(startCoord1, startCoord2, startCoord1, i) };
                                    }

                                    result.Add(new SimMove(startCoord1, startCoord2, startCoord1, i));
                                }

                                break;
                            }
                            else
                            {
                                result.Add(new SimMove(startCoord1, startCoord2, startCoord1, i));
                            }
                        }

                        // Check up-right diagonal.
                        for (int i = 1; startCoord1 - i >= 0 && startCoord2 + i < 8; ++i)
                        {
                            if (board[startCoord1 - i, startCoord2 + i] != null)
                            {
                                if (board[startCoord1 - i, startCoord2 + i].team != team)
                                {
                                    if (board[startCoord1 - i, startCoord2 + i].type == SimPieceType.King)
                                    {
                                        return new List<SimMove>() { new SimMove(startCoord1, startCoord2, startCoord1 - i, startCoord2 + i) };
                                    }

                                    result.Add(new SimMove(startCoord1, startCoord2, startCoord1 - i, startCoord2 + i));
                                }

                                break;
                            }
                            else
                            {
                                result.Add(new SimMove(startCoord1, startCoord2, startCoord1 - i, startCoord2 + i));
                            }
                        }

                        // Check above.
                        for (int i = startCoord1 - 1; i >= 0; --i)
                        {
                            if (board[i, startCoord2] != null)
                            {
                                if (board[i, startCoord2].team != team)
                                {
                                    if (board[i, startCoord2].type == SimPieceType.King)
                                    {
                                        return new List<SimMove>() { new SimMove(startCoord1, startCoord2, i, startCoord2) };
                                    }

                                    result.Add(new SimMove(startCoord1, startCoord2, i, startCoord2));
                                }

                                break;
                            }
                            else
                            {
                                result.Add(new SimMove(startCoord1, startCoord2, i, startCoord2));
                            }
                        }

                        // Check up-left diagonal.
                        for (int i = 1; startCoord1 - i >= 0 && startCoord2 - i >= 0; ++i)
                        {
                            if (board[startCoord1 - i, startCoord2 - i] != null)
                            {
                                if (board[startCoord1 - i, startCoord2 - i].team != team)
                                {
                                    if (board[startCoord1 - i, startCoord2 - i].type == SimPieceType.King)
                                    {
                                        return new List<SimMove>() { new SimMove(startCoord1, startCoord2, startCoord1 - i, startCoord2 - i) };
                                    }

                                    result.Add(new SimMove(startCoord1, startCoord2, startCoord1 - i, startCoord2 - i));
                                }

                                break;
                            }
                            else
                            {
                                result.Add(new SimMove(startCoord1, startCoord2, startCoord1 - i, startCoord2 - i));
                            }
                        }

                        // Check to the left.
                        for (int i = startCoord2 - 1; i >= 0; --i)
                        {
                            if (board[startCoord1, i] != null)
                            {
                                if (board[startCoord1, i].team != team)
                                {
                                    if (board[startCoord1, i].type == SimPieceType.King)
                                    {
                                        return new List<SimMove>() { new SimMove(startCoord1, startCoord2, startCoord1, i) };
                                    }

                                    result.Add(new SimMove(startCoord1, startCoord2, startCoord1, i));
                                }

                                break;
                            }
                            else
                            {
                                result.Add(new SimMove(startCoord1, startCoord2, startCoord1, i));
                            }
                        }

                        // Check down-left diagonal.
                        for (int i = 1; startCoord1 + i < 8 && startCoord2 - i >= 0; ++i)
                        {
                            if (board[startCoord1 + i, startCoord2 - i] != null)
                            {
                                if (board[startCoord1 + i, startCoord2 - i].team != team)
                                {
                                    if (board[startCoord1 + i, startCoord2 - i].type == SimPieceType.King)
                                    {
                                        return new List<SimMove>() { new SimMove(startCoord1, startCoord2, startCoord1 + i, startCoord2 - i) };
                                    }

                                    result.Add(new SimMove(startCoord1, startCoord2, startCoord1 + i, startCoord2 - i));
                                }

                                break;
                            }
                            else
                            {
                                result.Add(new SimMove(startCoord1, startCoord2, startCoord1 + i, startCoord2 - i));
                            }
                        }

                        return result;
                    }

                case SimPieceType.King:
                    {
                        for (int i = -1; i <= 1; ++i)
                        {
                            for (int j = -1; j <= 1; ++j)
                            {
                                if (
                                    (i != 0 || j != 0) &&
                                    startCoord1 + i >= 0 && startCoord1 + i < 8 &&
                                    startCoord2 + j >= 0 && startCoord2 + j < 8
                                    )
                                {
                                    if (board[startCoord1 + i, startCoord2 + j] == null || board[startCoord1 + i, startCoord2 + j].team != team)
                                    {
                                        if (board[startCoord1 + i, startCoord2 + j] != null && board[startCoord1 + i, startCoord2 + j].type == SimPieceType.King)
                                        {
                                            return new List<SimMove>() { new SimMove(startCoord1, startCoord2, startCoord1 + i, startCoord2 + j) };
                                        }

                                        result.Add(new SimMove(startCoord1, startCoord2, startCoord1 + i, startCoord2 + j));
                                    }
                                }
                            }
                        }

                        return result;
                    }

                default:
                    return result;
            }
        }

        /// <summary>
        /// Computes the code corresponding to the piece. The code consists of two letters,
        /// one which determines its colour and another which determines its type. This code
        /// is used to represent the piece when displaying the chess board on the console.
        /// </summary>
        private string GetCode()
        {
            string c = "";

            if (team)
            {
                c += 'w';
            }
            else
            {
                c += 'b';
            }

            switch (type)
            {
                case SimPieceType.Bishop: c += 'b'; break;
                case SimPieceType.King: c += 'K'; break;
                case SimPieceType.Knight: c += 'k'; break;
                case SimPieceType.Pawn: c += 'p'; break;
                case SimPieceType.Queen: c += 'q'; break;
                case SimPieceType.Rook: c += 'r'; break;
            }

            return c;
        }

        /// <summary>
        /// Gets the value of the piece based on the type.
        /// </summary>
        private int GetValue()
        {
            int v = 0;

            switch (type)
            {
                case SimPieceType.Bishop: v = Evaluation.bishopValue; break;
                case SimPieceType.Knight: v = Evaluation.knightValue; break;
                case SimPieceType.Pawn: v = Evaluation.pawnValue; break;
                case SimPieceType.Queen: v = Evaluation.queenValue; break;
                case SimPieceType.Rook: v = Evaluation.rookValue; break;
                case SimPieceType.King: v = Evaluation.kingValue; break;
            }

            return v;
        }
    }
}
