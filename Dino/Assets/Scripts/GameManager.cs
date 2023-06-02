using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// 게임 전체 감독 스크립트

public class GameManager : MonoBehaviour
{
    // 외부에서 접근이 가능하지만 수정은 불가능하게 property로 작성함
    public static GameManager Instance { get; private set; }

    // UI 관련 변수
    public TextMeshProUGUI gameOverText; // 게임 오버 텍스트
    public TextMeshProUGUI scoreText; // 스코어 텍스트
    public TextMeshProUGUI highscoreText; // 최고 점수 텍스트

    public Button retryButton; // 재시작 버튼

    public AudioSource GetPoint;

    
    private PlayerCtrl player; // 플레이어 컨트롤 스크립트 변수
    private PrefabCtrl prefab; // 프리팹 컨트롤 스크립트 변수
  
    float score; // 점수 변수

    public float initialGameSpeed = 5f; // 초기 게임 스피드
    public float gameSpeedIncrease = 0.1f; // 게임 스피드 증가폭
    public float gameSpeed { get; private set; } // 게임 스피드 변수

    private void Awake()
    {
        // 인스턴스가 없다면 현재 오브젝트를 할당
        if (Instance == null)
        {

            Instance = this;
        }

        // 이미 인스턴스가 있다면 삭제
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    // 오브젝트가 삭제되었을 때 인스턴스 값 NULL로 바꾸어 줌
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerCtrl>(); // 스크립트 담은 오브젝트 찾기
        prefab = FindObjectOfType<PrefabCtrl>(); // 스크립트 담은 오브젝트 찾기


        NewGame(); // 게임 시작 함수 호출

    }

    // 게임 시작 함수
    public void NewGame()
    {
        score = 0; // 점수 0으로 초기화

        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>(); // 장애물 오브젝트 배열
        Birds[] bires = GameObject.FindObjectsOfType<Birds>();

        foreach (var obstacle in obstacles) // 시작하기 전 장애물 삭제
        {
            Destroy(obstacle.gameObject);
        }

        foreach (var birds in bires)
        {
            Destroy(birds.gameObject);
        }

        gameSpeed = initialGameSpeed; // 게임 스피드 초기화
        enabled = true; // 게임 매니저 활성화

        // 각 오브젝트 활성화
        player.gameObject.SetActive(true);
        prefab.gameObject.SetActive(true);

        // 게임 오버 시 나타날 UI 오브젝트 비활성화
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

        SaveHighScore(); // 최고 점수 저장 함수 호출
    }

    // 게임 오버 함수
    public void GameOver()
    {
        gameSpeed = 0f; // 게임 스피드 0으로 바꿔 줌(게임 멈춤)
        enabled = false; // 비활성화

        // 각 오브젝트 비활성화
        player.gameObject.SetActive(false);
        prefab.gameObject.SetActive(false);

        // 게임 오버 시 나타날 UI 오브젝트 활성화
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);

        SaveHighScore(); // 최고 점수 저장 함수 호출
    }




    // 1초당 증가폭만큼 게임 스피드 증가
    private void Update()
    {

        gameSpeed += gameSpeedIncrease * Time.deltaTime; // 스피드 증가
        score += gameSpeed * Time.deltaTime; // 스피드에 따라서 점수 증가

        

        scoreText.text = Mathf.FloorToInt(score).ToString("D5"); // 다섯 번째 인덱스부터 바꿈
    }

    // 최고 점수 갱신 함수
    private void SaveHighScore()
    {
        float highScore = PlayerPrefs.GetFloat("highScore", 0); // 최고 점수 초기화

        // 최고 점수가 현재 점수보다 작으면
        if(score> highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("highScore", highScore); // 갱신(데이터 저장)


        }

        highscoreText.text = Mathf.FloorToInt(highScore).ToString("D5"); // 다섯 번째 인덱스부터 바꿈
    }

}

