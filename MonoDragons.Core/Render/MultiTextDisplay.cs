using System.Collections.Generic;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.Render
{
    public sealed class MultiTextDisplay : EntityComponent
    {
        public List<TextDisplay> Displays { get; set; }
    }
}
