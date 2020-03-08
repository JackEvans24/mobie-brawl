using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerLife>().Respawn();
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
