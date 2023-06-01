using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 횡스크롤(땅 스크롤) 컨트롤 스크립트
public class GroundScroll : MonoBehaviour
{
    private MeshRenderer meshRenderer; // 땅 오브젝트의 메쉬 렌더러 컴포넌트 받아올 변수

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        float speed = GameManager.Instance.gameSpeed / transform.localScale.x; // 땅 스크롤 할 스피드 지정(게임 스피드에 따라)
        meshRenderer.material.mainTextureOffset += Vector2.right * speed * Time.deltaTime; // 땅 계속 스크롤 해 줌(Offset 값을 바꾸어 줌)
    }
}
