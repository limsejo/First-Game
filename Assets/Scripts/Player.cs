using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    public float JumpForce;

    [Header("References")]
    public Rigidbody2D PlayerRigidBody;
    public Animator PlayerAnimator;

    private bool isGrounded = true; // 땅에 있는지 체크하는 변수

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 유저가 스페이스 키 누르는 것 감지
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // 점프
            PlayerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            PlayerAnimator.SetInteger("state", 1);
        }
    }

    // 두 개의 Collider가 충돌하면 자동으로 호출되는 메서드 (게임 시작하자마자 실행)
    void OnCollisionEnter2D(Collision2D collision) {
        
        if (collision.gameObject.name == "Platform") {
            if (!isGrounded) {
                PlayerAnimator.SetInteger("state", 2);
            }
            isGrounded = true;
        }
    }
}
