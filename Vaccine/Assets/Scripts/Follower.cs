using UnityEngine;

public class Follower : MonoBehaviour
{
    Vector3 offset;
    public Transform player;
    void Start()
    {
        offset = transform.position - player.position;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, player.position.y - 4, player.position.y + 4f), -10);
        if(DUDE.isRaging)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 10, 2.5f * Time.deltaTime);
        } else
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 5, 2.5f * Time.deltaTime);
        }

    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + offset, 5f * Time.unscaledDeltaTime);
    }

}
