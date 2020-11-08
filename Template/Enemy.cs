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
    class Enemy : BaseClass
    {
        Vector2 velocity = Vector2.Zero;
        public Enemy(Texture2D enemyTex, Vector2 enemyPos, Point size, float enemySpeed)
        {
            texture = enemyTex;
            position = enemyPos;
            rectangle = new Rectangle(enemyPos.ToPoint(), size);
            speed = enemySpeed;
        }
        public void Move(Vector2 playerPos)
        {

            direction.X = playerPos.X - position.X;
            direction.Y = playerPos.Y - position.Y;
            direction.Normalize();

            velocity += direction * speed; // Direction --> Velocity

            if (velocity.X > 3) // X Velocity not bigger than 4
                velocity.X = 3;

            else if (velocity.X < 0 - 3) // X Velocity not smaller than -4
                velocity.X = 0 - 3;

            if (velocity.Y > 3) // Y Velocity not bigger than 4
                velocity.Y = 3;

            else if (velocity.Y < 0 - 3) // Y Velocity not smaller than -4
                velocity.Y = 0 - 3;

            position.Y += velocity.Y; // Y Velocity --> Y Position
            position.X += velocity.X; // X Velocity --> X Position

            rectangle = new Rectangle(position.ToPoint(), rectangle.Size);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.Red);
        }
    }
}
