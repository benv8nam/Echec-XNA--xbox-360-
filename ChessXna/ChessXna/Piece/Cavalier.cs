using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ChessXna
{
    public class Cavalier : Piece
    {
        public Cavalier(PlayerColor couleur)
            : base(couleur, "cavalier")
        {
            
        }

        /// <summary>
        /// Vérifie les mouvements en L du cavalier
        /// </summary>
        /// <param name="plateau">plateau</param>
        /// <param name="destination">destination</param>
        /// <param name="currentPosition">position</param>
        /// <returns>Vrai / faux</returns>
        public override bool IsValidMouvement(ref Case[,] plateau, Point destination, Point currentPosition)
        {
            if (plateau[destination.Y, destination.X].UnePiece == null || plateau[destination.Y, destination.X].UnePiece.Couleur != this.Couleur)
            {
                if (destination.Y == currentPosition.Y - 2)
                {
                    if (destination.X + 1 == currentPosition.X || destination.X - 1 == currentPosition.X)
                        return true;
                }
                else if (destination.Y == currentPosition.Y + 2)
                {
                    if (destination.X + 1 == currentPosition.X || destination.X - 1 == currentPosition.X)
                        return true;
                }
                else if (destination.X == currentPosition.X + 2)
                {
                    if (destination.Y + 1 == currentPosition.Y || destination.Y - 1 == currentPosition.Y)
                        return true;
                }
                else if (destination.X == currentPosition.X - 2)
                {
                    if (destination.Y + 1 == currentPosition.Y || destination.Y - 1 == currentPosition.Y)
                        return true;
                }
            }
            
            return false;
        }

    }
}
