using System;
using System.Collections.Generic;
using System.Linq;

namespace DotsNLines
{
    public partial class Board
    {
        public double EvaluationFunction()
        {
            return computerScore - humanScore;
        }

        public Board Clone()
        {
            Board clone = new Board(rowsDots, columnsDots);

            for (int i = 0; i < Lines.Count; i++)
            {
                clone.Lines[i].OwnedBy = Lines[i].OwnedBy;
            }
            foreach (Box box in clone.boxes)
            {
                foreach (Line line in box.Lines)
                {
                    Line originalLine = Lines.FirstOrDefault(l => l.Id == line.Id);
                    if (originalLine != null)
                        line.OwnedBy = originalLine.OwnedBy;
                }
                Box originalBox = boxes.FirstOrDefault(b => b.Id == box.Id);
                if (originalBox != null)
                    box.OwnedBy = originalBox.OwnedBy;
            }
            clone.humanScore = humanScore;
            clone.computerScore = computerScore;
            return clone;
        }

        public bool ApplyMove(int lineId, PlayerType player)
        {
            Line line = Lines.FirstOrDefault(l => l.Id == lineId);
            if (line == null || line.OwnedBy != PlayerType.None)
                return false; 

            line.OwnedBy = player;
            bool scored = false;

            foreach (Box box in boxes)
            {
                if (box.Lines.Any(l => l.Id == line.Id))
                {
                    if (box.OwnedBy == PlayerType.None && box.Lines.All(l => l.OwnedBy != PlayerType.None))
                    {
                        box.OwnedBy = player;
                        scored = true;
                        if (player == PlayerType.Human) humanScore++;
                        else if (player == PlayerType.Computer) computerScore++;
                    }
                }
            }
            return scored;
        }
        public List<int> GetValidMoves()
        {
            return Lines.Where(l => l.OwnedBy == PlayerType.None).Select(l => l.Id).ToList();
        }
        public bool IsGameOver()
        {
            return Lines.All(l => l.OwnedBy != PlayerType.None);
        }
    }
    public partial class Minimax_alpha_beta
    {
        private int MaxDepth = 2;//factoru vietii

        public static Board FindNextBoard(Board currentBoard)
        {
            int bestMove = -1;
            double bestValue = double.NegativeInfinity;

            var validMoves = currentBoard.GetValidMoves();

            foreach (int move in validMoves)
            {
                Board newBoard = currentBoard.Clone();
                bool scored = newBoard.ApplyMove(move, PlayerType.Computer);

                double moveValue;
                if (scored)
                {
                    moveValue = Minimax(newBoard, currentBoard.difficulty, double.NegativeInfinity, double.PositiveInfinity, true);
                }
                else
                {
                    moveValue = Minimax(newBoard, currentBoard.difficulty, double.NegativeInfinity, double.PositiveInfinity, false);
                }

                if (moveValue > bestValue)
                {
                    bestValue = moveValue;
                    bestMove = move;
                }
            }

            if (bestMove == -1) return currentBoard;

            Board bestBoard = currentBoard.Clone();
            bestBoard.ApplyMove(bestMove, PlayerType.Computer);

            return bestBoard;
        }

        private static double Minimax(Board board, int depth, double alpha, double beta, bool maximizingPlayer)
        {
            if (depth == 0 || board.IsGameOver())
            {
                return board.EvaluationFunction();
            }

            var validMoves = board.GetValidMoves();

            if (maximizingPlayer)
            {
                double maxEval = double.NegativeInfinity;
                foreach (var move in validMoves)
                {
                    Board newBoard = board.Clone();
                    bool scored = newBoard.ApplyMove(move, PlayerType.Computer);

                    double eval;
                    if (scored)
                    {
                        eval = Minimax(newBoard, depth - 1, alpha, beta, true);
                    }
                    else
                    {
                        eval = Minimax(newBoard, depth - 1, alpha, beta, false);
                    }
                    maxEval = Math.Max(maxEval, eval);
                    alpha = Math.Max(alpha, eval);
                    if (beta <= alpha)
                        break; 
                }
                return maxEval;
            }
            else
            {
                double minEval = double.PositiveInfinity;
                foreach (var move in validMoves)
                {
                    Board newBoard = board.Clone();
                    bool scored = newBoard.ApplyMove(move, PlayerType.Human);

                    double eval;
                    if (scored)
                    {
                        eval = Minimax(newBoard, depth - 1, alpha, beta, false);
                    }
                    else
                    {
                        eval = Minimax(newBoard, depth - 1, alpha, beta, true);
                    }
                    minEval = Math.Min(minEval, eval);
                    beta = Math.Min(beta, eval);
                    if (beta <= alpha)
                        break; 
                }
                return minEval;
            }
        }
    }
}
