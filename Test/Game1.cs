using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GDS_FirstPotion
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int timer = 0, timer2 = 0;
        Character mainCharacter;
        Cursor cursor;
        Turret turret1, turret2, turret3;
        List<Bullet> bullets = new List<Bullet>();
        List<Enemy> enemies = new List<Enemy>();

        KeyboardState lastKey;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            turret1 = new Turret(this.Content, new Vector2(150, 200));
            turret2 = new Turret(this.Content, new Vector2(400, 400));
            turret3 = new Turret(this.Content, new Vector2(650, 150));
            // TODO: use this.Content to load your game content here
            mainCharacter = new Character(this.Content);
            cursor = new Cursor(this.Content);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            mainCharacter.Update(gameTime, cursor);
            cursor.Update(gameTime);
            SpawnEnemies(gameTime);
            UpdateTurrets(gameTime);
            UpdateTurrets(gameTime);
            UpdateTurrets(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && lastKey.IsKeyUp(Keys.Space))
                mainCharacter.Shoot(Content, cursor, bullets);

            lastKey = Keyboard.GetState();
            UpdateEnemies(gameTime);
            UpdateBullets(gameTime);


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkCyan);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.BackToFront, null);
            mainCharacter.Draw(this.spriteBatch);
            cursor.Draw(this.spriteBatch);
            turret1.Draw(spriteBatch);
            turret2.Draw(spriteBatch);
            turret3.Draw(spriteBatch);
            foreach (Bullet bullet in bullets)
            {
                bullet.Draw(this.spriteBatch);
            }
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(this.spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }








        public void UpdateBullets(GameTime gT)
        {
            foreach (Bullet bullet in bullets)
            {
                bullet.Update(gT);
                if (Vector2.Distance(bullet.Position, bullet.StartingPoint) > 300)
                {
                    bullet.IsVisible = false;
                }
            }
            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].IsVisible == false)
                {
                    bullets.RemoveAt(i);
                    i--;
                }
            }
        }

        public void SpawnEnemies(GameTime gT)
        {
            if (timer == 25)
            {
                Enemy newEnemy = new Enemy(this.Content, mainCharacter);
                enemies.Add(newEnemy);
                timer = 0;
            }
            else timer++;
        }

        bool CheckCollision(Sprite A, Sprite B)
        {
            if (A.BoundingBox.Intersects(B.BoundingBox))
                return true;
            return false;
        }

        public void UpdateEnemies(GameTime gT)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Update(gT, mainCharacter);
                for (int j = 0; j < bullets.Count; j++)
                {
                    if ((i != -1) && (CheckCollision(enemies[i], bullets[j])))
                    {
                        bullets.RemoveAt(j);
                        enemies.RemoveAt(i);
                        i--;
                        j--;
                    }
                }
            }
        }

        public void UpdateTurrets(GameTime gT)
        {
            turret1.Update(gT, enemies);
            if ((timer2 >= 200) && (enemies.Count> 0))
            {
                Sprite target = turret1.FindClosest(turret1, enemies);
                turret1.Shoot(Content, target, bullets);
            }

            turret2.Update(gT, enemies);
            if ((timer2 >= 200) && (enemies.Count > 0))
            {
                Sprite target = turret2.FindClosest(turret2, enemies);
                turret2.Shoot(Content, target, bullets);
            }

            turret3.Update(gT, enemies);
            if ((timer2 >= 200) && (enemies.Count > 0))
            {
                Sprite target = turret3.FindClosest(turret3, enemies);
                turret3.Shoot(Content, target, bullets);
                timer2 = 0;
            }
            timer2++;
        }
    }
}

