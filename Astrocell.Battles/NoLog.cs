using Astrocell.Battles.Battles;

namespace Astrocell.Battles
{
    public sealed class NoLog : ILog
    {
        public void Write(string msg)
        {
        }
    }
}
