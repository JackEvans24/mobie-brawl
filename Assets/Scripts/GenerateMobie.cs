using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateMobie : MonoBehaviour
{
    public GameObject mobie;
    public float spawnInterval;
    private bool spawning;

    void Update()
    {
        var anyAliveMobies = GameObject.FindGameObjectsWithTag("Mobie").Any(m => m.GetComponent<Enemy>().currentHealth > 0);
        if (!this.spawning && !anyAliveMobies)
        {
            this.spawning = true;
            StartCoroutine(this.SpawnMobie());
        }
    }

    private IEnumerator SpawnMobie()
    {
        yield return new WaitForSecondsRealtime(this.spawnInterval);

        var mobieObj = GameObject.Instantiate(this.mobie, this.transform.position, this.transform.rotation);

        this.spawning = false;
    }
}
