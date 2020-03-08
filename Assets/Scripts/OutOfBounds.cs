using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(GameManager.RestartInSeconds(1));
        }
        else if (other.gameObject.tag == "Mobie")
        {
            other.GetComponent<Enemy>().DieExternal();
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
