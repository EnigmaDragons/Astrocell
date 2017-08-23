using System;

namespace MonoDragons.Core.Common
{
    public sealed class Condition
    {
        private readonly Func<bool> _evaluate;

        public Condition(Func<bool> evaluate)
        {
            _evaluate = evaluate;
        }

        public bool Evaluate()
        {
            return _evaluate();
        }

        public static implicit operator Condition(Func<bool> evaluate)
        {
            return new Condition(evaluate);
        }

        public static implicit operator bool(Condition condition)
        {
            return condition.Evaluate();
        }
    }
}
