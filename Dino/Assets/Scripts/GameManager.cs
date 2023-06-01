using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 5f; // �ʱ� ���� ���ǵ�
    public float gameSpeedIncrease = 0.1f; // ���� ���ǵ� ������
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
        NewGame(); // ���� ���� �� ���� ���ǵ� �ʱ�ȭ
    }

    // ���� ���ǵ� �ʱ�ȭ �Լ�
    private void NewGame()
    {
        gameSpeed = initialGameSpeed;
    }

    // 1�ʴ� 0.1��ŭ ���� ���ǵ� ����
    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
    }
}

