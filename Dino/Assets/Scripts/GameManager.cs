using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 5f; // 초기 게임 스피드
    public float gameSpeedIncrease = 0.1f; // 게임 스피드 증가폭
    public float gameSpeed { get; private set; }

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

    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        NewGame(); // 게임 시작 시 게임 스피드 초기화
    }

    // 게임 스피드 초기화 함수
    private void NewGame()
    {
        gameSpeed = initialGameSpeed;
    }

    // 1초당 0.1만큼 게임 스피드 증가
    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
    }
}

