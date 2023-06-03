using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

// �÷��̾� ��Ʈ�� ��ũ��Ʈ
public class PlayerCtrl : MonoBehaviour
{
    private CharacterController character; // ĳ���� ��Ʈ�ѷ� ������Ʈ �޾ƿ� ����
    private SpriteRenderer spriteRenderer; // ��������Ʈ ������ ������Ʈ �޾ƿ� ����
    public Sprite Slide; // �����̵� ��������Ʈ
    public Sprite Walk; // �ȴ� ��������Ʈ(�⺻)
    private Vector3 direction; // ����
    public TextMeshProUGUI HP; // HP ǥ�� �ؽ�Ʈ

    public bool CanSlide = true;

    // ȿ����
    public AudioSource JumpSound;
    public AudioSource GameOverSound;
    public AudioSource HitSound;

    [SerializeField]
    int health = 3; // HP

    public float gravity = 9.81f * 2f; // �߷� ����
    public float jumpHigh = 8f; // ������ ����

    private void Awake() // start�� ������ ���� ����ż� ����
    {
        character = GetComponent<CharacterController>(); // �÷��̾��� ĳ���� ��Ʈ�ѷ� ������Ʈ �޾ƿ�
        spriteRenderer = GetComponent<SpriteRenderer>(); // ��������Ʈ ������ ������Ʈ �޾ƿ�
    }

    // �÷��̾ Ȱ��ȭ�Ǿ� ���� ��
    private void OnEnable()
    {
        direction = Vector3.zero; // 0,0,0���� �ʱ�ȭ
        health = 3; // hp �ʱ�ȭ
        spriteRenderer.sprite = Walk;
        character.enabled = true;
    }

    private void Update()
    {

        // �⺻ ��������Ʈ ����

         // HP ȭ�鿡 ǥ��
        HP.text = health.ToString();

        // HP 0 �Ǹ� ���� ����
        if(health <= 0)
        {
            GameOverSound.Play(); // ȿ���� ���
            GameManager.Instance.GameOver(); // ���� �Ŵ����� ���� ���� �Լ� ȣ��
        }

        direction += Vector3.down * gravity * Time.deltaTime; // �߷� �ۿ�

        character.center.Set(0f, 0f, 0f); // ĳ���� ��Ʈ�ѷ��� ���� �� 0���� ��� �ʱ�ȭ
        character.radius = 0.5f; // ĳ���� ��Ʈ�ѷ��� �浹 �� ������ �ʱ�ȭ


        jump();
        sliding();
        


        character.Move(direction * Time.deltaTime); // ĳ���� ������
    }

    void sliding()
    {
        // �����̵� �� ��(�Ʒ� ȭ��ǥ ���� ��)
        if (Input.GetKey(KeyCode.DownArrow))
        {
            CanSlide = true;
            direction = Vector3.down * jumpHigh; // ���� �ϴٰ��� ������ ������ �� �ֵ���
            spriteRenderer.sprite = Slide; // �÷��̾� ��������Ʈ�� �����̵� ��������Ʈ�� ����
            character.center.Set(0f, -0.23f, 0f); // �÷��̾� ��Ʈ�ѷ��� ���� y ���� �����Ͽ� ��������Ʈ�� �°� ��
            character.radius = 0.25f; // �÷��̾� ��Ʈ�ѷ��� �浹 �� ������ �����Ͽ� ��������Ʈ�� �°� ��

        }

        else
        {
            CanSlide = false;
            spriteRenderer.sprite = Walk;
        }
    }

    void jump()
    {
        // �÷��̾ �ٴڿ� ��� �ִ��� üũ
        if (character.isGrounded)
        {
            direction = Vector3.down; // �߷� �ۿ�

            // space Ű�� ������ ��
            if (Input.GetButton("Jump"))
            {
                direction = Vector3.up * jumpHigh; // ����

                if (!(Input.GetKey(KeyCode.DownArrow))) // ���� ���� ���� �Ҹ� ����
                    JumpSound.Play();
            }
        }
    }

    // ��ֹ��� �ε����� �� HP ����
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {
            HitSound.Play(); // ȿ���� ���
            health--;
        }
    }

}

