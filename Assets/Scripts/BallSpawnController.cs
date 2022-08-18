using System.Collections.Generic;
using UnityEngine;

public class BallSpawnController
{
    private BallPool _ballPool;

    private List<Ball> _activeBalls = new List<Ball>(8);

    private SpawnDifficultyModel _spawnDifficultyModel = new SpawnDifficultyModel();

    private ParticleLimitedPool _particleLimitedPool;

    private Color[] _ballColors;

    private bool _isSpawning = false;
    private float _halfWidthCanvas, _halfHeightCanvas;

    public event System.Action PlayerDamaged;
    public event System.Action PlayerGotPoints;

    private static BallSpawnController _instance;

    private BallSpawnController(BallPool ballPool, Canvas canvas, ParticleLimitedPool particleLimitedPool, Color[] colors) {
        _ballPool = ballPool;
        _particleLimitedPool = particleLimitedPool;
        Rect rect = canvas.GetComponent<RectTransform>().rect;
        _halfHeightCanvas = rect.yMax;
        _halfWidthCanvas = rect.xMax / 2f;
        _ballColors = colors;
        _isSpawning = false;
    }

    public static BallSpawnController GetInstance(BallPool ballPool, Canvas canvas, ParticleLimitedPool particleLimitedPool, Color[] colors)
    {
        if (_instance == null)
        {
            _instance = new BallSpawnController(ballPool, canvas, particleLimitedPool, colors);
            return _instance;
        }
        return null;
    } 
    public void RestartSpawn()
    {
        _spawnDifficultyModel.Restart();
        if (_activeBalls.Count != 0) CleanSpawnedBalls();
        _isSpawning = true;
    }

    public void Update()
    {
        if (_isSpawning && _spawnDifficultyModel.TrySpawn(Time.deltaTime))
        {
            SpawnBall();
        }
    }

    public void Pause(bool paused)
    {
        foreach (Ball ball in _activeBalls) ball.SetIsFalling(!paused);
        _isSpawning = !paused;
    }
    private void CleanSpawnedBalls()
    {
        while (_activeBalls.Count > 0) {
            ReturnBallToPool(_activeBalls[0]);
        }
    }
    private void SpawnBall()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-_halfWidthCanvas, _halfWidthCanvas), _halfHeightCanvas, 0);
        Ball ball = _ballPool.GetBall(spawnPosition);
        ball.EnteredDeathZone += DeathZoneTriggered;
        ball.BallClicked += BallClicked;
        ball.gameObject.SetActive(true);
        int i = Random.Range(0, _ballColors.Length);
        ball.SetUpBall(_ballColors[i], _spawnDifficultyModel.GetSpeed());
        _activeBalls.Add(ball);
    }

    private void BallClicked(Ball ball)
    {
        PlayerGotPoints?.Invoke();
        var particle = _particleLimitedPool.GetParticle(ball.transform.localPosition);
        var settings = particle.main;
        settings.startColor = ball.BallColor;
        particle.Play();
        ReturnBallToPool(ball);
    }

    private void ReturnBallToPool(Ball ball)
    {
        _activeBalls.Remove(ball);
        ball.EnteredDeathZone -= DeathZoneTriggered;
        ball.BallClicked -= BallClicked;
        _ballPool.ReturnBall(ball);
    }

    private void DeathZoneTriggered(Ball ball)
    {
        PlayerDamaged?.Invoke();
        ReturnBallToPool(ball);
    }


}

