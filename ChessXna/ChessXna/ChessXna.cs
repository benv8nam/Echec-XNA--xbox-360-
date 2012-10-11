using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace ChessXna
{
    public class ChessXna : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        ScreenManager screenManager;

        /// <summary>
        /// Constructeur du jeu
        /// </summary>
        public ChessXna()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 412;
            graphics.PreferredBackBufferWidth = 352;
            Window.Title = "Echec";
            Window.AllowUserResizing = false;
            IsMouseVisible = true;
            IsFixedTimeStep = true;

            screenManager = new ScreenManager(this);
            screenManager.Graphics = graphics;

            Components.Add(screenManager);

            screenManager.AddScreen(new BackgroundScreen(), null);
            screenManager.AddScreen(new MainMenuScreen(), null);
        }

        /// <summary>
        /// Initialise les données du jeu
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Charge les différentes images / polices
        /// </summary>
        protected override void LoadContent()
        {
            
        }

        /// <summary>
        /// Décharge les images / polices
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
        }

        /// <summary>
        /// Met à jour les données
        /// </summary>
        /// <param name="gameTime">Gestionnaire de temps</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// Dessine les différents éléments du jeu
        /// </summary>
        /// <param name="gameTime">Gestionnaire de temps</param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
