namespace MonoDragons.Core.Logs
{
    public sealed class NoLog : ILog
    {
        public void Write(string msg)
        {
        }
    }
}
