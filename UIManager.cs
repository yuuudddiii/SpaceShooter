using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;

    private Player _player;
    private Player2 _player2;
    private int _score;


    [SerializeField]
    private Image _playerOneLivesImage;
    [SerializeField]
    private Image _playerTwoLivesImage;
    [SerializeField]
    private Sprite[] _liveSprites = new Sprite[4];
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _player2 = GameObject.Find("Player 2").GetComponent<Player2>();
        _scoreText.text = "Score: " + 0;
    }

    // Update is called once per frame
    void Update()
    {
        updateScoreText();

        if (_restartText.IsActive())
        {
            if (Input.GetKeyDown(KeyCode.R)) Application.LoadLevel(Application.loadedLevel);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

    }

    private void updateScoreText()
    {
        _scoreText.text = "Score: " + _score;
    }

    public void updateLives()
    {
        _playerOneLivesImage.sprite = _liveSprites[_player.getLives()];
        _playerTwoLivesImage.sprite = _liveSprites[_player2.getLives()];
    }

    public void showGameOverText()
    {
        _restartText.gameObject.SetActive(true);
        StartCoroutine(gameOverTextFlicker());
    }

    private IEnumerator gameOverTextFlicker()
    {
        while (_restartText.IsActive())
        {
            yield return new WaitForSeconds(0.5f);
            if (_gameOverText.IsActive()) _gameOverText.gameObject.SetActive(false);
            else _gameOverText.gameObject.SetActive(true);
        }
    }

    public void addScore()
    {
        _score += 10;
    }

}
