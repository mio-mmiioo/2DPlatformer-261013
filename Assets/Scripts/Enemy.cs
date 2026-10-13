using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    private Animator anim = null;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
    }

    /// <summary>
    /// 敵の移動
    /// </summary>
    private void Walk()
    {
        float direction = 0.0f;
        bool isWalking = false;
        if (Keyboard.current.lKey.isPressed)
        {
            direction = 1.0f;
            sr.flipX = false;
            isWalking = true;
        }
        else if (Keyboard.current.jKey.isPressed)
        {
            direction = -1.0f;
            sr.flipX = true;
            isWalking = true;
        }
        anim.SetBool("isWalking", isWalking); // 歩いているのか止まっているのかをセットする

        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocityY);
    }
}
