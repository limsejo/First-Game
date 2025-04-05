using UnityEngine;

// Spawner.cs
// 이 스크립트는 게임 오브젝트를 무작위로 생성함
public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    public float minSpawnDelay; // 새로운 객체를 생성하는 최소 시간
    public float maxSpawnDelay; // 새로운 객체를 생성하는 최대 시간

    [Header("References")]
    public GameObject[] gameObjects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }

    void Spawn(){
        // 객체의 리스트에서 무작위로 객체를 하나 뽑아오고, 그 객체를 인스턴스화한 것임
        GameObject randomObject = gameObjects[Random.Range(0, gameObjects.Length)];
        Instantiate(randomObject, transform.position, Quaternion.identity);

        // 클래스에서 특정 메소드를 불러올 수 있도록 해줌
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }
}
