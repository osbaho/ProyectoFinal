using Interfaces;
using System.Collections.Generic;
using System.Linq;

public class StatHolder
{
    private readonly List<StatComponent> stats = new();

    public StatHolder(params StatComponent[] components)
    {
        if (components != null)
            stats.AddRange(components);
    }

    public void AddStat(StatComponent stat)
    {
        if (stat != null && !stats.Contains(stat))
            stats.Add(stat);
    }

    public void RemoveStat(StatComponent stat)
    {
        if (stat != null)
            stats.Remove(stat);
    }

    public T GetStat<T>() where T : StatComponent
    {
        return stats.OfType<T>().FirstOrDefault();
    }

    public IEnumerable<T> GetStats<T>() where T : StatComponent
    {
        return stats.OfType<T>();
    }

    public bool HasStat<T>() where T : StatComponent
    {
        return stats.OfType<T>().Any();
    }

    public IEnumerable<StatComponent> GetAllStats() => stats.AsReadOnly();
}
