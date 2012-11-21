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
    class Character : Sprite
    {
        KeyboardState pKS; //previousKeyboardState

        public Character(ContentManager cM)
        {
            AssetName = "Char";
            Scale = 0.5f;
            base.LoadContent(cM, AssetName); //takes care of sSpriteTexture + sSpriteSize IMPORTANT: AFTER SCALE DECLARATION
            Position = new Vector2(200, 200);
            Speed = 100;
            Origin = new Vector2((Size.Width / 2), (Size.Height / 2));
            Layer = 0.1f;
            Rotation = 0.0f;
            Direction = new Vector2(0, 0);
            CurrentState = State.Moving;
        }

        public void Update(GameTime gT, Cursor cur)
        {
            KeyboardState cKS = Keyboard.GetState(); //currentKeyboardState
            UpdateMovement(cKS);
            pKS = cKS;
            base.Update(gT, Speed, Direction);

            Rotation = FindRotationDirection(this, cur);
        }

        private void UpdateMovement(KeyboardState cKS) //currentKeyboardState
        {
            Direction = Vector2.Zero;

            if (Keyboard.GetState().GetPressedKeys().Length > 0)
            {
                CurrentState = State.Moving;

                if (cKS.IsKeyDown(Keys.Left) || cKS.IsKeyDown(Keys.A))
                {
                    Direction.X = -1;
                }
                else if (cKS.IsKeyDown(Keys.Right) || cKS.IsKeyDown(Keys.D))
                {
                    Direction.X = 1;
                }

                if (cKS.IsKeyDown(Keys.Up) || cKS.IsKeyDown(Keys.W))
                {
                    Direction.Y = -1;
                }
                else if (cKS.IsKeyDown(Keys.Down) || cKS.IsKeyDown(Keys.S))
                {
                    Direction.Y = 1;
                }
            }
            else CurrentState = State.Standing;

        }
    }
}

