using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ChessXna
{
    /// <summary>
    /// Classe Pion
    /// </summary>
    public class Pion : Piece
    {
        /// <summary>
        /// Premier mouvement
        /// </summary>
        private bool firstMove;

        public bool FirstMove
        {
            get { return firstMove; }
            set { firstMove = value; }
        }

        public Pion(PlayerColor couleur) : 
            base(couleur,"Pion")
        {
            firstMove = true;
        }

        public override bool IsValidMouvement(ref Case[,] plateau, Point destination, Point currentPosition)
        {
            if (this.Couleur == PlayerColor.Black)
            {
                if (plateau[destination.Y, destination.X].UnePiece == null)
                {
                    if (firstMove)
                    {
                        if (destination.Y == currentPosition.Y + 2 && destination.X == currentPosition.X)
                        {
                            return true;
                        }

                        if (destination.Y == currentPosition.Y + 1 && destination.X == currentPosition.X)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (destination.Y == currentPosition.Y + 1 && destination.X == currentPosition.X)
                            return true;
                    }
                }
                else if (plateau[destination.Y, destination.X].UnePiece.Couleur == PlayerColor.White)
                {
                    if (destination.Y == currentPosition.Y + 1 && (destination.X == currentPosition.X - 1 || destination.X == currentPosition.X + 1))
                        return true;
                }
            }
            else if (this.Couleur == PlayerColor.White)
            {
                if (plateau[destination.Y, destination.X].UnePiece == null)
                {
                    if (firstMove)
                    {
                        if (destination.Y == currentPosition.Y - 2 && destination.X == currentPosition.X)
                        {
                            return true;
                        }

                        if (destination.Y == currentPosition.Y - 1 && destination.X == currentPosition.X)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (destination.Y == currentPosition.Y - 1 && destination.X == currentPosition.X)
                            return true;
                    }
                }
                else if (plateau[destination.Y, destination.X].UnePiece.Couleur == PlayerColor.Black)
                {
                    if (destination.Y == currentPosition.Y - 1 && (destination.X == currentPosition.X+1 || destination.X == currentPosition.X-1))
                        return true;
                }
            }
            
            return false;
        }
    }
}
