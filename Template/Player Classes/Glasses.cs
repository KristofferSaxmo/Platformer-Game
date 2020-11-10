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
        public Glasses() { }
        public void Update(Vector2 playerPos, Texture2D glassesTex)
        {
            position = playerPos;
            texture = glassesTex;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
