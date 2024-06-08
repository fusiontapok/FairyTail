using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower; // ������������� ���� ������
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float horizontalInput;

    private void Awake()
    {
        // �������� ������ �� Rigidbody � Animator
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // �������������� ������ ��� �������� �����-������
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // ������������� ��������� ���������
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        // ������ ������������
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // ������ ������
        if (isGrounded() && Input.GetKey(KeyCode.Space))
            Jump();
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpPower); // ���������� ������������� ���� ������
        anim.SetTrigger("jump");
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded();
    }
}
