using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    class Bullet : BaseClass
    {
        Vector2 origin = new Vector2(9, 4);
        Rectangle sourceRectangle = new Rectangle(0, 0, 18, 8);
        readonly int speed = 15;
        public Bullet(Texture2D bulletTex, Vector2 bulletPos, Point size, float bulletRot, Vector2 bulletDir)
        {
            texture = bulletTex;
            position = bulletPos;
            rectangle = new Rectangle(bulletPos.ToPoint(), size);
            rotation = bulletRot;
            direction = bulletDir;
        }
        public void Move()
        {
            position += direction * speed;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(position.X, position.Y), sourceRectangle, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 1);
        }
    }
}
