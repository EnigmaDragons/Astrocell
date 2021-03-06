﻿using Astrocell.Battles.Characters;
using MonoDragons.Core.Common;
using MonoDragons.Core.Common.Reflection;
using MonoDragons.Core.IO;

namespace Astrocell.Battles.Equipments
{
    public struct Equipment : IEquipment
    {
        public int this[Extrinsic stat] => this.GetPropertyValue<int>(stat.ToString()).Value;

        public int MaxHp { get; set; }
        public int Attack { get; set; }
        public int Magic { get; set; }
        public int Resistance { get; set; }
        public int Defense { get; set; }
        public int Draw { get; set; }
        public int ActionPoints { get; set; }
        public int StartingEnergy { get; set; }
        public int StartingCards { get; set; }

        public static Equipment Load(string name)
        {
            return new JsonIo().Load<Equipment>($"./Content/Equipment/{name}.json");
        }
    }
}
