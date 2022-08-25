using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public Color BallColor { get; private set; }
    [SerializeField]
    private Image _ballImage;

    private float _speed = 1f;
    private const float TargetPositionY = -1000f;
    private bool _isFalling = true;

    public event System.Action<Ball> BallClicked;
    public event System.Action<Ball> EnteredDeathZone;

    public void SetIsFalling(bool isFalling)
    {
        _isFalling = isFalling;
    }
 
    public void SetUpBall(Color color, float speed)
    {
        SetBallColor(color);
        SetSpeed(speed);
        _isFalling = true;
    }

    void FixedUpdate()
    {
        if (_isFalling) {
            //transform.Translate(0f, -_speed * Time.deltaTime, 0f, Space.World);
            transform.position -= transform.up * _speed * Time.deltaTime;
            //transform.position = Vector3.MoveTowards(transform.position,
            //  new Vector3(transform.position.x, TargetPositionY, transform.position.z), _speed * Time.deltaTime);
        }
    }

    private void OnMouseDown()
    {
        if (_isFalling)
        BallClicked?.Invoke(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("DeathZone"))  
            return;
        EnteredDeathZone?.Invoke(this);
    }
    private void SetBallColor(Color color)
    {
        BallColor = color;
        _ballImage.color = color;
    }

    private void SetSpeed(float speed)
    {
        if (speed == 0) 
            return;
        if (speed < 0) 
            speed *= -1;
        _speed = speed;
    }
}
