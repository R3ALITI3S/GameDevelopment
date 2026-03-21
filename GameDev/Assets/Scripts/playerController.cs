using Unity.Cinemachine;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;

    public float groundCheckDistance = 2f;

    bool isGrounded;
    float x;
    float z;

    public static playerController Instance;
    [SerializeField] private CinemachineCamera cam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();


        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            return;
        }

        Instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
        GameObject.DontDestroyOnLoad(this.cam); // work in progress
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
                // Apply jump force to the player
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, StatsManager.Instance.jumpForce, rb.linearVelocity.z);
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
        // Ground checking
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, terrainLayer);

        Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, Color.red); // draws a ray in the editor 

        // Movement
        rb.linearVelocity = new Vector3(x * StatsManager.Instance.speed, rb.linearVelocity.y, z * StatsManager.Instance.speed);
    }
}