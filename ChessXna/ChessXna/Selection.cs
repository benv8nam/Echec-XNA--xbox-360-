using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ChessXna
{
    public class Selection : Sprite
    {
        bool isHide;

        public bool IsHide
        {
            get { return isHide; }
            set { isHide = value; }
        }

        public Selection()
            : base(Point.Zero)
        {
            isHide = true;
        }
    }
}
