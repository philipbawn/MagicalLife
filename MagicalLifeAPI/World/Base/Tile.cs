﻿using MagicalLifeAPI.Asset;
using MagicalLifeAPI.DataTypes;
using MagicalLifeAPI.Entities;
using MagicalLifeAPI.Universal;
using MagicalLifeAPI.World.Base;
using System;
using System.Collections.Generic;

namespace MagicalLifeAPI.World
{
    /// <summary>
    /// Every tile that implements this class must provide a parameterless version of itself for reflection purposes. That constructor will not be used during gameplay.
    /// </summary>
    public abstract class Tile : Unique
    {
        /// <summary>
        /// Initializes a new tile object.
        /// </summary>
        /// <param name="location">The 3D location of this tile in the map.</param>
        /// <param name="movementCost">This value is the movement cost of walking on this tile. It should be between 1 and 100</param>
        protected Tile(Point3D location, int movementCost)
        {
            this.Location = location;
            this.MovementCost = movementCost;
            Tile.TileCreatedHandler(new TileEventArg(this));
            this.TextureIndex = AssetManager.GetTextureIndex(this.GetTextureName());
        }

        /// <summary>
        /// This constructor is used during loading/reflection only.
        /// </summary>
        protected Tile()
        {
        }

        /// <summary>
        /// The index of the texture in our asset manager.
        /// </summary>
        public int TextureIndex { get; set; }

        /// <summary>
        /// Returns the name of the biome that this tile belongs to.
        /// </summary>
        public string BiomeName { get; }

        /// <summary>
        /// Returns the movement cost of this tile.
        /// Should be between 1-100.
        /// </summary>
        public int MovementCost { get; protected set; }

        /// <summary>
        /// The size, in pixels of how big each tile is.
        /// </summary>
        /// <returns></returns>
        public static Microsoft.Xna.Framework.Point GetTileSize()
        {
            return new Microsoft.Xna.Framework.Point(64, 64);
        }

        /// <summary>
        /// Returns the name of this tile.
        /// </summary>
        /// <returns></returns>
        public abstract string GetName();

        /// <summary>
        /// The resources that can be found in this tile.
        /// </summary>
        public List<Resource> Resources { get; set; } = new List<Resource>();

        public List<Vegetation> Plants { get; set; } = new List<Vegetation>();

        /// <summary>
        /// The location of this tile in the tilemap.
        /// </summary>
        public Point3D Location { get; protected set; }

        /// <summary>
        /// The entity that is in this tile. Is null if there is not an entity in this tile.
        /// </summary>
        public Living Living { get; set; } = null;

        /// <summary>
        /// Raised when the world is finished generating for the first time.
        /// </summary>
        public static event EventHandler<TileEventArg> TileCreated;

        /// <summary>
        /// Raises the world generated event.
        /// </summary>
        /// <param name="e"></param>
        public static void TileCreatedHandler(TileEventArg e)
        {
            EventHandler<TileEventArg> handler = TileCreated;
            if (handler != null)
            {
                handler(World.mainWorld, e);
            }
        }

        public abstract string GetTextureName();
    }
}