using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    class Platform : BaseClass
    {
        public Platform(Texture2D texture, Vector2 position, Point size)
        {
            this.texture = texture;
            base.position = position;
            rectangle = new Rectangle(position.ToPoint(), size);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.MediumAquamarine);
        }
    }
}
