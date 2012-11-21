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
    class Cursor : Sprite
    { 

        public Cursor(ContentManager cM)
        {
            AssetName = "Aim";
            Scale = 1.0f;
            base.LoadContent(cM, AssetName); //takes care of sSpriteTexture + sSpriteSize
            Position = new Vector2(GCP().X, GCP().Y);
         //   Speed = 100; //not relevant
            Origin = new Vector2((Size.Width / 2), (Size.Height / 2));
            Layer = 0.0f; 
            Rotation = 0.0f;
          //  Direction = new Vector2(0, 0);
          //  CurrentState = State.Moving;

        }

        public void Update(GameTime gT)
        {
            Position.X = GCP().X;
            Position.Y = GCP().Y;
            Rotation += 0.02f;
        }   
    }
}