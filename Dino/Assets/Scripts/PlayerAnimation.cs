using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Sprite[] SlidingSprites; // �����̵� ��������Ʈ �迭(�ִϸ��̼� �ٲ� �� ��������Ʈ �־� ��)
    public Sprite[] CommonSprites; // �Ϲ� �ȴ� ��������Ʈ �迭

    private PlayerCtrl canSlide;

    private bool Can;


    private SpriteRenderer spriteRenderer; // ��������Ʈ ������ ������Ʈ ���� ����
    private int frame; // ������ ��

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        canSlide = gameObject.GetComponent<PlayerCtrl>();
        Can = false;
    }

    private void OnEnable()
    {
        Invoke(nameof(SlidingAnimate), 0f); //��������Ʈ �ִϸ��̼� �Լ� �۵�
        Invoke(nameof(CommonAnimate), 0f);
    }

    // ������Ʈ�� Ȱ��ȭ���� �ʾ��� ���� ��� �Լ� �۵� ����
    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Update()
    {
        Can = canSlide.CanSlide; // can ���� ����
    }


    private void CommonAnimate()
    {
        if (!Can)
        {
            frame++;
            // ������ ���� ��������Ʈ �迭 �ִ� �ε������� Ŀ���� 0���� �ٽ� ���ư�
            if (frame >= CommonSprites.Length)
            {
                frame = 0;
            }
            // ������ ���� ��������Ʈ �迭 ���� ���� ��
            if (frame >= 0 && frame < CommonSprites.Length)
            {
                spriteRenderer.sprite = CommonSprites[frame]; // ��������Ʈ ����
            }

            CancelInvoke(nameof(SlidingAnimate));
            // ���� ���ǵ忡 �´� �ӵ��� ���(���� ���ǵ忡 �°� �ִϸ��̼� ���)
            Invoke(nameof(CommonAnimate), 1f / GameManager.Instance.gameSpeed);
        }

        else
        {
            CancelInvoke(nameof(CommonAnimate));
            Invoke(nameof(SlidingAnimate), 0f);
        }
            
            
    }

    private void SlidingAnimate()
    {
        if(Can)
        {
            frame++;
            // ������ ���� ��������Ʈ �迭 �ִ� �ε������� Ŀ���� 0���� �ٽ� ���ư�
            if (frame >= SlidingSprites.Length)
            {
                frame = 0;
            }
            // ������ ���� ��������Ʈ �迭 ���� ���� ��
            if (frame >= 0 && frame < SlidingSprites.Length)
            {
                spriteRenderer.sprite = SlidingSprites[frame]; // ��������Ʈ ����
            }

            CancelInvoke(nameof(CommonAnimate));
            // ���� ���ǵ忡 �´� �ӵ��� ���(���� ���ǵ忡 �°� �ִϸ��̼� ���)
            Invoke(nameof(SlidingAnimate), 1f / GameManager.Instance.gameSpeed);
        }


        else
        {
            CancelInvoke(nameof(SlidingAnimate));
            Invoke(nameof(CommonAnimate), 0f);

        }
    }


}

    
