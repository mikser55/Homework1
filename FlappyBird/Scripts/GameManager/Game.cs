using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] Bird _bird;
    [SerializeField] private StartWindow _startWindow;
    [SerializeField] private EndGameWindow _endWindow;
    [SerializeField] private PauseWindow _pauseWindow;
    [SerializeField] private RestartWindow _restartWindow;

    private void OnEnable()
    {
        _bird.Died += OnRestartButtonClicked;
        _startWindow.StartButtonClicked += OnPlayButtonClicked;
        _endWindow.EndButtonClicked += OnRestartButtonClicked;
        _pauseWindow.PauseButtonClicked += OnPauseButtonClicked;
        _restartWindow.RestartButtonClicked += OnRestartButtonClicked;
    }

    private void OnDisable()
    {
        _bird.Died -= OnRestartButtonClicked;
        _startWindow.StartButtonClicked -= OnPlayButtonClicked;
        _endWindow.EndButtonClicked -= OnRestartButtonClicked;
        _pauseWindow.PauseButtonClicked -= OnPauseButtonClicked;
        _restartWindow.RestartButtonClicked -= OnRestartButtonClicked;
    }

    private void Start()
    {
        StopGame();
        _pauseWindow.Close();
    }

    private void OnPauseButtonClicked()
    {
        _startWindow.Open();
        _pauseWindow.Close();
        StopGame();
    }

    private void OnRestartButtonClicked()
    {
        SceneManager.LoadScene("SampleScene");
        StopGame();
    }

    private void OnPlayButtonClicked()
    {
        _startWindow.Close();
        _pauseWindow.Open();
        StartGame();
    }

    private void StopGame()
    {
        Time.timeScale = 0f;
    }

    private void StartGame()
    {
        Time.timeScale = 1.0f;
    }
}