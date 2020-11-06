using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    class Platform : BaseClass
    {
        public Platform(Texture2D platformTex, Vector2 platformPos, Point size)
        {
            texture = platformTex;
            position = platformPos;
            rectangle = new Rectangle(platformPos.ToPoint(), size);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.MediumAquamarine);
        }
    }
}
