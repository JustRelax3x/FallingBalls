public class SpawnDifficultyModel
{
    private readonly float[] SpawnTimersByLevel = {1f, 0.8f, 0.7f, 0.6f, 0.5f}; 
    private readonly float[] SpawnSpeedByLevel = {1.5f, 2f, 2.5f, 3f, 3.2f};
    private readonly int[] TimeOfLevelUp = {5, 10, 15, 20, int.MaxValue};
    private int _level = 0;
    private float _timer = 0f, _timePassed = 0f;

    public bool TrySpawn(float deltaTime)
    {
        _timer -= deltaTime;
        _timePassed += deltaTime;
        if (_timePassed >= TimeOfLevelUp[_level])
            _level++;
        if (_timer > 0f)
            return false;
        _timer = SpawnTimersByLevel[_level];
        return true;
    }
    public float GetSpeed()
    {
        return SpawnSpeedByLevel[_level];
    }

    public void Restart()
    {
        _timer = 0f;
        _timePassed = 0f;
        _level = 0;
    }
}
