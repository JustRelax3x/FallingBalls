using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private BallPool _ballPool;

    [SerializeField]
    private ParticleLimitedPool _particleLimitedPool;

    [SerializeField]
    private Canvas _mainCanvas;

    [SerializeField]
    private GameView _view;

    [SerializeField]
    private Color[] _ballColors;

    private BallSpawnController _ballSpawnController;

    private PlayerStats _playerStats = new PlayerStats();

    private void Awake()
    {
        _ballSpawnController = BallSpawnController.GetInstance(_ballPool, _mainCanvas,_particleLimitedPool, _ballColors);
        _ballSpawnController.PlayerDamaged += PlayerDamaged;
        _ballSpawnController.PlayerGotPoints += PlayerGotPoints;
    }

    private void Start()
    {
        Restart();
    }

    private void Update()
    {
        _ballSpawnController.Update();
    }

    public void Pause(bool isPaused)
    {
        _view.Pause(isPaused);
        _ballSpawnController.Pause(isPaused);  
    }

    private void PlayerDamaged()
    {
        if (!_playerStats.TryTakeLetalDamage(GameConstants.BallDamage)) {
            _view.HpUpdate(_playerStats.GetHp());
            return;
        }
        _ballSpawnController.Pause(true);
        _view.ShowResult(_playerStats.GetPoints());
    }
    private void PlayerGotPoints()
    {
        _playerStats.AddPoints(GameConstants.BallPoints);
        _view.ScoreUpdate(_playerStats.GetPoints());
    }
    public void Restart()
    {
        _playerStats.SetStartingStats();
        _ballSpawnController.RestartSpawn();
        _view.Restart(_playerStats.GetHp());
    }

    private void OnDestroy()
    {
        _ballSpawnController.PlayerDamaged -= PlayerDamaged;
        _ballSpawnController.PlayerGotPoints -= PlayerGotPoints;
    }
}

