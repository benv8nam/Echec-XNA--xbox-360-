using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ChessXna
{
    public class Fou : Piece
    {
        public Fou(PlayerColor couleur)
            : base(couleur, "Fou")
        {
        }

        public override bool IsValidMouvement(ref Case[,] plateau, Point destination, Point currentPosition)
        {
            if (plateau[destination.Y, destination.X].UnePiece == null || plateau[destination.Y, destination.X].UnePiece.Couleur != this.Couleur)
            {
                if (destination.Y < currentPosition.Y && destination.X > currentPosition.X)
                {
                    // Diagonale Haut Droite    
                    if (TopRightToBottomLeft(destination, currentPosition, ref plateau))
                        return true;
                }
                else if (destination.Y < currentPosition.Y && destination.X < currentPosition.X)
                {
                    // Diagonale Haut Gauche
                    if (TopLeftToBottomRight(currentPosition, destination, ref plateau))
                        return true;
                }
                else if (destination.Y > currentPosition.Y && destination.X < currentPosition.X)
                {
                    // Diagonale Bas Gauche
                    if (TopRightToBottomLeft(currentPosition, destination, ref plateau))
                        return true;
                }
                else if (destination.Y > currentPosition.Y && destination.X > currentPosition.X)
                {
                    // Diagonale Bas droite

                    if (TopLeftToBottomRight(destination,currentPosition,ref plateau))
                        return true;
                }
            }
            
            return false;
        }

        /// <summary>
        /// Verifie sur la diagonale Nord ouest -> Sud Est
        /// </summary>
        /// <param name="currentPosition">Position courante</param>
        /// <param name="destination">Position destination</param>
        /// <param name="plateau">Plateau de jeu</param>
        /// <returns>Vrai / Faux</returns>
        private bool TopLeftToBottomRight(Point currentPosition,Point destination,ref Case[,] plateau)
        {
            if (destination.Y - destination.X == currentPosition.Y - currentPosition.X)
            {
                int init = currentPosition.Y - destination.Y;

                for (int i = 0 + 1; i < init; i++)
                {
                    if (plateau[i + destination.Y, i + destination.X].UnePiece != null)
                        return false;
                }
                return true;
            }

            return false;
        }

        /// <summary>
        /// Verifie sur la diagonale Nord Est -> Sud Ouest
        /// </summary>
        /// <param name="currentPosition">Position courante</param>
        /// <param name="destination">Position destination</param>
        /// <param name="plateau">Plateau de jeu</param>
        /// <returns>Vrai / Faux</returns>
        private bool TopRightToBottomLeft(Point currentPosition, Point destination, ref Case[,] plateau)
        {
            if (destination.Y + destination.X == currentPosition.Y + currentPosition.X)
            {
                int x = currentPosition.X - 1;
                int y = currentPosition.Y + 1;

                while (x > destination.X && y < destination.Y)
                {
                    if (plateau[y, x].UnePiece != null)
                        return false;

                    x--;
                    y++;
                }

                return true;
            }

            return false;
        }

    }
}
