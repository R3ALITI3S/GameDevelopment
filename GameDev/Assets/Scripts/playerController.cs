using Unity.Cinemachine;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 10f;

    public LayerMask terrainLayer;
    public Rigidbody rb;
    private SpriteRenderer[] srs;
    public float groundCheckDistance = 2f;

    public Animator anim;

    bool isGrounded;
    float x;
    float z;

    public static playerController Instance;
    [SerializeField] private CinemachineCamera cam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        srs = GetComponentsInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();

        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            return;
        }

        Instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
        GameObject.DontDestroyOnLoad(this.cam); 
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Fight");
        }

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

        if (x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (x > 0)
            transform.localScale = new Vector3(1, 1, 1);

        // animation (float-based)
        float SpeedWolf = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).magnitude;

        anim.SetFloat("Speed", SpeedWolf);

        Debug.Log(SpeedWolf);
    }

    void FixedUpdate()
    {
        // ground checking
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, terrainLayer);

        Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, Color.red); // draws a ray in the editor 

        // moveement
        rb.linearVelocity = new Vector3(x * speed, rb.linearVelocity.y, z * speed);

        // animation based on actual movement
        float horizontalSpeed = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).magnitude;
    }
}