﻿using System;

namespace MonoDragons.Core.Common
{
    public static class ConditionExtensions
    {
        public static void If(this bool condition, Action action)
        {
            if (condition)
                action();
        }

        public static void IfNot(this bool condition, Action action)
        {
            if (!condition)
                action();
        }
    }
}
