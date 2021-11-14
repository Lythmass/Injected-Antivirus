using UnityEngine;

public class Corona : MonoBehaviour
{
    public static bool isOver;
    private DUDE player;
    public float speed, dist;
    private Vector2 box1, box2;
    private Vector3 startPos;
    private float x, y, z, w;
    private bool isOut;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<DUDE>();
        box1 = new Vector2(transform.position.x + 0.5f, transform.position.y + 0.5f);
        box2 = new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f);
        startPos = transform.position;
        x = Random.Range(-0.2f, 0.2f);
        y = Random.Range(-0.2f, 0.2f);
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(z, w), speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, new Vector3(z, w)) <= 0.025f)
        {
            startPos = transform.position;
            x = Random.Range(-0.2f, 0.2f);
            y = Random.Range(-0.2f, 0.2f);
            z = Mathf.Clamp(startPos.x + x, box2.x, box1.x);
            w = Mathf.Clamp(startPos.y + y, box2.y, box1.y);
        }
    }
    void OnMouseOver()
    {
        isOver = true;
    }
    void OnMouseExit()
    {
        isOver = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            isOver = false;
        }
    }
}
