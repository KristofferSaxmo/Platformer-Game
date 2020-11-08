using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    class Gun : BaseClass
    {
        Vector2 distance;
        Vector2 rightOrigin = new Vector2(0, 8);
        Vector2 leftOrigin = new Vector2(26, 8);

        float leftRotation;
        float rightRotation;

        Rectangle sourceRectangle = new Rectangle(0, 0, 26, 16);

        public Gun(Texture2D gunTex, Vector2 gunPos)
        {
            texture = gunTex;
            position = gunPos;
        }
        public void ChangePosition(Vector2 playerPos)
        {
            position = playerPos;
        }
        public override void Update()
        {
            var mouseState = Mouse.GetState();

            distance.X = mouseState.X - position.X;
            distance.Y = mouseState.Y - position.Y;
            distance.Normalize();

            leftRotation = (float)Math.Atan2(0 - distance.Y, 0 - distance.X);   // Left angle
            rightRotation = (float)Math.Atan2(distance.Y, distance.X);          // Right angle

        }
        public void DrawRight(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(position.X + 20, position.Y + 12), sourceRectangle, Color.White, rightRotation, rightOrigin, 1.0f, SpriteEffects.None, 1);
        }
        public void DrawLeft(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(position.X + 10, position.Y + 12), sourceRectangle, Color.White, leftRotation, leftOrigin, 1.0f, SpriteEffects.None, 1);
        }
    }
}
