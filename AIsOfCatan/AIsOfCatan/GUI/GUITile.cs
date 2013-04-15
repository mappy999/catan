﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TroyXnaAPI;

namespace AIsOfCatan
{
    class GUITile : TXADrawableComponent
    {
        public const int TILE_WIDTH = (int) (222 * GUIControl.SCALE);
        public const int TILE_HEIGHT = (int) (255*GUIControl.SCALE);
        private static readonly float TileShift = (float)Math.Sqrt(Math.Pow(TILE_WIDTH, 2) - Math.Pow((TILE_WIDTH / 2), 2));

        //private static readonly Vector2 EAST = new Vector2(0, TILE_WIDTH);
        //private static readonly Vector2 WEST = new Vector2(0, -TILE_WIDTH);
        //private static readonly Vector2 S_EAST = new Vector2(TileShift, TILE_WIDTH / 2);
        //private static readonly Vector2 N_EAST = new Vector2(-TileShift, TILE_WIDTH / 2);
        //private static readonly Vector2 S_WEST = new Vector2(TileShift, -TILE_WIDTH / 2);
        //private static readonly Vector2 N_WEST = new Vector2(-TileShift, -TILE_WIDTH / 2);

        private Vector2 numberPos;
        private Vector2 textPos;
        private readonly Color valueColour;

        private Board.Tile Tile { get; set; }

        public GUITile(int x, int y, Board.Tile tile) : 
            base(
                new Vector2((float) ((x+0.5+(y % 2 == 0 ? 0.5 : 0))*TILE_WIDTH), (float) ((0.66+y)*TileShift)),
                GetTexture(tile.Terrain)
            )
        {
            Tile = tile;
            //Y = y;
            //X = x;
            NumAreaAndTextPos();
            valueColour = (Tile.Value == 6 || Tile.Value == 8 ? Color.Red : Color.Black);
        }

        private static Texture2D GetTexture(Terrain ter)
        {
            return TXAGame.TEXTURES["T_" + ter];
        }

        protected override void DoUpdate(GameTime time)
        {
            //throw new NotImplementedException();
        }

        protected override void UpdateRect()
        {
            int width = (int)Math.Round(Texture.Width * GUIControl.SCALE);
            int height = (int)Math.Round(Texture.Height * GUIControl.SCALE);

            Area = new Rectangle(
                ((int)Math.Round(Position.X)) - (width / 2),
                ((int)Math.Round(Position.Y)) - (height / 2),
                width,
                height);
            
        }

        private void NumAreaAndTextPos()
        {
            int width = TXAGame.TEXTURES["TO_Number"].Width;
            int height = TXAGame.TEXTURES["TO_Number"].Height;

            numberPos = new Vector2(
                (Position.X/GUIControl.SCALE) - (width/2),
                (Position.Y / GUIControl.SCALE) - (height / 2));

            Vector2 measurementValue = TXAGame.ARIAL.MeasureString(Tile.Value.ToString(CultureInfo.InvariantCulture));

            textPos = new Vector2((Position.X / GUIControl.SCALE - (measurementValue.X / 2)), (Position.Y / GUIControl.SCALE - (measurementValue.Y / 2)));
        }

        protected override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            if (IsTileNumbered(Tile))
            {
                batch.Draw(TXAGame.TEXTURES["TO_Number"],numberPos,null,Color.Wheat,0f,numberPos,GUIControl.SCALE,SpriteEffects.None,0.1f);
                batch.DrawString(TXAGame.ARIAL, Tile.Value.ToString(CultureInfo.InvariantCulture), textPos, valueColour,0f,textPos,GUIControl.SCALE,SpriteEffects.None, 0f);
            }
            
        }

        private static bool IsTileNumbered(Board.Tile tile)
        {
            switch (tile.Terrain)
            {
                case Terrain.Pasture:
                case Terrain.Mountains:
                case Terrain.Hills:
                case Terrain.Fields:
                case Terrain.Forest:
                    return true;
                default:
                    return false;
            }
            //
        }
    }
}