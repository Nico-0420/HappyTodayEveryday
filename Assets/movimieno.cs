using UnityEngine;

public class movimieno : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private float x;
    private float y;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        GetInput();
        Animar();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(x, y).normalized * moveSpeed;
    }

    private void GetInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
    }

    private void Animar()
    {
        Vector2 move = new Vector2(x, y);

        anim.SetBool("isagachado", move != Vector2.zero);

        if (x < 0) sr.flipX = true;
        else if (x > 0) sr.flipX = false;
    }
}
