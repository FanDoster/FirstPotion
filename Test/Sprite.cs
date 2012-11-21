using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GDS_FirstPotion
{
    class Sprite
    {
        public enum State
        {
            Standing, Moving
        }

        private Texture2D sSpriteTexture;
        private float sScale; 
        private int sSpeed;
        private Vector2 sOrigin;
        private float sLayer;

        public string AssetName;
        public Vector2 Position;
        public Rectangle Size;
        public State CurrentState;

        public float Rotation;
        //    float Frame;
        //    Rectangle FrameRect;
        public Vector2 Direction;

        public virtual Rectangle BoundingBox
        {
            get
            {
                return new Rectangle();
            }
        }

        public Vector2 Origin
        {
            get
            {
                return sOrigin;
            }
            set
            {
                sOrigin = value;
            }
        }

        public int Speed
        {
            get
            {
                return sSpeed;
            }
            set
            {
                sSpeed = value;
            }
        }

        public Texture2D SpriteTexture
        {
            get
            {
                return sSpriteTexture;
            }
        }

        public float Scale
        {
            get
            {
                return sScale;
            }
            set
            {
                sScale = value;
                if (sSpriteTexture != null)
                {
                    Size = new Rectangle(0, 0, ((int)(sSpriteTexture.Width * Scale)), ((int)(sSpriteTexture.Height * Scale)));
                }
            }
        }

        public float Layer
        {
            get
            {
                return sLayer;
            }
            set
            {
                sLayer = value;
            }
        }


        public virtual void LoadContent(ContentManager cM, string aN) //aN = assetName
        {
            sSpriteTexture = cM.Load<Texture2D>(aN);
            Size = new Rectangle(0, 0, sSpriteTexture.Width, sSpriteTexture.Height);
        }

        public virtual void Update(GameTime gT, int speed, Vector2 dir)
        {
            Position += dir * speed * (float)gT.ElapsedGameTime.TotalSeconds;
        }

        public virtual void Draw(SpriteBatch sB)
        {
            sB.Draw(sSpriteTexture, Position, new Rectangle(0, 0, SpriteTexture.Width, SpriteTexture.Height), Color.White, Rotation, sOrigin, sScale, SpriteEffects.None, sLayer);
        }

        public Point GCP() //Get Cursor Position ------------------ To be moved and renamed.
        {
            MouseState mS = Mouse.GetState();
            return new Point(mS.X, mS.Y);
        }

        public float FindRotationDirection(Sprite toBeRotatedSprite, Sprite targetSprite) //rename & change parameters - use Vector2's
        {
            float rot;
            //Calculate the distance from the square to the mouse's X and Y position
            float distX = toBeRotatedSprite.Position.X - targetSprite.Position.X;
            float distY = toBeRotatedSprite.Position.Y - targetSprite.Position.Y;

            //Calculate the required rotation by doing a two-variable arc-tan
            return rot = (float)Math.Atan2(distY, distX);
        }

        public Sprite FindClosest(Sprite sprite, List<Enemy> enemyList)
        {
            Sprite result = sprite;
            float minDist;

            if (enemyList.Count > 0)
            {
                result = enemyList[0];
                minDist = Vector2.Distance(sprite.Position, enemyList[0].Position);

                foreach (Sprite member in enemyList)
                {
                    if (Vector2.Distance(sprite.Position, member.Position) < minDist)
                    {
                        result = member;
                    }
                }
            }
            return result;
        }

        public void Shoot(ContentManager cM, Sprite target, List<Bullet> bullets) // CHANGE ALMOST FULLY
        {
            Bullet newBullet = new Bullet(cM, this, target);
            newBullet.Position = this.Position + newBullet.Direction;
            bullets.Add(newBullet);
        }
    }
}
