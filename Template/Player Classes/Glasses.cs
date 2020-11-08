using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    class Glasses : BaseClass
    {
        public Glasses(Texture2D glassesTex, Vector2 glassesPos)
        {
            texture = glassesTex;
            position = glassesPos;
        }
        public void UpdatePos(Vector2 playerPos)
        {
            position = playerPos;
            rectangle = new Rectangle(position.ToPoint(), rectangle.Size);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
