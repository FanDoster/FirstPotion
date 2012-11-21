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
    class Turret : Sprite
    {

        public Turret(ContentManager cM, Vector2 pos)
        {
            AssetName = "Turret";
            Scale = 1.0f;
            base.LoadContent(cM, AssetName); //takes care of sSpriteTexture + sSpriteSize IMPORTANT: AFTER SCALE DECLARATION
            Position = pos;
            Speed = 50;
            Origin = new Vector2((Size.Width / 2), (Size.Height / 2));
            Layer = 0.2f;
           // Rotation = FindRotationDirection(this, target);
            Direction = new Vector2(0, 0);

            //CurrentState = State.Moving;
        }

        public override Rectangle BoundingBox
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

        public void Update(GameTime gT, List<Enemy> enemies)
        {
            Sprite target = FindClosest(this, enemies);
            if (enemies.Count > 0)
            {
                Rotation = FindRotationDirection(this, target);
                Direction = new Vector2((target.Position.X - Position.X), (target.Position.Y - Position.Y));
                if (Direction != Vector2.Zero) Direction.Normalize();
            }
        }
    }
}
