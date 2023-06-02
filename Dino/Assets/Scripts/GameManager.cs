using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// ���� ��ü ���� ��ũ��Ʈ

public class GameManager : MonoBehaviour
{
    // �ܺο��� ������ ���������� ������ �Ұ����ϰ� property�� �ۼ���
    public static GameManager Instance { get; private set; }

    // UI ���� ����
    public TextMeshProUGUI gameOverText; // ���� ���� �ؽ�Ʈ
    public TextMeshProUGUI scoreText; // ���ھ� �ؽ�Ʈ
    public TextMeshProUGUI highscoreText; // �ְ� ���� �ؽ�Ʈ

    public Button retryButton; // ����� ��ư

    public AudioSource GetPoint;

    
    private PlayerCtrl player; // �÷��̾� ��Ʈ�� ��ũ��Ʈ ����
    private PrefabCtrl prefab; // ������ ��Ʈ�� ��ũ��Ʈ ����
  
    float score; // ���� ����

    public float initialGameSpeed = 5f; // �ʱ� ���� ���ǵ�
    public float gameSpeedIncrease = 0.1f; // ���� ���ǵ� ������
    public float gameSpeed { get; private set; } // ���� ���ǵ� ����

    private void Awake()
    {
        // �ν��Ͻ��� ���ٸ� ���� ������Ʈ�� �Ҵ�
        if (Instance == null)
        {

            Instance = this;
        }

        // �̹� �ν��Ͻ��� �ִٸ� ����
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    // ������Ʈ�� �����Ǿ��� �� �ν��Ͻ� �� NULL�� �ٲپ� ��
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerCtrl>(); // ��ũ��Ʈ ���� ������Ʈ ã��
        prefab = FindObjectOfType<PrefabCtrl>(); // ��ũ��Ʈ ���� ������Ʈ ã��


        NewGame(); // ���� ���� �Լ� ȣ��

    }

    // ���� ���� �Լ�
    public void NewGame()
    {
        score = 0; // ���� 0���� �ʱ�ȭ

        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>(); // ��ֹ� ������Ʈ �迭
        Birds[] bires = GameObject.FindObjectsOfType<Birds>();

        foreach (var obstacle in obstacles) // �����ϱ� �� ��ֹ� ����
        {
            Destroy(obstacle.gameObject);
        }

        foreach (var birds in bires)
        {
            Destroy(birds.gameObject);
        }

        gameSpeed = initialGameSpeed; // ���� ���ǵ� �ʱ�ȭ
        enabled = true; // ���� �Ŵ��� Ȱ��ȭ

        // �� ������Ʈ Ȱ��ȭ
        player.gameObject.SetActive(true);
        prefab.gameObject.SetActive(true);

        // ���� ���� �� ��Ÿ�� UI ������Ʈ ��Ȱ��ȭ
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

        SaveHighScore(); // �ְ� ���� ���� �Լ� ȣ��
    }

    // ���� ���� �Լ�
    public void GameOver()
    {
        gameSpeed = 0f; // ���� ���ǵ� 0���� �ٲ� ��(���� ����)
        enabled = false; // ��Ȱ��ȭ

        // �� ������Ʈ ��Ȱ��ȭ
        player.gameObject.SetActive(false);
        prefab.gameObject.SetActive(false);

        // ���� ���� �� ��Ÿ�� UI ������Ʈ Ȱ��ȭ
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);

        SaveHighScore(); // �ְ� ���� ���� �Լ� ȣ��
    }




    // 1�ʴ� ��������ŭ ���� ���ǵ� ����
    private void Update()
    {

        gameSpeed += gameSpeedIncrease * Time.deltaTime; // ���ǵ� ����
        score += gameSpeed * Time.deltaTime; // ���ǵ忡 ���� ���� ����

        

        scoreText.text = Mathf.FloorToInt(score).ToString("D5"); // �ټ� ��° �ε������� �ٲ�
    }

    // �ְ� ���� ���� �Լ�
    private void SaveHighScore()
    {
        float highScore = PlayerPrefs.GetFloat("highScore", 0); // �ְ� ���� �ʱ�ȭ

        // �ְ� ������ ���� �������� ������
        if(score> highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("highScore", highScore); // ����(������ ����)


        }

        highscoreText.text = Mathf.FloorToInt(highScore).ToString("D5"); // �ټ� ��° �ε������� �ٲ�
    }

}

