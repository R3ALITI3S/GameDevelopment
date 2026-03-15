using UnityEngine;

public class playerController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 10f;

    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;

    public float groundCheckDistance = 2f;

    bool isGrounded;
    float x;
    float z;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed");

            if (isGrounded)
            {
                Debug.Log("Jumping");
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            }
            else
            {
                Debug.Log("Not grounded");
            }
        }

        // Flip the sprite and keep it tht way until other
        if (x < 0)
            sr.flipX = true;
        else if (x > 0)
            sr.flipX = false;
    }

    void FixedUpdate()
    {
        // ground checking
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, terrainLayer);

        Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, Color.red); // draws a ray in the editor 

        // moveement
        rb.linearVelocity = new Vector3(x * speed, rb.linearVelocity.y, z * speed);
    }
}