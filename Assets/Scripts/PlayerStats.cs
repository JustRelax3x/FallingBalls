public class PlayerStats
{
    private int _hp = 0, _points = 0;

    public int GetHp() => _hp;
    public int GetPoints() => _points;

    public void SetStartingStats()
    {
        _hp = GameConstants.PlayerStartingHp;
        _points = 0;
    }

    public bool TryTakeLetalDamage(int hp)
    {
        if (hp <= 0) return false;
        _hp -= hp;
        return _hp <= 0;
    }

    public void AddPoints(int points)
    {
        if (points <= 0) return;
        _points += points;
    }
}
