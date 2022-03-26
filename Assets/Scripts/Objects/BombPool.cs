using System.Collections.Generic;

public class BombPool
{
    private BombPool() { }

    private readonly List<IBomb> _bombPool = new List<IBomb>();

    public static BombPool Current { get; private set; }

    public static void Initialize()
    {
        Current = new BombPool();
    }

    public void Add<T>(T bomb) where T : IBomb
    {
        _bombPool.Add(bomb);
    }

    public void Remove<T>(T bomb) where T : IBomb
    {
        _bombPool.Remove(bomb);
    }

    public int GetCount()
    {
        return _bombPool.Count;
    }
}
