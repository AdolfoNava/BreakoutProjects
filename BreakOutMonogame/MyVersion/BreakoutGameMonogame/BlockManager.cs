using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BreakoutGameMonogame
{
    class BlockManager : DrawableGameComponent
    {
        public List<MonogameBlock> Blocks { get; private set; } //List of Blocks the are managed by Block Manager
        //Dependancy on Ball
        Ball ball;

        List<MonogameBlock> blocksToRemove; //list of block to remove probably because they were hit

        /// <summary>
        /// BlockManager hold a list of blocks and handles updating, drawing a block collision
        /// </summary>
        /// <param name="game">Reference to Game</param>
        /// <param name="ball">Refernce to Ball for collision</param>
        public BlockManager(Game game, Ball b,ScoreManager score)
            : base(game)
        {
            this.Blocks = new List<MonogameBlock>();
            this.blocksToRemove = new List<MonogameBlock>();
            this.ball = b;
        }

        public override void Initialize()
        {

            LoadLevel();
            base.Initialize();
        }

        /// <summary>
        /// Replacable Method to Load a Level by filling the Blocks List with Blocks
        /// </summary>
        public virtual void LoadLevel()
        {
            switch (ScoreManager.Level)
            {
                case 1:
                    CreateBlockArrayByWidthAndHeight(24, 2, 1,3);
                    break;
                case 2:
                    CreateBlockArrayByWidthAndHeight(24, 3, 1,2);
                    break;
                case 3:
                    CreateBlockArrayByWidthAndHeight(24, 4, 1,1);
                    break;
                case 4:
                    CreateBlockArrayByWidthAndHeight(24, 5, 1,0);
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Simple Level lays out multiple levels of blocks
        /// </summary>
        /// <param name="width">Number of blocks wide</param>
        /// <param name="height">Number of blocks high</param>
        /// <param name="margin">space between blocks</param>
        private void CreateBlockArrayByWidthAndHeight(int width, int height, int margin,int startingblockforlevel)
        {
            MonogameBlock b;
            
            //Create Block Array based on with and hieght
            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    b = new MonogameBlock(this.Game,startingblockforlevel);
                    b.Initialize();
                    b.Location = new Vector2(5 + (w * b.SpriteTexture.Width + (w * margin)), 50 + (h * b.SpriteTexture.Height + (h * margin)));
                    Blocks.Add(b);
                }
            }
        }

        bool reflected; //the ball should only reflect once even if it hits two bricks
        public override void Update(GameTime gameTime)
        {
            this.reflected = false; //only reflect once per update
            UpdateCheckBlocksForCollision(gameTime);
            UpdateBlocks(gameTime);
            UpdateRemoveDisabledBlocks();

            if (Blocks.Count == 0)
            {
                ball.resetBall(gameTime);
                ScoreManager.NextLevel();
                LoadLevel();
            }
            base.Update(gameTime);
        }

        private void UpdateBlocks(GameTime gameTime)
        {
            foreach (var block in Blocks)
            {
                block.Update(gameTime);
            }
        }

        /// <summary>
        /// Removes disabled blocks from list
        /// </summary>
        private void UpdateRemoveDisabledBlocks()
        {
            //remove disabled blocks
            foreach (var block in blocksToRemove)
            {
                Blocks.Remove(block);
                ScoreManager.Score++;
            }
            blocksToRemove.Clear();
        }
        public void ClearAllBlocksOnScreen()
        {
            Blocks.Clear();
        }
        private void UpdateCheckBlocksForCollision(GameTime gameTime)
        {
            foreach (MonogameBlock b in Blocks)
            {
                if (b.Enabled) //Only chack active blocks
                {
                    b.Update(gameTime); //Update Block
                    //Ball Collision
                    if (b.Intersects(ball)) //chek rectagle collision between ball and current block 
                    {
                        //hit
                        b.HitByBall(ball);
                        ball.Speed += 2;
                        if (b.BlockState == BlockState.Broken)
                            blocksToRemove.Add(b);  //Ball is hit add it to remove list
                        if (!reflected) //only reflect once
                        {
                            ball.Reflect(b);
                            this.reflected = true;
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Block Manager Draws blocks they don't draw themselves
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            foreach (var block in this.Blocks)
            {
                if (block.Visible)   //respect block visible property
                    block.Draw(gameTime);
            }
            base.Draw(gameTime);
        }
    }
}
