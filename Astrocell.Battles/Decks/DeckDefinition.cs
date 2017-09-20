using MonoDragons.Core.Common;
using MonoDragons.Core.IO;

namespace Astrocell.Battles.Decks
{
    public struct DeckDefinition
    {
        public string Name { get; set; }
        public Map<string, int> CardCounts { get; set; }

        public static DeckDefinition Load(string name)
        {
            return new JsonIo().Load<DeckDefinition>($"./Content/Decks/{name}.deck");
        }

        public void SaveAs(string name)
        {
            new JsonIo().Save($"./Content/Decks/{name}.deck", this);
        }
    }
}
