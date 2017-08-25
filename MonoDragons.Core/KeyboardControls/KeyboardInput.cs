using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Common;
using MonoDragons.Core.Entities;

namespace MonoDragons.Core.KeyboardControls
{
    public sealed class KeyboardInput : ISystem
    {
        private const string Backspace = "AddBackspace";

        public void Update(IEntities entities, TimeSpan delta)
        {
            Update(delta);
            var inputs = _newInputs.Copy();
            _newInputs.Clear();
            entities.With<TypingInput>((o, t) => t.If(t.IsActive, () => ProcessAllInputs(inputs, t)));
        }

        private static void ProcessAllInputs(IEnumerable<string> inputs, TypingInput t)
        {
            inputs.ForEach(input =>
            {
                if (input.Equals(Backspace))
                    t.Backspace();
                else
                    t.Append(input);
            });
        }

        private readonly List<string> _newInputs = new List<string>();

        List<Keys> keys;
        bool[] IskeyUp;
        private bool _backspaceIsDown;

        public KeyboardInput()
        {
            InitValidKeys();
        }

        private void InitValidKeys()
        {
            keys = new List<Keys>();
            var tempkeys = Enum.GetValues(typeof(Keys)).Cast<Keys>().ToArray();
            for (var i = 0; i < tempkeys.Length; i++)
            {
                if (i == 1 || i == 11 || (i > 26 && i < 63) || (i > 66 && i < 77) || i == 137 || i == 81) //get the keys listed above as well as A-Z
                    keys.Add(tempkeys[i]); //fill our key array
            }
            IskeyUp = new bool[keys.Count];
            for (int i = 0; i < keys.Count; i++)
                IskeyUp[i] = true;
        }

        public void Update(TimeSpan delta)
        {
            var state = Keyboard.GetState();
            var i = 0;
            foreach (Keys key in keys)
            {
                if (state.IsKeyDown(key))
                {
                    if (IskeyUp[i])
                    {
                        if (key == Keys.Space)
                            _newInputs.Add(" ");
                        if (key == Keys.OemPeriod || key == Keys.Decimal)
                            _newInputs.Add(".");
                        if (key.ToString().StartsWith("NumPad"))
                            _newInputs.Add(key.ToString().Substring(6));
                        if (i >= 2 && i <= 11)
                        {
                            if (state.IsKeyUp(Keys.RightShift) || state.IsKeyUp(Keys.LeftShift))
                                _newInputs.Add(key.ToString()[1].ToString());
                        }
                        if (i >= 12 && i <= 37)
                        {
                            if ((state.IsKeyDown(Keys.RightShift) || state.IsKeyDown(Keys.LeftShift)) != Console.CapsLock)
                                _newInputs.Add(key.ToString());
                            else _newInputs.Add(key.ToString().ToLower());
                        }
                    }
                    IskeyUp[i] = false; //make sure we know the key is pressed
                }
                else if (state.IsKeyUp(key)) IskeyUp[i] = true;
                i++;
            }
            UpdateBackspace(state);
        }

        private void UpdateBackspace(KeyboardState state)
        {
            if (!_backspaceIsDown && state.IsKeyDown(Keys.Back))
            {
                _backspaceIsDown = true;
                AddBackspace();
            }
            if (_backspaceIsDown && !state.IsKeyDown(Keys.Back))
                _backspaceIsDown = false;
        }

        private void AddBackspace()
        {
            _newInputs.Add(Backspace);
        }
    }
}
