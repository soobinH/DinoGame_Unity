using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // �ܺο��� ������ ���������� ������ �Ұ����ϰ� property�� �ۼ���
    public static GameManager Instance { get; private set; }

    private PlayerCtrl player;
    private PrefabCtrl prefab;

    public float initialGameSpeed = 5f; // �ʱ� ���� ���ǵ�
    public float gameSpeedIncrease = 0.1f; // ���� ���ǵ� ������
    public float gameSpeed { get; private set; } // ���� ���ǵ� ����

    private void Start()
    {
        player = FindObjectOfType<PlayerCtrl>();
        prefab = FindObjectOfType<PrefabCtrl>();

        NewGame(); // ���� ���� �� ���� ���ǵ� �ʱ�ȭ

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

    // ������Ʈ�� �����Ǿ��� �� �ν��Ͻ� �� NULL�� �ٲپ� ��
    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }


    // 1�ʴ� ��������ŭ ���� ���ǵ� ����
    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
    }

    // ���� ���ǵ� �ʱ�ȭ �Լ�
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

