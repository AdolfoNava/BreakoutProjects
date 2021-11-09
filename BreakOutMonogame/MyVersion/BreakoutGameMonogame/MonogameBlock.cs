using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BreakoutGameMonogame
{
    public class MonogameBlock : DrawableSprite
    {
        protected Block block;

        protected string earthBlockName,redBlockName,yellowBlockName, NormalTextureName, HitTextureName; 
        protected Texture2D earthBlockTexture, redBlockTexture, yellowBlockTexture, NormalTexture, HitTexture;

        private BlockState blockstate;
        public BlockState BlockState
        {
            get { return this.block.BlockState; } //encapulsate block.BlockState
            set { this.block.BlockState = value; }
        }

        public MonogameBlock(Game game, int value)
        : base(game)
        {
            this.block = new Block(value);
            NormalTextureName = "block_blue";
            HitTextureName = "block_bubble";
            earthBlockName = "block_earth";
            redBlockName = "block_red";
            yellowBlockName = "block_yellow";

        }

        protected virtual void updateBlockTexture()
        {

            this.blockstate = this.block.BlockState;
            switch (block.BlockState)
            {
                case BlockState.Earth:
                    Visible = true;
                    spriteTexture = earthBlockTexture;
                        break;
                case BlockState.Red:
                    Visible = true;
                    spriteTexture = redBlockTexture;
                    break;
                case BlockState.Yellow:
                    Visible = true;
                    spriteTexture = yellowBlockTexture;
                    break;
                case BlockState.Normal:
                    this.Visible = true;
                    this.spriteTexture = NormalTexture;
                    break;
                case BlockState.Hit:
                    this.spriteTexture = HitTexture;
                    break;
                case BlockState.Broken:
                    this.spriteTexture = NormalTexture;
                    //this.enabled = false;
                    this.Visible = false; //don't show block

                    break;
            }
        }

        protected override void LoadContent()
        {
            this.NormalTexture = this.Game.Content.Load<Texture2D>(NormalTextureName);
            this.HitTexture = this.Game.Content.Load<Texture2D>(HitTextureName);
            earthBlockTexture = this.Game.Content.Load<Texture2D>(earthBlockName);
            yellowBlockTexture = this.Game.Content.Load<Texture2D>(yellowBlockName);
            redBlockTexture = this.Game.Content.Load<Texture2D>(redBlockName);
            updateBlockTexture(); //notice this is in loadcontent not the constuctor
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            block.UpdateBlockState();
            UnityBlockUpdate();
        }

        protected virtual void UnityBlockUpdate()
        {
            updateBlockTexture();
        }

        public void HitByBall(Ball ball)
        {
            this.block.Hit();

        }
    }
}
