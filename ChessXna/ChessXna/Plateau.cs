using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ChessXna
{
    public class Plateau
    {
        int CaseHeight, CaseWidth;

        Rectangle plateauRectangle;

        Texture2D background;
        Texture2D casePoss;

        SpriteFont fontText;
        Color couleurCase = Color.Blue;
        bool pieceIsSelected, isEchec, isEchecEtMat;

        Case caseSelected = null;

        Case[,] plateauValeur;

        Selection rectangleSelection;

        PlayerColor playerTour;

        List<Point> positionPossibilities;

        //String infoMessage = "";

        public bool IsEchecEtMat
        {
            get { return isEchecEtMat; }
            set { isEchecEtMat = value; }
        }

        public Rectangle PlateauRectangle
        {
            get { return plateauRectangle; }
            set { plateauRectangle = value; }
        }

        public bool PieceIsSelected
        {
            get { return pieceIsSelected; }
        }

        public Plateau()
        {
            rectangleSelection = new Selection();
            plateauRectangle = new Rectangle(0, 0, 352, 352);
            plateauValeur = new Case[8, 8];
            positionPossibilities = new List<Point>();
            playerTour = PlayerColor.White;

            pieceIsSelected = false;
            isEchec = false;
            isEchecEtMat = false;

            CaseHeight = 44;
            CaseWidth = 44;
        }

        public void Initialize()
        {
            positionPossibilities.Clear();

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                        plateauValeur[y, x] = new Case();
                        plateauValeur[y, x].Position = new Point(x, y);
                }
            }

            for (int x = 0; x < 8; x++)
            {
                Pion pionNoir = new Pion(PlayerColor.Black);
                plateauValeur[1, x].UnePiece = pionNoir;

                Pion pionBlanc = new Pion(PlayerColor.White);
                plateauValeur[6, x].UnePiece = pionBlanc;
            }

            for (int y = 0; y < 2; y++)
            {
                PlayerColor couleur;

                if (y % 2 == 1)
                    couleur = PlayerColor.White;
                else
                    couleur = PlayerColor.Black;

                plateauValeur[y * 7, 0].UnePiece = new Tour(couleur);
                plateauValeur[y * 7, 7].UnePiece = new Tour(couleur);

                plateauValeur[y * 7, 1].UnePiece = new Cavalier(couleur);
                plateauValeur[y * 7, 6].UnePiece = new Cavalier(couleur);

                plateauValeur[y * 7, 2].UnePiece = new Fou(couleur);
                plateauValeur[y * 7, 5].UnePiece = new Fou(couleur);

                plateauValeur[y * 7, 3].UnePiece = new Reine(couleur);
                plateauValeur[y * 7, 4].UnePiece = new Roi(couleur);
            }
        }

        /// <summary>
        /// Charge les différentes images / polices
        /// </summary>
        /// <param name="Content">Gestionnaire de contenue</param>
        public void LoadContent(ContentManager Content)
        {
            rectangleSelection.LoadContent(Content, "selection");
            background = Content.Load<Texture2D>("bg");
            fontText = Content.Load<SpriteFont>("gamefont");
            casePoss = Content.Load<Texture2D>("case");

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Piece piece = plateauValeur[y, x].UnePiece;

                    if (piece != null)
                    {
                        piece.LoadContent(Content);
                    }
                }
            }
        }

        /// <summary>
        /// Méthode de dessin
        /// </summary>
        /// <param name="spriteBatch">moteur de dessin</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            DrawBoard(spriteBatch);
            DrawPiece(spriteBatch);
            DrawPossibilites(spriteBatch);
            DrawText(spriteBatch);
        }

        /// <summary>
        /// Dessine le plateau
        /// </summary>
        /// <param name="spriteBatch">moteur de dessin</param>
        private void DrawBoard(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, plateauRectangle, Color.White);
            spriteBatch.End();
        }

        /// <summary>
        /// Dessine les textes
        /// </summary>
        /// <param name="spriteBatch">moteur de dessin</param>
        private void DrawText(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            String message;
            if (playerTour == PlayerColor.White)
                message = "Au joueur blanc de jouer";
            else
                message = "Au joueur noir de jouer";

            spriteBatch.DrawString(fontText,message,new Vector2(0,plateauRectangle.Height),Color.Black);

            if (isEchec)
            {
                spriteBatch.DrawString(fontText, "ECHEC", new Vector2(0, plateauRectangle.Height + 20), Color.Red);
            }
            else if (pieceIsSelected && !isEchec)
            {
                spriteBatch.DrawString(fontText, "Selectionnez une destination", new Vector2(0, plateauRectangle.Height + 20), Color.Black);
            }

            spriteBatch.End();
        }

        /// <summary>
        /// Dessine les piéces du plateau
        /// </summary>
        /// <param name="spriteBatch">moteur de dessin</param>
        private void DrawPiece(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Piece piece = plateauValeur[y, x].UnePiece;

                    if (piece != null)
                    {
                        spriteBatch.Draw(piece.Texture,new Rectangle(x*CaseWidth,y*CaseHeight,CaseWidth,CaseHeight),Color.White);
                    }
                }
            }
            if(!rectangleSelection.IsHide)
                spriteBatch.Draw(rectangleSelection.Texture,rectangleSelection.Rectangle,Color.White);

            spriteBatch.End();
        }

        /// <summary>
        /// Retourne la case
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <returns>Retourne la case</returns>
        private Case getCase(int x, int y)
        {
            return plateauValeur[y,x];
        }

        /// <summary>
        /// Retourne une piece
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <returns>Retourne la piece</returns>
        private Piece getPiece(int x, int y)
        {
            return plateauValeur[y, x].UnePiece;
        }

        /// <summary>
        /// Sélectionne la piéce sous le curseur
        /// </summary>
        /// <param name="mouseCoord">Coordonnée de la souris</param>
        public void selectPiece(Point mouseCoord)
        {
            int x = mouseCoord.X - (mouseCoord.X % CaseWidth);
            int y = mouseCoord.Y - (mouseCoord.Y % CaseHeight);

            if (new Rectangle(x, y, 1, 1).Intersects(plateauRectangle))
            {
                caseSelected = getCase(x / CaseWidth, y / CaseHeight);


                
                if (caseSelected.UnePiece != null && caseSelected.UnePiece.Couleur == playerTour)
                {
                    if (isEchec)
                    {
                        if (!caseSelected.Equals(getKingCase(ref plateauValeur)))
                        {
                            ReleaseSelection();
                            return;
                        }
                    }

                    rectangleSelection.IsHide = false;
                    rectangleSelection.MoveTo(new Point(x, y));
                    pieceIsSelected = true;
                    TestPossibilities(ref plateauValeur, caseSelected, positionPossibilities);                 
                }
            }
        }

        /// <summary>
        /// Déplace la piece
        /// </summary>
        /// <param name="mouseCoord">Coordonnées de la souris</param>
        public void movePiece(Point mouseCoord)
        {
            int x = mouseCoord.X - (mouseCoord.X % CaseWidth);
            int y = mouseCoord.Y - (mouseCoord.Y % CaseHeight);

            if (new Rectangle(x, y, 1, 1).Intersects(plateauRectangle))
            {

                int caseX = x / CaseWidth;
                int caseY = y / CaseHeight;

                if (getCase(caseX, caseY).UnePiece == null || getCase(caseX, caseY).UnePiece.Couleur != playerTour)
                {
                    if (caseSelected.UnePiece.IsValidMouvement(ref plateauValeur, new Point(caseX, caseY),caseSelected.Position))
                    {
                        plateauValeur[caseY, caseX].UnePiece = caseSelected.UnePiece;
                        
                        if (caseSelected.UnePiece.GetType() == typeof(Pion))
                        {
                            ((Pion)plateauValeur[caseY, caseX].UnePiece).FirstMove = false;
                        }

                        caseSelected.UnePiece = null;
                        
                        if (playerTour == PlayerColor.White)
                            playerTour = PlayerColor.Black;
                        else
                            playerTour = PlayerColor.White;

                        if (CheckEchec(ref plateauValeur))
                        {
                            isEchec = true;
                            isEchecEtMat = CheckEchecEtMat();
                        }

                        ReleaseSelection();
                    }
                }
            }
        }

        /// <summary>
        /// Enléve la sélection
        /// </summary>
        public void ReleaseSelection()
        {
            rectangleSelection.IsHide = true;
            pieceIsSelected = false;
            caseSelected = null;
            positionPossibilities.Clear();
        }

        /// <summary>
        /// Teste les possibilités
        /// </summary>
        public void TestPossibilities(ref Case[,] plateau,Case caseSelectionnee,List<Point> possibilites)
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (caseSelectionnee.UnePiece.IsValidMouvement(ref plateau, new Point(x, y), caseSelectionnee.Position))
                        possibilites.Add(new Point(x, y));
                }
            }
        }

        /// <summary>
        /// Dessine les possibilités
        /// </summary>
        /// <param name="spriteBatch">Gestionnaire d'affichage</param>
        public void DrawPossibilites(SpriteBatch spriteBatch)
        {
            if (positionPossibilities.Count > 0)
            {               
                couleurCase.A = 50;
                spriteBatch.Begin();
                foreach (Point point in positionPossibilities)
                {
                    spriteBatch.Draw(casePoss, new Rectangle(point.X * casePoss.Width, point.Y * casePoss.Height, casePoss.Width, casePoss.Height), couleurCase);
                }
                spriteBatch.End();
            }
        }

        /// <summary>
        /// Retourne la case ou se trouve le roi courant
        /// </summary>
        /// <returns>Retourne la case</returns>
        private Case getKingCase(ref Case[,] plateau)
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (plateau[y, x].UnePiece != null)
                    {
                        if (plateau[y, x].UnePiece.GetType().Equals(typeof(Roi)) && plateau[y, x].UnePiece.Couleur == playerTour)
                        {
                            return plateau[y, x];
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Vérifie l'état d'échec de la partie
        /// </summary>
        /// <returns>Vrai / Faux</returns>
        private bool CheckEchec(ref Case[,] plateau)
        {
            Case caseRoi = getKingCase(ref plateau);

            if (caseRoi != null)
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        if (plateau[y, x].UnePiece != null && plateau[y, x].UnePiece.Couleur != playerTour)
                            if (plateau[y, x].UnePiece.IsValidMouvement(ref plateau, caseRoi.Position, plateau[y, x].Position))
                                return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Vérifie l'état d'échec et mat et termine la partie
        /// </summary>
        private bool CheckEchecEtMat()
        {
            Case[,] plateauTmp = new Case[8,8];

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    plateauTmp[y, x] = new Case(plateauValeur[y, x]);
                }
            }

            List<Point> possibilites = new List<Point>();

            Case king = getKingCase(ref plateauTmp);
            TestPossibilities(ref plateauTmp, king, possibilites);

            foreach (Point position in possibilites)
            {
                king.Position = position;

                if (!CheckEchec(ref plateauTmp))
                {
                    return false;
                }

            }

            return true;
        }
    }
}
