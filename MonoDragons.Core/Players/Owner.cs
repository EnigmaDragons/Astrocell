using System;
using System.Diagnostics;
using MonoDragons.Core.Common;

namespace MonoDragons.Core.Players
{
    public sealed class Owner
    {
        public Optional<string> Id { get; set; }

        public bool IsOtherPlayer(string id)
        {
            var isOtherPlayer = true;
            if (!Id.HasValue)
                isOtherPlayer = false;
            if (Id.Value.Equals(id, StringComparison.InvariantCultureIgnoreCase))
                isOtherPlayer = false;

            Debug.WriteLine($"Current Player {id}, Owner {Id}, IsOtherPlayer {isOtherPlayer}");
            return isOtherPlayer;
        }
    }
}
