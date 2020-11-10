using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    class BaseGun
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected float rotation;
        protected Vector2 direction;
        protected int damage;
        protected int rateOfFire;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        public Vector2 Direction
        {
            get { return direction; }
            set { direction = value; }
        }
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }
        public int RateOfFire
        {
            get { return rateOfFire; }
            set { rateOfFire = value; }
        }
        public virtual void Shoot(List<Bullet> bullets, Texture2D bulletTex, int gunOffset) { }
        public virtual void UpdatePos(Vector2 playerPos)
        {
            position = playerPos;
        }
        public virtual void Update() { }
        public virtual void DrawRight(SpriteBatch spriteBatch) { }
        public virtual void DrawLeft(SpriteBatch spriteBatch) { }
    }
}
