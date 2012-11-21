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
    class Bullet : Sprite
    {
        public bool IsVisible = true;
        public Vector2 StartingPoint;

        public Bullet(ContentManager cM, Sprite shooter, Sprite target)
        {
            AssetName = "Bullet";
            Scale = 0.5f;
            base.LoadContent(cM, AssetName); //takes care of sSpriteTexture + sSpriteSize
            StartingPoint = Position = shooter.Position;
            Speed = 300; 
            Origin = new Vector2((Size.Width / 2), (Size.Height / 2));
            Layer = 0.2f;
            Rotation = FindRotationDirection(this, target);
            Direction = new Vector2((target.Position.X - Position.X),(target.Position.Y - Position.Y));
            if (Direction != Vector2.Zero) Direction.Normalize();
          //  CurrentState = State.Moving
        }

        public override Rectangle BoundingBox //AFTER BULLETS HAVE BEEN DRAWN, FIXED SIZE FORLESS CALCULATIONS
        {
            get
            {
                return new Rectangle
                (
                    (int)(Position.X - (SpriteTexture.Width / 4)),
                    (int)(Position.Y - (SpriteTexture.Height / 2)),
                    (int)(SpriteTexture.Width / 4),
                    (int)(SpriteTexture.Height / 2)
                ); 
            }
        }

        public void Update(GameTime gT)
        {
            Position += Direction * Speed * (float)gT.ElapsedGameTime.TotalSeconds;
        }


    }
}