﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;

namespace MonoDragons.Core.Entities
{
    public static class EntitiesExtensions
    {
        public static void With<T>(this IEntities entities, Action<T> action)
        {
            entities.With<T>((o, t) => action(t));
        }

        public static void WithIntersecting<T>(this IEntities entities, Point point, Action<T> action)
        {
            Where(entities, o => o.Transform.Intersects(point), action);
        }

        public static void WithTopMost<T>(this IEntities entities, Point point, Action<T> action)
        {
            WithTopMost<T>(entities, point, (_, x) => action(x));
        }

        public static void WithTopMost<T>(this IEntities entities, Point point, Action<GameObject, T> action)
        {
            Collect<T>(entities)
                .Where(o => o.Transform.Intersects(point))
                .OrderByDescending(o => o.Transform.ZIndex)
                .FirstAsOptional()
                .IfPresent(o => o.With<T>(x => action(o, x)));
        }

        public static void WithTopMost<T>(this IEntities entities, Point point, 
            Action<GameObject, T> action, Action<GameObject, T> othersAction)
        {
            IEnumerable<GameObject> objs = Collect<T>(entities)
                .OrderByDescending(o => o.Transform.ZIndex);
            objs.Where(o => o.Transform.Intersects(point))
                .FirstAsOptional()
                .IfPresent(o =>
                {
                    o.With<T>(x => action(o, x));
                    objs = objs.Except(o.AsList());
                });
            objs.ForEach(o => o.With<T>(x => othersAction(o, x)));
        }

        public static void Where<T>(this IEntities entities, Predicate<GameObject> condition, Action<T> action)
        {
            var targets = new List<GameObject>();
            entities.With<T>((o, x) => x.If(condition(o), () => targets.Add(o)));
            targets.ForEach(o => o.With(action));
        }

        public static void Where<T>(this IEntities entities, Predicate<T> condition, Action<T> action)
        {
            var targets = new List<GameObject>();
            entities.With<T>((o, x) => x.If(condition(x), () => targets.Add(o)));
            targets.ForEach(o => o.With(action));
        }

        public static List<GameObject> Collect<T>(this IEntities entities)
        {
            var targets = new List<GameObject>();
            entities.With<T>((o, x) => targets.Add(o));
            return targets;
        }

        public static List<GameObject> Collect<T1, T2>(this IEntities entities)
        {
            var collection1 = entities.Collect<T1>();
            var collection2 = entities.Collect<T2>();
            return collection1.Intersect(collection2).ToList();
        }
    }
}
