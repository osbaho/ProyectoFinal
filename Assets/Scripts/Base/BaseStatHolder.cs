using System.Collections.Generic;
using UnityEngine;

namespace Base
{
    public abstract class BaseStatHolder : MonoBehaviour
    {
        protected List<StatComponent> stats = new();

        public virtual void Initialize(params StatComponent[] components)
        {
            if (components != null)
                stats.AddRange(components);
        }

        public virtual void AddStat(StatComponent stat)
        {
            if (stat != null && !stats.Contains(stat))
                stats.Add(stat);
        }

        public virtual void RemoveStat(StatComponent stat)
        {
            if (stat != null)
                stats.Remove(stat);
        }

        public virtual T GetStat<T>() where T : StatComponent
        {
            foreach (var stat in stats)
                if (stat is T tStat)
                    return tStat;
            return null;
        }
    }
}