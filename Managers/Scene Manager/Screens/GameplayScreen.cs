#region Using Statements
using System;
using System.Threading;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameStateManagement;
using System.Linq;
using System.Text;
#endregion

namespace GameStateManagementSample
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    public class GameplayScreen : GameScreen
    {
        GraphicsDeviceManager graphics;
        ContentManager content;
        SpriteFont gameFont;

        InputManager inputMgr;
        
        Random random = new Random();

        InputAction pauseAction;

        GameStateManagementGame game;

        EntityManager entityManager;

        IEntity entity; // a singular entity that can be requested

        private List<IEntity> coldEnemies; // a list that will contain all the enemies of type ColdVirus
        Boolean removeCold = false; // boolean to be set to true when an enemy is to be removed
        Boolean addCold = false; // boolean to be set to treu when an enemy is to be added

        private List<IEntity> strepEnemies; // a list that will contain all the enemies of type StrepThroatVirus
        Boolean removeStrep = false;
        Boolean addStrep = false;

        IEntity thisEnemy; // to be set to a singular instance of an enemy


        private List<IEntity> players; // a list that will contain the player so that it can be added and removed from the scene
        Boolean removePlayer = false;
        IEntity thisPlayer; // to be set to a singular instance of the player


        public List<IEntity> allies; // a list that will contain all the allies, in this case BloodCells
        Boolean removeAlly = false;
        IEntity thisAlly; // to be set to a singular instance of an ally


        private List<IEntity> bullets; // a list that will contain all bullets
        Boolean removeBullet = false;
        IEntity thisBullet; // to be set to a singular instance of a bullet


        public static Boolean isBulletBlue = true; // a boolean to be set to true if the bullet is blue

        IEntity background; // a reference to the background image

        public static int screenHeight = 0;
        public static int screenWidth = 0;

        // both to be used for the delay between each bullet fired
        private DateTime dateTime; 
        private TimeSpan timeSpan;

        // a set of variables for the score, to meet the requirement of DrawString method in SpriteBatch
        SpriteFont scoreFont;
        String scoreString;
        int scoreInt = 0;
        Vector2 scorePos;
        Vector2 scoreOrigin;
        Color scoreColor;
        float scoreSize;

        // a set of variables for the patient health, to meet the requirement of DrawString method in SpriteBatch
        SpriteFont healthFont;
        String healthString;
        int healthInt = 150;
        Vector2 healthPos;
        Vector2 healthOrigin;
        Color healthColor;
        float healthSize;

        Boolean isPlayerKill = false; // a boolean to detect whether the enemy died due to the player shooting it


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            game = new GameStateManagementGame();

            // initializing all lists
            coldEnemies = new List<IEntity>();
            strepEnemies = new List<IEntity>();
            allies = new List<IEntity>();
            players = new List<IEntity>();
            bullets = new List<IEntity>();

            entityManager = new EntityManager();

            inputMgr = new InputManager();

            pauseAction = new InputAction(new Keys[] { Keys.Escape }, true); // defining the key that will bring up the pause menu
        }

        // a method to set the values of all the variables for the score
        public void KeepScore()
        {
            scoreFont = ScreenManager.Font;
            scoreString = "Score: " + scoreInt;
            scoreOrigin = scoreFont.MeasureString(scoreString) / 2;
            scorePos = new Vector2(80, screenHeight - 20);
            scoreColor = Color.Black;
            scoreSize = 1.0f;
        }

        // a method to set the values of all the variables for the patient health
        public void KeepHealth()
        {
            healthFont = ScreenManager.Font;
            healthString = "Patient Health: " + healthInt;
            healthOrigin = scoreFont.MeasureString(healthString) / 2;
            healthPos = new Vector2(screenWidth - 180, screenHeight - 20);
            healthColor = Color.Black;
            healthSize = 1.0f;

        }

        // a method that handles that adds 'entity's into the list 'entities'
        public void Add()
        {
            players.Add(game.playerRequest(entity));

            coldEnemies.Add(game.coldRequest(entity));
            coldEnemies.Add(game.coldRequest(entity));

            strepEnemies.Add(game.strepRequest(entity));

            allies.Add(game.bloodRequest(entity));
            allies.Add(game.bloodRequest(entity));
            allies.Add(game.bloodRequest(entity));

            background = entityManager.AddEntity<Player1>(); // requesting an entity just to be used for the background image, could be any entity
        }

        // a method that gives obejcts in the list 'entities' position 
        public void listPosition()
        {
            players[0].Position = new Vector2(0, screenHeight / 2);

            coldEnemies[0].Position = new Vector2((screenWidth - 250), 150);
            coldEnemies[1].Position = new Vector2((screenWidth - 200), screenHeight - 220);

            strepEnemies[0].Position = new Vector2((screenWidth - 200), (screenHeight /2) - 50);

            allies[0].Position = new Vector2((screenWidth/ 2) + 200, (screenHeight / 2) + 100);
            allies[1].Position = new Vector2((screenWidth / 2) + 200, (screenHeight / 2) - 150);
            allies[2].Position = new Vector2((screenWidth / 2) - 200, (screenHeight / 2) - 30);

            background.Position = new Vector2(0,0);
        }

        // a method that gives obejcts in the list 'entities' a texture/image 
        public void listImage()
        {
            players[0].Image = content.Load<Texture2D>("NanoBlue");

            foreach(IEntity cold in coldEnemies)
                cold.Image = content.Load<Texture2D>("Cold_1");

            foreach (IEntity strep in strepEnemies)
                strep.Image = content.Load<Texture2D>("Strep_1");

            foreach (IEntity bloodCell in allies)
                bloodCell.Image = content.Load<Texture2D>("BloodCell");

            background.Image = content.Load<Texture2D>("GameBackground");
        }

        // a method to decide which bullet is currently being used
        public void currentBullet()
        {
            if (isBulletBlue == true) // if the bullet is blue
            {
                bullets.Add(entityManager.AddEntity<BlueBullet>()); // create an instance of a blue bullet
                timeSpan = new TimeSpan(0, 0, 0, 0, 200); // set the delay between each bullet
                players[0].Image = content.Load<Texture2D>("NanoBlue"); // the shipe is set to blue
            }
            else // if it is not
            {
                bullets.Add(entityManager.AddEntity<RedBullet>()); // create an instance of a red bullet
                timeSpan = new TimeSpan(0, 0, 0, 0, 500);
                players[0].Image = content.Load<Texture2D>("NanoRed");//the ship is set to red
            }
        }

        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void Activate(bool instancePreserved)
        {
            if (!instancePreserved)
            {
                if (content == null)
                    content = new ContentManager(ScreenManager.Game.Services, "Content");

                gameFont = content.Load<SpriteFont>("gamefont");

                screenHeight = ScreenManager.GraphicsDevice.Viewport.Height;
                screenWidth = ScreenManager.GraphicsDevice.Viewport.Width;

                KeepScore(); // call to set the score to appear

                KeepHealth(); // call to set the patients health to appear

                // these methods are only call during the activation of the GameplayScreen
                Add();
                listPosition();
                listImage();

                // adding the player to be a listener for these events
                inputMgr.AddListener(players[0].GetInput);
                inputMgr.AddListener(players[0].GetKeyUp);
                inputMgr.AddListener(players[0].GetSpacebar);

                // A real game would probably have more content than this sample, so
                // it would take longer to load. We simulate that by delaying for a
                // while, giving you a chance to admire the beautiful loading screen.
                Thread.Sleep(2000);

                // once the load has finished, we use ResetElapsedTime to tell the game's
                // timing mechanism that we have just finished a very long frame, and that
                // it should not try to catch up.
                ScreenManager.Game.ResetElapsedTime();
            }
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }

        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void Unload()
        {
            content.Unload();
        }

        

        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);
            
            Vector2 inputControl = Input.GetKeyboardInputDirection();
            // as long as 'IsActive' is true the following statements will function.
            // however if 'IsActive' is false, which would be in the case of the 'PauseMenuScreen' being active, they will not function and will be in a static state.
            if (IsActive)
            {
                inputMgr.Update();
                players[0].Update(); // giving this entity the functionality to be controlled by the player

                // if the bullets list is not null
                if (bullets != null)
                {
                    //if the boolean "IsBullet" is set to true" within the player class, which happen due to an event with the space bar, and the dateTime is more than or equal to the current real time
                    if ((players[0].IsBullet == true) && (dateTime <= DateTime.Now)) 
                    {
                        currentBullet(); // the current active bullet is created
                        dateTime = DateTime.Now + timeSpan; // the datetime is increased
                        players[0].IsBullet = false; // "IsBullet" is to be set to false, as to stop bullets spawning continuously
                    }
               
                    foreach (IEntity bullet in bullets)
                    {
                        if (bullet.IsBulletNew == true) // if there is a new bullet
                        {
                            bullet.Position = players[0].centrePoint() + new Vector2(70, -5); // it is to spawn from the front, middle of the player
                            bullet.IsBulletNew = false; // the bullet is no longer new
                        }

                        if (isBulletBlue == true) 
                        {
                            bullet.Image = content.Load<Texture2D>("Blue_Bullet"); // the bullet will be blue
                        }
                        else
                        {
                            bullet.Image = content.Load<Texture2D>("Red_Bullet"); // the bullet will be red
                        }

                        bullet.Update(); // the bullet will move as long as it exists

                        if((bullets.Count != 0) && ((coldEnemies.Count != 0)||(strepEnemies.Count != 0))) // if there are bullets and there are either cold and/or strep throat enemies in the lists
                        {
                            if (bullet.Position.X > (screenWidth)) // if the bullet moves off of the game screen
                            {
                                thisBullet = bullet; // thisBullet is set to be the instance of that bullet
                                removeBullet = true; // that bullet is set to be removed
                            }

                            foreach(IEntity cold in coldEnemies)
                            {
                                if (CollisionManager.RectangleCollision(bullet, cold)) // if a bullet collides with a cold enemy
                                {
                                    thisBullet = bullet; // thisBullet is set to be the instance of that bullet
                                    removeBullet = true;
                                    Console.WriteLine("before " + cold.Health + "-" + bullet.BulletDamage); // show the health before minus the bullet damage
                                    cold.Health -= bullet.BulletDamage; // do the math
                                    Console.WriteLine("after " + cold.Health); // show the health after

                                    if (cold.Health <= 0) // if the colds health is 0
                                    {
                                        thisEnemy = cold; // thisEnemy is set to be the instance of that cold enemy
                                        removeCold = true; // and it is set to be removed
                                        isPlayerKill = true; // this was due to the player killing it
                                    }

                                    Console.WriteLine("hit comfirmed"); // to confirm there was a collision
                                }
                            }

                            foreach (IEntity strep in strepEnemies)
                            {
                                if (CollisionManager.RectangleCollision(bullet, strep))
                                {
                                    thisBullet = bullet; // thisBullet is set to be the instance of that bullet
                                    removeBullet = true;
                                    Console.WriteLine("before " + strep.Health + "-" + bullet.BulletDamage); // show the health before minus the bullet damage
                                    strep.Health -= bullet.BulletDamage; // do the math
                                    Console.WriteLine("after " + strep.Health); // show the health after

                                    if (strep.Health <= 0)  // if the strep throats health is 0
                                    {
                                        thisEnemy = strep; // thisEnemy is set to be the instance of that strep throat enemy
                                        removeStrep = true; // and it is set to be removed
                                        isPlayerKill = true; // this was due to the player killing it
                                    }

                                    Console.WriteLine("hit comfirmed"); // to confirm there was a collision
                                }
                            }

                            foreach (IEntity bloodCell in allies)
                            {
                                if (CollisionManager.RectangleCollision(bullet, bloodCell)) // if a bullet collides with a blood cell
                                {
                                    thisBullet = bullet; // thisBullet is set to be the instance of that bullet
                                    removeBullet = true; // it is set to be removed

                                    thisAlly = bloodCell; // thisAlly is set to be the instance of that bloodCell
                                    removeAlly = true; // it is set to be removed

                                    Console.WriteLine("ally hit comfirmed"); // to confirm there was a collision
                                }
                            }
                        }      
                    }
                }

                foreach (IEntity strep in strepEnemies)
                {
                    if (CollisionManager.CircleCollision(players[0], strep)) // if the strep throat enemy can see the player
                    {
                        strep.Update(players[0].Position); // it will be set to follow the player
                    }
                    else // if it can't
                    {
                        strep.Update(); // it will be in an idle state
                    }
                }

                if (strepEnemies.Count < 1) // if there is less than 1 strep throat enemy, add a new one
                {
                    strepEnemies.Add(game.strepRequest(entity));
                    strepEnemies[0].Position = new Vector2((screenWidth - 200), (screenHeight / 2) - 50);
                    strepEnemies[0].Image = content.Load<Texture2D>("Strep_1");
                }

                if (removeStrep == true) // if the strep throat enemy is set to be removed
                {
                    entityManager.RemoveEntity<StrepThroatVirus>(strepEnemies, thisEnemy); // remove it
                    removeStrep = false; // it is no longer to be removed
                }

                foreach (IEntity cold in coldEnemies) 
                {
                    cold.Update(); // set to move left

                    if (cold.Position.X < 0) // if it moves off the screen
                    {
                        thisEnemy = cold; // thisEnemy is set to be the instance of that cold enemy
                        removeCold = true; // it is set to be removed
                    }
                }

                if (coldEnemies.Count < 2) // if there are less than 2 cold enemies in the scene, add a new one for the one that was removed
                {
                    coldEnemies.Add(game.coldRequest(entity)); 
                    coldEnemies[1].Position = new Vector2((screenWidth - 100), thisEnemy.Position.Y);
                    coldEnemies[1].Image = content.Load<Texture2D>("Cold_1");
                }

                if (removeCold == true) // if a cold is set to be removed
                {
                    entityManager.RemoveEntity<ColdVirus>(coldEnemies, thisEnemy); // remove it
                    removeCold = false; // it is no longer to be removed
                }

                foreach (IEntity player in players)
                {
                    foreach (IEntity strep in strepEnemies)
                    {
                        if (CollisionManager.RectangleCollision(player, strep)) // if the player and a strep throat enemy collide
                        {
                            player.Health -= 100; // the player loses this amount of health
                            thisEnemy = strep; // thisEnemy is set to be the instance of that strep throat enemy
                            removeStrep = true; // it is set to be removed
                        }
                    }

                    foreach (IEntity cold in coldEnemies)
                    {
                        if (CollisionManager.RectangleCollision(player, cold)) // if the player and a cold enemy collide
                        {
                            player.Health -= 50;// the player loses this amount of health
                            thisEnemy = cold; // thisEnemy is set to be the instance of that cold enemy
                            removeCold = true; // it is set to be removed
                        }
                    }

                    if (player.Health <= 0) // if the players health is 0
                    {
                        removePlayer = true; // the player is set to be removed
                        thisPlayer = player; // thisPlayer is set to an instance of the player
                    }
                }

                if (removePlayer == true) // if the player is set to be removed
                {
                    // it is to be removed as a listener from these events
                    inputMgr.RemoveListener(players[0].GetInput); 
                    inputMgr.RemoveListener(players[0].GetKeyUp);
                    inputMgr.RemoveListener(players[0].GetSpacebar);
                    entityManager.RemoveEntity<Player1>(players, thisPlayer); // remove the player
                    removePlayer = false; // it is no longer to removed
                }

                if (players.Count < 1) // if there is no player, go back to the main menu
                {
                    LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
                                                               new MainMenuScreen());
                }

                if (isPlayerKill == true) // if the player killed the enemy, add to the score
                {
                    scoreInt += 100;
                    scoreString = "Score: " + scoreInt;
                    isPlayerKill = false;
                }

                if (removeBullet == true) // if the bullet is set to be removed, remove it
                {
                    entityManager.RemoveEntity<Bullet>(bullets, thisBullet);
                    Console.WriteLine("generic bullet removed"); // check that it was removed
                    removeBullet = false;
                    thisBullet = null; // reset thisBullet
                }

                foreach (IEntity bloodCell in allies)
                {
                    foreach (IEntity strep in strepEnemies)
                    { 
                        if(CollisionManager.RectangleCollision(bloodCell, strep)) // if the strep throat enemy collides with the blood cell, the blood cell is set to be removed
                        {
                            thisAlly = bloodCell;
                            removeAlly = true;
                        }
                    }
                }

                if (removeAlly == true) // if the ally is set to be removed
                {
                    entityManager.RemoveEntity<BloodCell>(allies, thisAlly); // remove it
                    removeAlly = false;
                    healthInt -= 50; // the patient loses health
                    healthString = "Patient Health: " + healthInt; // show this
                    thisAlly = null; // reset thisAlly

                    if (healthInt <= 0) // if the patients health is 0, go back to the main menu
                    {
                        LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
                                                               new MainMenuScreen());
                    }
                }
            }
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            PlayerIndex player;

            if (pauseAction.Evaluate(input, ControllingPlayer, out player))
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
        }

        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, new Color(186,73,99), 0, 0);

            // Our player and enemy are both actually just text strings.
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            
            spriteBatch.Begin();

            background.Draw(spriteBatch);

            foreach (IEntity cold in coldEnemies)
                cold.Draw(spriteBatch); 

            foreach (IEntity strep in strepEnemies)
                strep.Draw(spriteBatch); 

            foreach (IEntity bloodCell in allies)
                bloodCell.Draw(spriteBatch); 
            
            foreach (IEntity bullet in bullets)
                bullet.Draw(spriteBatch);

            foreach (IEntity player in players)
                player.Draw(spriteBatch);

            spriteBatch.DrawString(scoreFont, scoreString, scorePos, scoreColor, 0, scoreOrigin, scoreSize, SpriteEffects.None, 0);
            spriteBatch.DrawString(healthFont, healthString, healthPos, healthColor, 0, healthOrigin, healthSize, SpriteEffects.None, 0);

            spriteBatch.End();
        }
    }
}
