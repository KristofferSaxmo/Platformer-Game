using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    class HealthBar : BaseClass
    {
        Rectangle remainingHealthBar;
        public HealthBar(Texture2D texture, Vector2 position, Point size, int health)
        {
            base.texture = texture;
            base.position = position;
            rectangle = new Rectangle(position.ToPoint(), size);
            base.health = health;
        }
        public void Update(Vector2 enemyPos, Rectangle enemyRec, int enemyHealth)
        {
            position = new Vector2(
                enemyPos.X + enemyRec.Width * 0.1f,
                enemyPos.Y + enemyRec.Height * 1.1f);

            rectangle = new Rectangle(position.ToPoint(), rectangle.Size);
            remainingHealthBar = new Rectangle(position.ToPoint(), new Point((rectangle.Width / health) * enemyHealth, rectangle.Height));
        }
        public void Draw(SpriteBatch spriteBatch, int gunDamage)
        {
            if(health > gunDamage)
            {
                spriteBatch.Draw(texture, rectangle, Color.DarkGray);
                spriteBatch.Draw(texture, remainingHealthBar, Color.LightBlue);
            }
        }
    }
}
