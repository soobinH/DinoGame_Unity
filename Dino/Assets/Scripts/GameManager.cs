using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 외부에서 접근이 가능하지만 수정은 불가능하게 property로 작성함
    public static GameManager Instance { get; private set; }

    private PlayerCtrl player;
    private PrefabCtrl prefab;

    public float initialGameSpeed = 5f; // 초기 게임 스피드
    public float gameSpeedIncrease = 0.1f; // 게임 스피드 증가폭
    public float gameSpeed { get; private set; } // 게임 스피드 변수

    private void Start()
    {
        player = FindObjectOfType<PlayerCtrl>();
        prefab = FindObjectOfType<PrefabCtrl>();

        NewGame(); // 게임 시작 시 게임 스피드 초기화

    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        else
        {
            DestroyImmediate(gameObject);
        }
    }

    // 오브젝트가 삭제되었을 때 인스턴스 값 NULL로 바꾸어 줌
    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }


    // 1초당 증가폭만큼 게임 스피드 증가
    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
    }

    // 게임 스피드 초기화 함수
    private void NewGame()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        
        foreach(var obstacle in obstacles)
        {
            Destroy(obstacle);
        }
        gameSpeed = initialGameSpeed;
        enabled = true;

        player.gameObject.SetActive(true);
        prefab.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;
        player.gameObject.SetActive(false);
        prefab.gameObject.SetActive(false);
    }

}

