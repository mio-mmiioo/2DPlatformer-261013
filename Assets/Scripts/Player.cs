using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f; // 移動速度
    [SerializeField] private float jumpForce = 5.0f; // ジャンプの高さ

    [SerializeField] private Transform groundCheck; // プレイヤーの足元に置く空のオブジェクトの位置
    [SerializeField] private LayerMask groundLayer; // 地面として認識するレイヤー
    private float checkRadius = 0.3f; // 足元の判定円の大きさ

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

    void FixedUpdate()
    {
        if (groundCheck != null)
        {
            isOnGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        }
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

        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocityY);
    }

    /// <summary>
    /// プレイヤーのジャンプ処理
    /// </summary>
    private void Jump()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isOnGround == true)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// Unityエディタ上でプレイヤーの足元の判定範囲を赤い円で表示する
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }
    }
}
