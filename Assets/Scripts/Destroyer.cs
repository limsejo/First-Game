using UnityEngine;

// Destroyer.cs
// 이 스크립트는 게임 오브젝트를 파괴함
public class Destroyer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -15)
        {
            Destroy(gameObject);
        }
    }
}
