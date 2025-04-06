using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState {
    Intro,
    Playing,
    Dead
}

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public GameState State = GameState.Intro;

    public int Lives = 3; // 플레이어의 생명 수

    [Header("References")]
    public GameObject IntroUI;
    public GameObject EnemySpawner;
    public GameObject FoodSpawner;
    public GameObject GoldenSpawner;

    public Player PlayerScript;

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

    // Update is called once per frame
    void Update()
    {
        if (State == GameState.Intro && Input.GetKeyDown(KeyCode.Space)) {
            State = GameState.Playing;
            IntroUI.SetActive(false);
            EnemySpawner.SetActive(true);
            FoodSpawner.SetActive(true);
            GoldenSpawner.SetActive(true);
        }
        if (State == GameState.Playing && Lives == 0) {
            PlayerScript.KillPlayer();
            EnemySpawner.SetActive(false);
            FoodSpawner.SetActive(false);
            GoldenSpawner.SetActive(false);
            State = GameState.Dead;
        }
        if (State == GameState.Dead && Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene("main");
        }
    }
}
