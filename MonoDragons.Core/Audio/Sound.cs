
namespace MonoDragons.Core.Audio
{
    public class Sound
    {
        public static readonly Sound Missing = new Sound { Name = "missingsound" };

        private string _suffix;

        public string Prefix { get; set; } = "";
        public float Volume { get; set; } = 1.0f;
        
        public string Name
        {
            get { return Prefix + _suffix; }
            set { _suffix = value; }
        }
    }
}
