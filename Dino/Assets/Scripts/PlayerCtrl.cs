using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

// 플레이어 컨트롤 스크립트
public class PlayerCtrl : MonoBehaviour
{
    private CharacterController character; // 캐릭터 컨트롤러 컴포넌트 받아올 변수
    private SpriteRenderer spriteRenderer; // 스프라이트 렌더러 컴포넌트 받아올 변수
    public Sprite Slide; // 슬라이드 스프라이트
    public Sprite Walk; // 걷는 스프라이트(기본)
    private Vector3 direction; // 방향
    public TextMeshProUGUI HP; // HP 표시 텍스트

    public bool CanSlide = true;

    // 효과음
    public AudioSource JumpSound;
    public AudioSource GameOverSound;
    public AudioSource HitSound;

    [SerializeField]
    int health = 3; // HP

    public float gravity = 9.81f * 2f; // 중력 설정
    public float jumpHigh = 8f; // 점프력 설정

    private void Awake() // start와 같지만 먼저 실행돼서 빠름
    {
        character = GetComponent<CharacterController>(); // 플레이어의 캐릭터 컨트롤러 컴포넌트 받아옴
        spriteRenderer = GetComponent<SpriteRenderer>(); // 스프라이트 렌더러 컴포넌트 받아옴
    }

    // 플레이어가 활성화되어 있을 때
    private void OnEnable()
    {
        direction = Vector3.zero; // 0,0,0으로 초기화
        health = 3; // hp 초기화
        spriteRenderer.sprite = Walk;
        character.enabled = true;
    }

    private void Update()
    {

        // 기본 스프라이트 설정

         // HP 화면에 표시
        HP.text = health.ToString();

        // HP 0 되면 게임 종료
        if(health <= 0)
        {
            GameOverSound.Play(); // 효과음 재생
            GameManager.Instance.GameOver(); // 게임 매니저의 게임 오버 함수 호출
        }

        direction += Vector3.down * gravity * Time.deltaTime; // 중력 작용

        character.center.Set(0f, 0f, 0f); // 캐릭터 컨트롤러의 센터 값 0으로 모두 초기화
        character.radius = 0.5f; // 캐릭터 컨트롤러의 충돌 원 반지름 초기화


        jump();
        sliding();
        


        character.Move(direction * Time.deltaTime); // 캐릭터 움직임
    }

    void sliding()
    {
        // 슬라이드 할 때(아래 화살표 누를 때)
        if (Input.GetKey(KeyCode.DownArrow))
        {
            CanSlide = true;
            direction = Vector3.down * jumpHigh; // 점프 하다가도 밑으로 내려올 수 있도록
            spriteRenderer.sprite = Slide; // 플레이어 스프라이트를 슬라이드 스프라이트로 변경
            character.center.Set(0f, -0.23f, 0f); // 플레이어 컨트롤러의 센터 y 값을 변경하여 스프라이트에 맞게 함
            character.radius = 0.25f; // 플레이어 컨트롤러의 충돌 원 반지름 조정하여 스프라이트에 맞게 함

        }

        else
        {
            CanSlide = false;
            spriteRenderer.sprite = Walk;
        }
    }

    void jump()
    {
        // 플레이어가 바닥에 닿아 있는지 체크
        if (character.isGrounded)
        {
            direction = Vector3.down; // 중력 작용

            // space 키를 눌렀을 때
            if (Input.GetButton("Jump"))
            {
                direction = Vector3.up * jumpHigh; // 점프

                if (!(Input.GetKey(KeyCode.DownArrow))) // 점프 했을 때만 소리 나게
                    JumpSound.Play();
            }
        }
    }

    // 장애물과 부딪혔을 때 HP 감소
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {
            HitSound.Play(); // 효과음 재생
            health--;
        }
    }

}

