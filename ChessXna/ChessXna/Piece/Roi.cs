using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ChessXna
{
    public class Roi : Piece
    {
        public Roi(PlayerColor couleur)
            : base(couleur, "Roi")
        {
        }

        public override bool IsValidMouvement(ref Case[,] plateau, Point destination, Point currentPosition)
        {
            if (plateau[destination.Y, destination.X].UnePiece == null || plateau[destination.Y, destination.X].UnePiece.Couleur != this.Couleur)
            {
                if (destination.Y == currentPosition.Y - 1)
                {
                    if (CheckLigne(destination, currentPosition))
                        return true;
                }
                else if (destination.Y == currentPosition.Y)
                {
                    if (destination.X == currentPosition.X - 1 || destination.X == currentPosition.X + 1)
                    {
                        return true;
                    }
                }
                else if (destination.Y == currentPosition.Y + 1)
                {
                    if (CheckLigne(destination, currentPosition))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Vérifie la ligne supérieure / inférieure
        /// </summary>
        /// <param name="destination">Destination</param>
        /// <param name="currentPosition">Position courante</param>
        /// <returns>Vrai / faux</returns>
        private bool CheckLigne(Point destination, Point currentPosition)
        {
            if (destination.X == currentPosition.X - 1 || destination.X == currentPosition.X || destination.X == currentPosition.X + 1)
            {
                return true;
            }

            return false;
        }

    }
}
