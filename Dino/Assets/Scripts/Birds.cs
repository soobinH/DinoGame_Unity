using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 새 장애물을 다른 장애물과 다른 속도 부여하기 위한 스크립트
public class Birds : MonoBehaviour
{
    private float leftEdge; // 좌측 가장자리 처리 변수

    private void Start()
    {
        leftEdge = -8; // 좌측 가장자리 변수 초기화

    }
    private void Update()
    {
        transform.position += Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime*1.2f; // 게임 스피드에 맞춰 장애물 오브젝트를 왼쪽으로 이동시킴

        // 좌측 가장자리로 가면 장애물 삭제
        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);

        }
    }

    // 플레이어와 닿으면 장애물 오브젝트 삭제
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Destroy(gameObject);
    }
}
