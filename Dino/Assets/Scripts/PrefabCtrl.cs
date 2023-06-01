using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ֹ� ������Ʈ(������) ��Ʈ�� ��ũ��Ʈ
public class PrefabCtrl : MonoBehaviour
{
    // ������ ����ü ����
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab; // ������ ���� ������Ʈ
        [Range(0f, 1f)] // (0~1����)
        public float spawnChance; // ���� Ȯ��(0~1���� �Է�)

    }

    public SpawnableObject[] objects; // ������ ����ü �迭

    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;

    // ������Ʈ�� Ȱ��ȭ�Ǿ� ���� ����
    private void OnEnable()
    {
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate)); // ������ ���� �Լ� ȣ��(1~2�� ���� ��������)
    }

    // ������Ʈ�� Ȱ��ȭ�Ǿ� ���� ���� ���� �Լ� ȣ�� �ߴ�
    private void OnDisable()
    {
        CancelInvoke();
    }

    // ������ ���� �Լ�
    private void Spawn()
    {
        float spawnChance = Random.value; // ���� ���� ��ȸ�� ���� ������ ���

        // ������ ����ü �迭�� ��� �ϳ��� �˻�
        foreach (var obj in objects)
        {
            if(spawnChance<obj.spawnChance) // ���� ���� ��ȸ�� ������ ����ü ����� ���� Ȯ������ ���� ��
            {
                GameObject obstacle = Instantiate(obj.prefab); // ��ֹ� ������ ����
                obstacle.transform.position += transform.position; // ��ֹ� ������ ��ġ ����
                break;
            }

            spawnChance -= obj.spawnChance; // ��ֹ� ������ ����ü ����� ���� Ȯ����ŭ ���� ���� ��ȸ���� ����
        }

        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate)); // 1~2�ʸ��� ���

    }
}
