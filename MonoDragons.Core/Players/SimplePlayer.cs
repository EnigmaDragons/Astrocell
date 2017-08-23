using System;

namespace MonoDragons.Core.Players
{
    public sealed class SimplePlayer
    {
        public string Id { get; }
        public string Name { get; }

        public SimplePlayer(string name)
            : this (name, Guid.NewGuid()) { }

        public SimplePlayer(string name, Guid id)
        {
            Id = id.ToString();
            Name = name;
        }
    }
}
