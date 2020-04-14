using UnityEngine;

public class ShowTextOnTrigger : MonoBehaviour
{
    public Rigidbody2D text;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            this.text.bodyType = RigidbodyType2D.Dynamic;
    }
}
