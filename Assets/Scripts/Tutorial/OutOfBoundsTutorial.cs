using UnityEngine;

public class OutOfBoundsTutorial : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerReset>().Reset();
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
