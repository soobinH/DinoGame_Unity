using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ��ֹ��� �ٸ� ��ֹ��� �ٸ� �ӵ� �ο��ϱ� ���� ��ũ��Ʈ
public class Birds : MonoBehaviour
{
    private float leftEdge; // ���� �����ڸ� ó�� ����

    private void Start()
    {
        leftEdge = -8; // ���� �����ڸ� ���� �ʱ�ȭ

    }
    private void Update()
    {
        transform.position += Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime*1.2f; // ���� ���ǵ忡 ���� ��ֹ� ������Ʈ�� �������� �̵���Ŵ

        // ���� �����ڸ��� ���� ��ֹ� ����
        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);

        }
    }

    // �÷��̾�� ������ ��ֹ� ������Ʈ ����
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Destroy(gameObject);
    }
}
