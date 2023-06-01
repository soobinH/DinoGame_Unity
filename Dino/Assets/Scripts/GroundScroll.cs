using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ⱦ��ũ��(�� ��ũ��) ��Ʈ�� ��ũ��Ʈ
public class GroundScroll : MonoBehaviour
{
    private MeshRenderer meshRenderer; // �� ������Ʈ�� �޽� ������ ������Ʈ �޾ƿ� ����

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        float speed = GameManager.Instance.gameSpeed / transform.localScale.x; // �� ��ũ�� �� ���ǵ� ����(���� ���ǵ忡 ����)
        meshRenderer.material.mainTextureOffset += Vector2.right * speed * Time.deltaTime; // �� ��� ��ũ�� �� ��(Offset ���� �ٲپ� ��)
    }
}
