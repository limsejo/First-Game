using UnityEngine;

// Player.cs
// 이 스크립트는 플레이어의 점프 기능을 담당함
public class Player : MonoBehaviour
{
    [Header("Settings")]
    public float JumpForce;

    [Header("References")]
    public Rigidbody2D PlayerRigidBody;
    public Animator PlayerAnimator;

    public BoxCollider2D PlayerCollider;

    private bool isGrounded = true; // 땅에 있는지 체크하는 변수
    public int lives = 3; // 플레이어의 생명 수
    public bool isInvincible = false; // 플레이어가 무적 상태인지 체크하는 변수

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
    
    void KillPlayer() {
        PlayerCollider.enabled = false; // 플레이어의 Collider 비활성화
        PlayerAnimator.enabled = false; // 플레이어의 Animator 비활성화
        PlayerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse);
    }

    void Hit() {
        lives -= 1;
        if (lives == 0) {
            KillPlayer();
        }
    }

    void Heal() {
        lives = Mathf.Min(3, lives + 1);
    }

    void StartInvincible() {
        isInvincible = true;
        Invoke("StopInvincible", 5f); // 5초 후에 무적 상태 해제
    }

    void StopInvincible() {
        isInvincible = false;
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

    // Trigger와 충돌했을 때 호출되는 메서드
    void OnTriggerEnter2D(Collider2D collider) {

        if (collider.gameObject.tag == "enemy") {

            // 플레이어가 무적 상태가 아닐 때만 적과 충돌
            if (!isInvincible) {
                Destroy(collider.gameObject);
            }
            Hit();
        }
        else if (collider.gameObject.tag == "food") {
            Destroy(collider.gameObject);
            Heal();
        }
        else if (collider.gameObject.tag == "golden") {
            Destroy(collider.gameObject);
            StartInvincible();
        }
        
    }
}
