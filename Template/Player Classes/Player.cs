using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    class Player : BaseClass
    {
        Vector2 velocity = Vector2.Zero;
        int jumpTime;
        int doubleJumpTime;
        bool allowJump = false;
        bool allowDoubleJump = false;

        public Player(Texture2D playerTex, Vector2 playerPos, Point size)
        {
            texture = playerTex;
            position = playerPos;
            rectangle = new Rectangle(playerPos.ToPoint(), size);
        }
        public void MovePlayer()
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.A)) // Left
            {
                velocity.X -= 50f * 1f / 60f;
            }

            if(keyboardState.IsKeyDown(Keys.D)) // Right
            {
                velocity.X += 50f * 1f / 60f;
            }

            if (keyboardState.IsKeyDown(Keys.Space) && allowJump == true && jumpTime > 0) // Jump
            {
                velocity.Y = -4;
                jumpTime--;
            }

            if (keyboardState.IsKeyDown(Keys.Space) && allowJump == false && allowDoubleJump == true && doubleJumpTime > 0) // Double jump
            {
                velocity.Y = -4;
                doubleJumpTime--;
            }

            if (keyboardState.IsKeyUp(Keys.Space) && jumpTime != 20) // Disallow jump
                allowJump = false;

            if (keyboardState.IsKeyUp(Keys.Space) && doubleJumpTime != 20) // Disallow double jump
                allowDoubleJump = false;

            if (velocity.X > 4) // X Velocity not bigger than 4
                velocity.X = 4;

            else if (velocity.X < -4) // X Velocity not smaller than -4
                velocity.X = -4;

            if (velocity.X < 0)
                velocity.X += 0.1f;

            else if (velocity.X > 0)
                velocity.X -= 0.1f;

            if (velocity.X < 0.1f && velocity.X > -0.1f)
                velocity.X = 0;
        }
        public override void MirrorMap()
        {
            base.MirrorMap();
        }
        public void FloorCollision()
        {
            velocity.Y = 0;
            allowJump = true;
            allowDoubleJump = true;
            jumpTime = 20;
            doubleJumpTime = 20;

            if (velocity.X < 0)
                velocity.X += 0.2f;

            else if (velocity.X > 0)
                velocity.X -= 0.2f;
        }
        public void RoofCollision()
        {
            velocity.Y = 0;
        }
        public void WallCollision()
        {
            velocity.X = 0;
        }

        public override void Update()
        {
            MovePlayer();
            MirrorMap();

            velocity.Y += 9.82f * 1f / 60f; // Gravitation
            if (velocity.Y > 20)
                velocity.Y = 20;

            position.Y += velocity.Y; // Y Velocity --> Y Position
            position.X += velocity.X; // X Velocity --> X Position

            rectangle = new Rectangle(position.ToPoint(), rectangle.Size); // Rectangle = Position

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}