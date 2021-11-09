using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Util;

namespace BreakoutGameMonogame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        //Services
        InputHandler input;
        GameConsole console;

        //Components
        BlockManager bm;
        Paddle paddle;
        Ball ball;

        ScoreManager score;
        SpriteFont sf;
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Services
            input = new InputHandler(this);
            console = new GameConsole(this);
            this.Components.Add(console);
#if RELEASE
            console.ToggleConsole(); //close the console
#endif


            score = new ScoreManager(this);


            //GameComponents
            ball = new Ball(this); //Ball first paddle and block manager depend on ball
            paddle = new Paddle(this, ball);


            bm = new BlockManager(this, ball,score);
            this.Components.Add(input);
            this.Components.Add(score);
            this.Components.Add(ball);
            this.Components.Add(paddle);
            this.Components.Add(bm);
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

            base.LoadContent();
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (input.WasKeyPressed(Keys.Space))
            {
                ScoreManager.SetupNewGame();
                ball.resetBall(gameTime);
                paddle.SetInitialLocation();
                bm.ClearAllBlocksOnScreen();
                bm.LoadLevel();
            }
            //if(ScoreManager.Lives == 0)
            //{
            //    Components.Remove
            //}
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
