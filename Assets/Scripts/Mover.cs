using UnityEngine;

// Mover.cs
// 이 스크립트는 게임 오브젝트를 왼쪽으로 이동시킴
public class Mover : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }
}
