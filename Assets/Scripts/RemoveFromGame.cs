using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveFromGame : MonoBehaviour
{
    public float removeAfter = 5f;
    
    public IEnumerator Remove()
    {
        yield return new WaitForSecondsRealtime(removeAfter);
        Destroy(this.gameObject);
    }
}
