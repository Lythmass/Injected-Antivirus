using UnityEngine;

public class BloodWater : MonoBehaviour
{
    public static bool isUnderBlood;
    void Start()
    {
        isUnderBlood = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isUnderBlood = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isUnderBlood = false;
        }
    }
}
