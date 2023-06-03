using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Sprite[] SlidingSprites; // 슬라이드 스프라이트 배열(애니메이션 바꿔 줄 스프라이트 넣어 줌)
    public Sprite[] CommonSprites; // 일반 걷는 스프라이트 배열

    private PlayerCtrl canSlide;

    private bool Can;


    private SpriteRenderer spriteRenderer; // 스프라이트 렌더러 컴포넌트 받을 변수
    private int frame; // 프레임 수

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        canSlide = gameObject.GetComponent<PlayerCtrl>();
        Can = false;
    }

    private void OnEnable()
    {
        Invoke(nameof(SlidingAnimate), 0f); //스프라이트 애니메이션 함수 작동
        Invoke(nameof(CommonAnimate), 0f);
    }

    // 오브젝트가 활성화되지 않았을 때는 모든 함수 작동 중지
    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Update()
    {
        Can = canSlide.CanSlide; // can 변수 갱신
    }


    private void CommonAnimate()
    {
        if (!Can)
        {
            frame++;
            // 프레임 수가 스프라이트 배열 최대 인덱스보다 커지면 0으로 다시 돌아감
            if (frame >= CommonSprites.Length)
            {
                frame = 0;
            }
            // 프레임 수가 스프라이트 배열 내에 있을 때
            if (frame >= 0 && frame < CommonSprites.Length)
            {
                spriteRenderer.sprite = CommonSprites[frame]; // 스프라이트 변경
            }

            CancelInvoke(nameof(SlidingAnimate));
            // 게임 스피드에 맞는 속도로 재귀(게임 스피드에 맞게 애니메이션 재생)
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
            // 프레임 수가 스프라이트 배열 최대 인덱스보다 커지면 0으로 다시 돌아감
            if (frame >= SlidingSprites.Length)
            {
                frame = 0;
            }
            // 프레임 수가 스프라이트 배열 내에 있을 때
            if (frame >= 0 && frame < SlidingSprites.Length)
            {
                spriteRenderer.sprite = SlidingSprites[frame]; // 스프라이트 변경
            }

            CancelInvoke(nameof(CommonAnimate));
            // 게임 스피드에 맞는 속도로 재귀(게임 스피드에 맞게 애니메이션 재생)
            Invoke(nameof(SlidingAnimate), 1f / GameManager.Instance.gameSpeed);
        }


        else
        {
            CancelInvoke(nameof(SlidingAnimate));
            Invoke(nameof(CommonAnimate), 0f);

        }
    }


}

    
