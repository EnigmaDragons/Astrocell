namespace MonoDragons.Core.KeyboardControls
{
    public class TypingInput
    {
        public bool IsActive { get; set; }
        public int MaxChars { get; set; } = 32;
        public string Value { get; set; } = "";

        public void Append(string val)
        {
            if (Value.Length < MaxChars)
                Value += val;
        }

        public void Backspace()
        {
            if (Value.Length > 0)
                Value = Value.Remove(Value.Length - 1);
        }
    }
}
