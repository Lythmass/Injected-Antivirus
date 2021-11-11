using UnityEngine;
using UnityEngine.SceneManagement;
public class DUDE : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed, jumpForce, radius;
    public LayerMask whatIsGround;
    public Transform detector;
    private float movement;
    public GameObject xray;
    public float publicJumpCount, maxHealth;
    private float jumpCount, health;
    private bool isKilling = false;
    public static bool isRaging;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpCount = publicJumpCount;
        health = maxHealth;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            xray.SetActive(true);
            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            xray.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        if (Input.GetMouseButton(1) && Time.timeScale >= 0.05f)
        {
            Time.timeScale -= Time.unscaledDeltaTime * 7f;
        }
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        Time.timeScale += Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0.05f, 1f);
        if(!Input.GetMouseButton(1))
        {
            xray.SetActive(false);
        }
        
        if (Corona.isOver && Input.GetMouseButtonDown(0) && Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position) <= 10f && xray.activeInHierarchy)
        {
            isKilling = true;
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        }
        if (Input.GetMouseButtonUp(0) && isKilling)
        {
            isKilling = false;
        }
        movement = Input.GetAxisRaw("Horizontal");
        Collider2D ground = Physics2D.OverlapCircle(detector.position, radius, whatIsGround);
        if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))&& jumpCount > 0)
        {
            rb.velocity = Vector2.up * jumpForce * 0.02f;
            jumpCount--;
        }
        if(ground)
        {
            jumpCount = publicJumpCount;
        }
        if(health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        Debug.Log(health);
        if(BloodWater.isUnderBlood)
        {
            health -= Time.deltaTime * 0.5f;
        }
    }
    void FixedUpdate()
    {
        if (BloodWater.isUnderBlood)
        {
            if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
            {
                rb.AddForce(Vector2.up * jumpForce * Time.deltaTime);
            }
        }
        rb.velocity = new Vector2(movement * speed, rb.velocity.y);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detector.position, radius);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Virus") && !isKilling)
        {
            health--;
        }
        if(other.gameObject.CompareTag("Instant Death"))
        {
            health = -1;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Rage Zone"))
        {
            speed *= 2;
            jumpForce *= 1.5f;
            isRaging = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Rage Zone"))
        {
            speed /= 2;
            jumpForce /= 1.5f;
            isRaging = false;
        }
    }
}
