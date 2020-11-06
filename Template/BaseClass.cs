using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    class BaseClass
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Rectangle rectangle;

        public Rectangle Rectangle
        {
            get { return rectangle; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public virtual void Update() { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
