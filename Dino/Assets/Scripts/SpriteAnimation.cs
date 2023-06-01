using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ִϸ��̼� ��Ʈ�� ��ũ��Ʈ
public class SpriteAnimation : MonoBehaviour
{
    public Sprite[] sprites; // ��������Ʈ �迭(�ִϸ��̼� �ٲ� �� ��������Ʈ �־� ��)

    private SpriteRenderer spriteRenderer; // ��������Ʈ ������ ������Ʈ ���� ����
    private int frame; // ������ ��

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // ������Ʈ�� Ȱ��ȭ�Ǿ� ���� �� 
    private void OnEnable()
    {
        Invoke(nameof(Animate), 0f); //��������Ʈ �ִϸ��̼� �۵�
    }

    // ������Ʈ�� Ȱ��ȭ���� �ʾ��� ���� �ִϸ��̼� �۵� ����
    private void OnDisable()
    {
        CancelInvoke();
    }

    // ��������Ʈ �ִϸ��̼� �Լ�
    private void Animate()
    {
        frame++;

        // ������ ���� ��������Ʈ �迭 �ִ� �ε������� Ŀ���� 0���� �ٽ� ���ư�
        if(frame>=sprites.Length)
        {
            frame = 0;
        }

        // ������ ���� ��������Ʈ �迭 ���� ���� ��
        if(frame>=0 && frame<sprites.Length)
        {
            spriteRenderer.sprite = sprites[frame]; // ��������Ʈ ����
        }

        // ���� ���ǵ忡 �´� �ӵ��� ���(���� ���ǵ忡 �°� �ִϸ��̼� ���)
        Invoke(nameof(Animate), 1f / GameManager.Instance.gameSpeed);

    }
}
