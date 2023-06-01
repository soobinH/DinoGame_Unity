using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private CharacterController character; // ĳ���� ��Ʈ��
    private Vector3 direction; // ����

    public float gravity = 9.81f * 2f; // �߷� ����
    public float jumpHigh = 8f; // ������ ����

    private void Awake()
    {
        character = GetComponent<CharacterController>(); // �÷��̾��� ĳ���� ��Ʈ�ѷ� ������Ʈ �޾ƿ�
    }

    private void OnEnable()
    {
        direction = Vector3.zero; // 0,0,0���� �ʱ�ȭ
    }

    private void Update()
    {
        direction += Vector3.down * gravity * Time.deltaTime;


        // �÷��̾ �ٴڿ� ��� �ִ��� üũ
        if(character.isGrounded)
        {
            direction = Vector3.down; // �߷� �ۿ�

            // space Ű�� ������ ��
            if(Input.GetButton("Jump"))
            {
                direction = Vector3.up * jumpHigh; // ����
            }
        }

        character.Move(direction * Time.deltaTime); // ĳ���� ������
    }

   

}

