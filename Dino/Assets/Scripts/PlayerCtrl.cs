using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// 플레이어 컨트롤 스크립트
public class PlayerCtrl : MonoBehaviour
{
    private CharacterController character; // 캐릭터 컨트롤 컴포넌트 받아올 변수
    private Vector3 direction; // 방향

    public float gravity = 9.81f * 2f; // 중력 설정
    public float jumpHigh = 8f; // 점프력 설정

    private void Awake() // start와 같지만 먼저 실행돼서 빠름
    {
        character = GetComponent<CharacterController>(); // 플레이어의 캐릭터 컨트롤러 컴포넌트 받아옴
    }

    // 플레이어가 활성화되어 있을 때
    private void OnEnable()
    {
        direction = Vector3.zero; // 0,0,0으로 초기화
    }

    private void Update()
    {
        direction += Vector3.down * gravity * Time.deltaTime; // 중력 작용


        // 플레이어가 바닥에 닿아 있는지 체크
        if(character.isGrounded)
        {
            direction = Vector3.down; // 중력 작용

            // space 키를 눌렀을 때
            if(Input.GetButton("Jump"))
            {
                direction = Vector3.up * jumpHigh; // 점프
            }
        }

        character.Move(direction * Time.deltaTime); // 캐릭터 움직임
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
    }

}

