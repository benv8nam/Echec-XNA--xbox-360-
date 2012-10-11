using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ChessXna
{
    public abstract class Piece : Sprite
    {
        PlayerColor couleur;
        String nom;

        public PlayerColor Couleur
        {
            get { return couleur; }
            set { couleur = value; }
        }

        public Piece(PlayerColor couleur, String nom)
        {
            if (couleur == PlayerColor.White)
                this.nom = nom + "B";
            else
                this.nom = nom + "N";

            this.couleur = couleur;
        }

        /// <summary>
        /// Vérifie si le mouvement de la piéce est valide
        /// </summary>
        /// <param name="plateau">Plateau de jeu</param>
        /// <returns>Retourne vrai / faux</returns>
        public abstract bool IsValidMouvement(ref Case[,] plateau,Point destination,Point currentPosition);

        public virtual void Move()
        {
        }

        public void LoadContent(ContentManager Content)
        {
            this.Texture = Content.Load<Texture2D>(nom);
        }
    }
}
