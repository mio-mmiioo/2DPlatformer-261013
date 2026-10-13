using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f; // 移動速度
    [SerializeField] private float jumpForce = 20.0f; // ジャンプの高さ

    private Rigidbody2D rb; // Rigidbody2Dの情報を入れる
    private SpriteRenderer sr; // SpriteRendererの情報を入れる

    public bool isOnGround = false; // 地面に足がついているならtrue
    private const string groundTag = "Ground"; // 地面タグの名前

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
        Jump();
    }
    /// <summary>
    /// プレイヤーの移動処理
    /// </summary>
    private void Walk()
    {
        float direction = 0.0f;
        if (Keyboard.current.aKey.isPressed)
        {
            direction = -1.0f;
            sr.flipX = false;
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            direction = 1.0f;
            sr.flipX = true; // 画像を反転させる
        }

        rb.linearVelocity = new Vector2(direction* moveSpeed, rb.linearVelocityY);
    }

    /// <summary>
    /// プレイヤーのジャンプ処理
    /// </summary>
    private void Jump()
    {
        if (Keyboard.current.spaceKey.isPressed && isOnGround == true)
        {
            isOnGround = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
       // 地面にぶつかっていないなら戻る
       if (!collision.gameObject.CompareTag(groundTag))
       {
            return;
       }

       for (int count = 0; count < collision.contactCount; count++)
       {
            ContactPoint2D contact = collision.GetContact(count);

            // 上向きの面に接触しているか確認
            if (contact.normal.y > 0.5f)
            {
                isOnGround = true;
                return;
            }
       }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            isOnGround = false;
        }
    }
}
