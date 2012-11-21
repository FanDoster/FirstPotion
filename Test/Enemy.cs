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

    class Enemy : Sprite
    {
        Random RNG = new Random();

        public Enemy(ContentManager cM, Character target)
        {
            AssetName = "Enemy";
            Scale = 1.0f;
            base.LoadContent(cM, AssetName); //takes care of sSpriteTexture + sSpriteSize IMPORTANT: AFTER SCALE DECLARATION
            Position = RandomSpawn();
            Speed = 100;
            Origin = new Vector2((Size.Width / 2), (Size.Height / 2));
            Layer = 0.3f;
            Rotation = FindRotationDirection(this, target);
           // Direction = new Vector2((target.Position.X - Position.X), (target.Position.Y - Position.Y));
           // if (Direction != Vector2.Zero) Direction.Normalize();
            
            //CurrentState = State.Moving;
        }

        public override Rectangle BoundingBox //FIXED SIZE LATER
        {
            get
            {
                return new Rectangle
                    (
                        (int)(Position.X - ((SpriteTexture.Width * Scale) / 2)),
                        (int)(Position.Y - ((SpriteTexture.Height * Scale) / 2)),
                        (int)(SpriteTexture.Width * Scale),
                        (int)(SpriteTexture.Height * Scale)
                    );
            }
        }

        public void Update(GameTime gT, Sprite target)
        {
            Rotation = FindRotationDirection(this, target);
            Direction = new Vector2((target.Position.X - Position.X), (target.Position.Y - Position.Y));
            if (Direction != Vector2.Zero) Direction.Normalize();
            Position += Direction * Speed * (float)gT.ElapsedGameTime.TotalSeconds;
        }

        public Vector2 RandomSpawn()
        {
            Vector2 spawnLoc = this.Position;
            int spawnCase = RNG.Next(0, 4);

            switch (spawnCase)
            {
                case 0:
                    {
                        spawnLoc = new Vector2(RNG.Next(500), 0);
                        break;
                    }
                case 1:
                    {
                        spawnLoc = new Vector2(850, RNG.Next(500));
                        break;
                    }
                case 2:
                    {
                        spawnLoc = new Vector2(RNG.Next(500), 550);
                        break;
                    }
                case 3:
                    {
                        spawnLoc = new Vector2(0, RNG.Next(500));
                        break;
                    }
                    
            }
            return spawnLoc;
        }
    }
}

