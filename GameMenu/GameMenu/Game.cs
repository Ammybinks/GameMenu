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
using Microsoft.Xna.Framework.Storage;

using SpriteLibrary;

namespace ExperimentalFloss
{
    public partial class Game : Microsoft.Xna.Framework.Game
    {

        static PrintString printString = new PrintString();
        static IsPressed isPressed = new IsPressed();

        public GraphicsDeviceManager graphics;

        public SpriteBatch spriteBatch;

        public int[] functionIndex = new int[] {1, 2, 3}; // An array holding an index which certain functions will use to discern what values to return to what function, 
                                                          // useful if a function is being used more than once in a loop
        public bool keyboardActive = true; // For if the keyboard is in use, as opposed to a controller

        public KeyboardState currentKeyState;
        public KeyboardState oldKeyState;

        public GamePadState currentPadState;
        public GamePadState oldPadState;

        public MouseState currentMouseState = Mouse.GetState();
        public MouseState oldMouseState;

        public Sprite effectSprite;
        public Texture2D effectTexture;

        public Sprite playerSprite;
        public Texture2D playerTexture;

        public Sprite mainMenuSprite;
        public Texture2D mainMenuTexture;

        public Sprite pointerSprite;
        public Texture2D pointerTexture;

        public Sprite fontSheetSprite;
        public Texture2D fontSheetTexture;

        public float mainMenuUpperLeft;

        public int pointIndex;

        public int elapsedMilliseconds = 0;

        public LinkedList<Sprite> sentenceList = new LinkedList<Sprite>();

        public bool toMenu = true;
        public bool toGame = true;
        public bool toPause = true;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        public enum GameScreen
        {
            TITLE = 0,
            GAME = 1,
            PAUSED = 2,
        }
        public GameScreen CurrentScreen = GameScreen.TITLE;

        public enum PointingAt
        {
            PLAY = 0,
            OPTIONS = 1,
            BONUS = 2,
            EXIT = 3
        }
        public PointingAt CurrentPoint;

        public enum GameTile
        {

        }
        public GameTile CurrentTile;

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            this.IsMouseVisible = true;
            // TODO: Add your initialization logic here
            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 30.0f);
            this.IsFixedTimeStep = false;

            Window.AllowUserResizing = true;

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            playerTexture = this.Content.Load<Texture2D>("Images\\Player");
            effectTexture = this.Content.Load<Texture2D>("Images\\Pixel");
            mainMenuTexture = this.Content.Load<Texture2D>("Images\\Main Menu");
            pointerTexture = this.Content.Load<Texture2D>("Images\\Pointer");

            fontSheetTexture = this.Content.Load<Texture2D>("Fonts\\FontSheet");

            mainMenuUpperLeft = GraphicsDevice.Viewport.Width / 2 - mainMenuTexture.Width / 2;
            mainMenuSprite = new Sprite();
            mainMenuSprite.SetTexture(mainMenuTexture);
            mainMenuSprite.UpperLeft.X = mainMenuUpperLeft;
            mainMenuSprite.UpperLeft.Y = 10;

            pointerSprite = new Sprite();
            pointerSprite.SetTexture(pointerTexture);
            pointerSprite.UpperLeft = new Vector2(10, 170);

            playerSprite = new Sprite();
            playerSprite.SetTexture(playerTexture);
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

            if (CurrentScreen == GameScreen.TITLE)
            {
                UpdateTitle(gameTime);
            }
            if (CurrentScreen == GameScreen.PAUSED)
            {
                UpdatePaused(gameTime);
            }
            if (CurrentScreen == GameScreen.GAME)
            {
                UpdateGame(gameTime);
            }
            // TODO: Add your update logic here

            base.Update(gameTime);

        }

        void UpdateTitle(GameTime gameTime)
        {

            elapsedMilliseconds -= gameTime.ElapsedGameTime.Milliseconds;

            if (toMenu)
            {
                //Runs once every time the game returns to the menu (not on every loop), use this for single functions
                printString.Do(this, new Vector2(65, 180), "start game", 0.1f, 35, fontSheetSprite, fontSheetTexture);
                printString.Do(this, new Vector2(65, 230), "options", 0.1f, 35, fontSheetSprite, fontSheetTexture);
                printString.Do(this, new Vector2(58, 280), "bonus content", 0.1f, 35, fontSheetSprite, fontSheetTexture);
                printString.Do(this, new Vector2(65, 330), "exit", 0.1f, 35, fontSheetSprite, fontSheetTexture);

                printString.Do(this, new Vector2(0, 0), "Hello", 0.1f, 35, fontSheetSprite, fontSheetTexture);

                toMenu = false;
            }

            currentKeyState = Keyboard.GetState();

            if (oldKeyState == null)
                oldKeyState = currentKeyState;

            currentPadState = GamePad.GetState(PlayerIndex.One);

            if (oldPadState == null)
                oldPadState = currentPadState;

            if ((currentKeyState.IsKeyDown(Keys.Down))) // For navigating the Main Menu     && (oldKeyState.IsKeyUp(Keys.Down)
            {
                if (pointIndex < 3)
                    pointIndex++;
                else
                    pointIndex = 0;

            }
            if ((currentKeyState.IsKeyDown(Keys.S)) && (oldKeyState.IsKeyUp(Keys.S))) // Alternate button navigation (WASD) ((Subject to change when options are up))
            {
                if (pointIndex < 3)
                    pointIndex++;
                else
                    pointIndex = 0;
            }
            if ((currentKeyState.IsKeyDown(Keys.Up)) && (oldKeyState.IsKeyUp(Keys.Up))) // For navigating the Main Menu
            {
                if (pointIndex > 0)
                    pointIndex--;
                else
                    pointIndex = 3;
            }
            if ((currentKeyState.IsKeyDown(Keys.W)) && (oldKeyState.IsKeyUp(Keys.W))) // Alternate button navigation (WASD) ((Subject to change when options are up))
            {
                if (pointIndex > 0)
                    pointIndex--;
                else
                    pointIndex = 3;
            }


            if (pointIndex == 0)
            {
                CurrentPoint = PointingAt.PLAY;

                pointerSprite.UpperLeft = new Vector2(10, 170);
            }
            if (pointIndex == 1)
            {
                CurrentPoint = PointingAt.OPTIONS;

                pointerSprite.UpperLeft = new Vector2(10, 220);
            }
            if (pointIndex == 2)
            {
                CurrentPoint = PointingAt.BONUS;

                pointerSprite.UpperLeft = new Vector2(10, 270);
            }
            if (pointIndex == 3)
            {
                CurrentPoint = PointingAt.EXIT;

                pointerSprite.UpperLeft = new Vector2(10, 320);
            }

            if ((currentKeyState.IsKeyDown(Keys.Enter)) && (oldKeyState.IsKeyUp(Keys.Enter)))
            {
                if (CurrentPoint == PointingAt.PLAY)
                {

                }
                if (CurrentPoint == PointingAt.OPTIONS)
                {

                }
                if (CurrentPoint == PointingAt.BONUS)
                {

                }
                if (CurrentPoint == PointingAt.EXIT)
                {
                    Exit();
                }
            }

            oldKeyState = currentKeyState;
        }
        void UpdatePaused(GameTime gameTime)
        {

        }
        void UpdateGame(GameTime gameTime)
        {
            if (toGame)
            {
                playerSprite.UpperLeft = new Vector2(0, 0);

                pointerSprite.UpperLeft = new Vector2(0, 200);

                toGame = false;
            }
            if (playerSprite.IsCollided(pointerSprite) == true)
            {
                playerSprite.SetVelocity(0, 0);
            }
            else
            {
                playerSprite.Accelerate(0.0f, 0.05f);

                playerSprite.Move();
            }
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            foreach (Sprite letter in sentenceList) // Handles drawing for <PrintString.cs>
            {
                letter.Draw(spriteBatch);
            }

            if (CurrentScreen == GameScreen.TITLE)
            {
                DrawTitle(gameTime);
            }
            if (CurrentScreen == GameScreen.GAME)
            {
                DrawGame(gameTime);
            }
            if (CurrentScreen == GameScreen.PAUSED)
            {
                DrawPaused(gameTime);
            }

            spriteBatch.End();

            base.Draw(gameTime);

        }
        void DrawTitle(GameTime gameTime)
        {
            mainMenuSprite.Draw(spriteBatch);

            pointerSprite.Draw(spriteBatch);
        }
        void DrawGame(GameTime gameTime)
        {
            pointerSprite.Draw(spriteBatch);

            playerSprite.Draw(spriteBatch);
        }
        void DrawPaused(GameTime gameTime)
        {
            DrawGame(gameTime);
        }
    }
}
