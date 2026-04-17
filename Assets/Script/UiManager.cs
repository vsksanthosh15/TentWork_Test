using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour {

    public static UiManager Instance { get; private set; }

    [SerializeField] GameObject StartLayer, EndLayer;
    [SerializeField] TextMeshProUGUI TimerText;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI HighScoreText;

    void Awake() {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void StartGame() {
        GameManager.Instance.OnStartGame();
    }

    public void RestartGame() {
        GameManager.Instance.OnRestartGame();
    }

    public void ActiveStartLayer(bool isActive) {
        StartLayer.SetActive(isActive);
    }

    public void ActiveEndLayer(bool isActive) {
        EndLayer.SetActive(isActive);
    }

    public void UpdateTimerText(float time) {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateScoreText(float score) {
        ScoreText.text = "Score : " + score.ToString();
    }

    public void UpdateHighScoreText(float score) {
        HighScoreText.text = "High Score : " + score.ToString();
    }

    public void OnPauseButton() {
        EndLayer.SetActive(!EndLayer.activeSelf);
    }

    public void Quit() {
        Application.Quit();
    }

}
