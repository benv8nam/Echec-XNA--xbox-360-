using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ChessXna
{
    /// <summary>
    /// Classe Tour
    /// </summary>
    public class Tour : Piece
    {
        public Tour(PlayerColor couleur)
            : base(couleur, "Tour")
        {
        }

        /// <summary>
        /// Vérifie les mouvements verticaux & horizontaux de la tour
        /// </summary>
        /// <param name="plateau">plateau</param>
        /// <param name="destination">destination</param>
        /// <param name="currentPosition">position</param>
        /// <returns>Vrai / faux</returns>
        public override bool IsValidMouvement(ref Case[,] plateau, Point destination, Point currentPosition)
        {

            if (plateau[destination.Y, destination.X].UnePiece == null || plateau[destination.Y, destination.X].UnePiece.Couleur != this.Couleur)
            {
                if (destination.Y == currentPosition.Y)
                {
                    if (destination.X > currentPosition.X)
                    {
                        if (MoveHorizontal(currentPosition, destination, ref plateau))
                            return true;
                    }
                    else
                    {
                        if (MoveHorizontal(destination, currentPosition, ref plateau))
                            return true;
                    }
                }
                else if (destination.X == currentPosition.X)
                {
                    if (destination.Y > currentPosition.Y)
                    {
                        if (MoveVertical(currentPosition, destination, ref plateau))
                            return true;
                    }
                    else
                    {
                        if (MoveVertical(destination, currentPosition, ref plateau))
                            return true;
                    }
                }
            }
            return false;
        }

        private bool MoveVertical(Point currentPosition, Point destination, ref Case[,] plateau)
        {
            for (int y = currentPosition.Y + 1; y < destination.Y; y++)
            {
                if (plateau[y, destination.X].UnePiece != null)
                    return false;
            }

            return true;
        }

        private bool MoveHorizontal(Point currentPosition, Point destination, ref Case[,] plateau)
        {
            for (int x = destination.X + 1; x < currentPosition.X; x++)
            {
                if (plateau[destination.Y, x].UnePiece != null)
                    return false;
            }

            return true;
        }

    }
}
