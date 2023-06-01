using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 장애물 오브젝트(프리팹) 컨트롤 스크립트
public class PrefabCtrl : MonoBehaviour
{
    // 프리팹 구조체 생성
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab; // 프리팹 게임 오브젝트
        [Range(0f, 1f)] // (0~1까지)
        public float spawnChance; // 생성 확률(0~1까지 입력)

    }

    public SpawnableObject[] objects; // 프리팹 구조체 배열

    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;

    // 오브젝트가 활성화되어 있을 때만
    private void OnEnable()
    {
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate)); // 프리팹 스폰 함수 호출(1~2초 랜덤 간격으로)
    }

    // 오브젝트가 활성화되어 있지 않을 때는 함수 호출 중단
    private void OnDisable()
    {
        CancelInvoke();
    }

    // 프리팹 생성 함수
    private void Spawn()
    {
        float spawnChance = Random.value; // 현재 스폰 기회를 랜덤 값으로 배당

        // 프리팹 구조체 배열의 요소 하나씩 검사
        foreach (var obj in objects)
        {
            if(spawnChance<obj.spawnChance) // 현재 스폰 기회가 프리팹 구조체 요소의 스폰 확률보다 작을 때
            {
                GameObject obstacle = Instantiate(obj.prefab); // 장애물 프리팹 생성
                obstacle.transform.position += transform.position; // 장애물 프리팹 위치 지정
                break;
            }

            spawnChance -= obj.spawnChance; // 장애물 프리팹 구조체 요소의 스폰 확률만큼 현재 스폰 기회에서 제외
        }

        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate)); // 1~2초마다 재귀

    }
}
