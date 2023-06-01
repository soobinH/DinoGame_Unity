using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 애니메이션 컨트롤 스크립트
public class SpriteAnimation : MonoBehaviour
{
    public Sprite[] sprites; // 스프라이트 배열(애니메이션 바꿔 줄 스프라이트 넣어 줌)

    private SpriteRenderer spriteRenderer; // 스프라이트 렌더러 컴포넌트 받을 변수
    private int frame; // 프레임 수

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // 오브젝트가 활성화되어 있을 때 
    private void OnEnable()
    {
        Invoke(nameof(Animate), 0f); //스프라이트 애니메이션 작동
    }

    // 오브젝트가 활성화되지 않았을 때는 애니메이션 작동 중지
    private void OnDisable()
    {
        CancelInvoke();
    }

    // 스프라이트 애니메이션 함수
    private void Animate()
    {
        frame++;

        // 프레임 수가 스프라이트 배열 최대 인덱스보다 커지면 0으로 다시 돌아감
        if(frame>=sprites.Length)
        {
            frame = 0;
        }

        // 프레임 수가 스프라이트 배열 내에 있을 때
        if(frame>=0 && frame<sprites.Length)
        {
            spriteRenderer.sprite = sprites[frame]; // 스프라이트 변경
        }

        // 게임 스피드에 맞는 속도로 재귀(게임 스피드에 맞게 애니메이션 재생)
        Invoke(nameof(Animate), 1f / GameManager.Instance.gameSpeed);

    }
}
