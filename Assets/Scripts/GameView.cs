using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField]
    private GameObject _pausePanel;

    [SerializeField]
    private GameObject _resultPanel;

    [SerializeField]
    private GameObject _objectsInGame;

    [SerializeField]
    private TextMeshProUGUI _scoreResult;

    [SerializeField]
    private TextMeshProUGUI _scoreInGame;

    [SerializeField]
    private TextMeshProUGUI _hpInGame;

    [SerializeField]
    private Button _pause;

    public void Restart(int playerHp)
    {
        _pausePanel.SetActive(false);
        _resultPanel.SetActive(false);
        _pause.gameObject.SetActive(true);
        _objectsInGame.SetActive(true);
        _hpInGame.text = playerHp.ToString();
        _scoreInGame.text = "0";
    }

    public void ScoreUpdate(int score)
    {
        _scoreInGame.text = score.ToString();
    }

    public void HpUpdate(int hp)
    {
        _hpInGame.text = hp.ToString();
    }

    public void ShowResult(int score)
    {
        _resultPanel.SetActive(true);
        _pause.gameObject.SetActive(false);
        _objectsInGame.SetActive(false);
        _scoreResult.text = score.ToString();
    }

    public void Pause(bool isPaused)
    {
        _pausePanel.SetActive(isPaused);
        _pause.gameObject.SetActive(!isPaused);
    }
}
