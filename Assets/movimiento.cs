using UnityEngine;

public class movimiento : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private float x;
    private float y;

    private void Animar()
    {
    Vector2 move = new Vector2(x, y);

    anim.SetFloat("Speed", move.sqrMagnitude);
    anim.SetFloat("Horizontal", x);
    anim.SetFloat("Vertical", y);

    if (x < 0) sr.flipX = true;
    else if (x > 0) sr.flipX = false;
    }

    private void Start ()
    {
        rb= GetComponent<Rigidbody2D>();
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
        x= Input.GetAxisRaw("Horizontal");
        y= Input.GetAxisRaw("Vertical");
    }

}
