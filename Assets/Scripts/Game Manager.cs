using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum GameState {
    Intro,
    Playing,
    Dead
}

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public GameState State = GameState.Intro;

    public float PlayerStartTime;

    public int Lives = 3; // 플레이어의 생명 수

    // 참조변수
    [Header("References")]
    public GameObject IntroUI;
    public GameObject DeadUI;
    public GameObject EnemySpawner;
    public GameObject FoodSpawner;
    public GameObject GoldenSpawner;
    public Player PlayerScript;
    public TMP_Text scoreText;

    // Unity에 의해서 자동으로 호출되는 메소드 (Start보다 먼저 호출됨)
    void Awake() 
    {
        if (Instance == null) {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 게임 시작 시 IntroUI를 활성화
        IntroUI.SetActive(true);
    }

    float CalculateScore() 
    {
        // 현재 시간에서 플레이어가 시작한 시간을 빼서 점수 계산
        return Time.time - PlayerStartTime;
    }

    void SaveHighScore() 
    {
        int score = Mathf.FloorToInt(CalculateScore());
        int currentHighScore = PlayerPrefs.GetInt("highScore");
        if (score > currentHighScore) {
            PlayerPrefs.SetInt("highScore", score);
            PlayerPrefs.Save();
        }
    }

    int GetHighScore()
    {
        return PlayerPrefs.GetInt("highScore");
    }

    // Update is called once per frame
    void Update()
    {
        if (State == GameState.Playing) {
            scoreText.text = "Score: " + Mathf.FloorToInt(CalculateScore());
        } else if (State == GameState.Dead) {
            scoreText.text = "High Score: " + GetHighScore();
        }

        if (State == GameState.Intro && Input.GetKeyDown(KeyCode.Space)) {
            State = GameState.Playing;
            IntroUI.SetActive(false);
            EnemySpawner.SetActive(true);
            FoodSpawner.SetActive(true);
            GoldenSpawner.SetActive(true);
            PlayerStartTime = Time.time;
        }
        if (State == GameState.Playing && Lives == 0) {
            PlayerScript.KillPlayer();
            EnemySpawner.SetActive(false);
            FoodSpawner.SetActive(false);
            GoldenSpawner.SetActive(false);
            SaveHighScore();
            DeadUI.SetActive(true);
            State = GameState.Dead;
        }
        if (State == GameState.Dead && Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene("main");
        }
    }
}
