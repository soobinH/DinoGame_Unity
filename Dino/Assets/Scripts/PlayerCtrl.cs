using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

// �÷��̾� ��Ʈ�� ��ũ��Ʈ
public class PlayerCtrl : MonoBehaviour
{
    private CharacterController character; // ĳ���� ��Ʈ�� ������Ʈ �޾ƿ� ����
    private Vector3 direction; // ����
    public TextMeshProUGUI HP; // HP ǥ�� �ؽ�Ʈ

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
    }

    // �÷��̾ Ȱ��ȭ�Ǿ� ���� ��
    private void OnEnable()
    {
        direction = Vector3.zero; // 0,0,0���� �ʱ�ȭ
        health = 3; // hp �ʱ�ȭ
    }

    private void Update()
    {
         // HP ȭ�鿡 ǥ��
        HP.text = health.ToString();

        // HP 0 �Ǹ� ���� ����
        if(health <= 0)
        {
            GameOverSound.Play();
            GameManager.Instance.GameOver();
        }

        direction += Vector3.down * gravity * Time.deltaTime; // �߷� �ۿ�


        // �÷��̾ �ٴڿ� ��� �ִ��� üũ
        if(character.isGrounded)
        {
            direction = Vector3.down; // �߷� �ۿ�

            // space Ű�� ������ ��
            if(Input.GetButton("Jump"))
            {
                direction = Vector3.up * jumpHigh; // ����
                JumpSound.Play();
            }


        }

        character.Move(direction * Time.deltaTime); // ĳ���� ������
    }

    // ��ֹ��� �ε����� �� HP ����
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {
            HitSound.Play();
            health--;
        }
    }

}

