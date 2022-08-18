using System.Collections.Generic;
using UnityEngine;
public class BallPool : MonoBehaviour
{
    [SerializeField]
    private Ball _ballPrefab;
    [SerializeField]
    private Canvas _mainCanvas;
    private Queue<Ball> _ballPool = new Queue<Ball>();
    private const int QuantityBalls = 10;

    private void Awake()
    {
        for (int i = 0; i < QuantityBalls; i++)
        {
            _ballPool.Enqueue(CreateBall());
        }
    }
    private Ball CreateBall()
    {
        var ball = Instantiate(_ballPrefab, _mainCanvas.transform, false);
        ball.gameObject.SetActive(false);
        return ball;
    }

    public Ball GetBall(Vector3 position)
    {
        Ball result = _ballPool.Count == 0 ? CreateBall() : _ballPool.Dequeue();
        result.transform.localPosition = position;
        return result;
    }
    public void ReturnBall(Ball ball)
    {
        ball.transform.position = new Vector3(-100f, ball.transform.position.y, -10f);
        ball.gameObject.SetActive(false);
        _ballPool.Enqueue(ball);
    }

}
