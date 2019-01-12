using MagicalLifeAPI.Components.Generic.Renderable;
using MagicalLifeAPI.Components.Resource;
using MagicalLifeAPI.Sound;
using MagicalLifeAPI.Visual.Rendering.AbstractVisuals;
using MagicalLifeAPI.World.Base;
using MagicalLifeAPI.World.Items;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace MagicalLifeAPI.World.Resources
{
    /// <summary>
    /// A maple tree.
    /// </summary>
    [ProtoContract]
    public class Corn : PlantBase
    {
        private static readonly string Name = "Corn";
        private const int MinimumCornYield = 1;
        private const int MaximumCornYield = 7;
        private const int DefaultCornDurability = 5;

        public static readonly int XOffset = Tile.GetTileSize().X / -2;
        public static readonly int YOffset = Tile.GetTileSize().Y * -3 / 2;

        public static StaticTexture CornPlant { get; set; }

        public Corn(int durability) : base(Name, durability)
        {
            // Determine corn yield
            Random random = new Random();
            int cornYield = random.Next(MinimumCornYield, MaximumCornYield);

            // initialize harvesting list
            List<Base.Item> list = new List<Base.Item>
            {
                new Log(cornYield, this.Durability)
            };

            this.HarvestingBehavior = new DropWhenCompletelyHarvested(list, SoundsTable.UIClick);
        }

        public Corn()
        {
            this.Durability = DefaultCornDurability;
        }

        public override AbstractHarvestable HarvestingBehavior { get; set; }

        public override List<AbstractVisual> GetVisuals()
        {
            List<AbstractVisual> ret = new List<AbstractVisual>
            {
                CornPlant
            };

            return ret;
        }
    }
}