using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace BreakoutGameMonogame
{
    public enum BlockState { Earth, Red, Yellow, Normal, Hit, Broken }

    public class Block
    {
        protected int hitCount; //Future use maybe should change state?
        protected uint blockID;

        protected static uint blockCount;

        public BlockState BlockState { get; set; }

        public Block(int hit)
        {
            this.BlockState = BlockState.Normal;
            blockCount++;
            this.blockID = blockCount;
            hitCount = hit;
        }

        public virtual void Hit()
        {
            this.hitCount++;
            this.UpdateBlockState();
        }

        public virtual void UpdateBlockState()
        {
            switch (this.hitCount)
            {
                case 0:
                    this.BlockState = BlockState.Earth;
                    break;
                case 1:
                    this.BlockState = BlockState.Red;
                    break;
                case 2:
                    this.BlockState = BlockState.Yellow;
                    break;
                case 3:
                    this.BlockState = BlockState.Normal;
                    break;
                case 4:
                    this.BlockState = BlockState.Hit;
                    break;
                case 5:
                    this.BlockState = BlockState.Broken;
                    break;
            }

        }
    
    }
}

