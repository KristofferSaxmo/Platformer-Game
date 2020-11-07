using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    class Bullets : BaseClass
    {
        public Bullets(Texture2D bulletTex, Vector2 bulletPos, Point size)
        {
            texture = bulletTex;
            position = bulletPos;
            rectangle = new Rectangle(bulletPos.ToPoint(), size);
        }
        public void Fly()
        {

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
