using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject mobie;

    public GameObject ui;
    public GameObject gameOverCanvas;

    public Text murderValueText;
    public Text gameOverMurderValueText;
    public Text gameOverCommentText;
    private int mobieMurderCount;

    public float mobieSpawnInterval;
    private float mobieSpawnIntervalRemaining;
    public float mobieLimit = 10;

    private void Start()
    {
        this.mobieSpawnIntervalRemaining = this.mobieSpawnInterval;
    }

    private void Update()
    {
        if (this.gameOverCanvas.activeInHierarchy)
        {
            if (Input.GetButton("Jump"))
                RestartLevel();
            else if (Input.GetButton("Escape"))
                Application.Quit();
        }

        if (!this.gameOverCanvas.activeInHierarchy)
            SpawnMobie();
    }

    public static IEnumerator RestartInSeconds(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        GameManager.RestartLevel();
    }

    public static void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void IncrementMurderCount()
    {
        this.mobieMurderCount++;

        if (mobieMurderCount % 3 == 0)
        {
            this.mobieLimit++;
            if (this.mobieSpawnInterval > 0.5)
                this.mobieSpawnInterval -= 0.2f;
        }

        this.murderValueText.text = this.mobieMurderCount.ToString();
        this.gameOverMurderValueText.text = this.mobieMurderCount.ToString();
        this.SetCommentText();
    }

    private void SetCommentText()
    {
        var comment = string.Empty;

        if (mobieMurderCount < 5) comment = "Pathetic";
        else if (mobieMurderCount < 10) comment = "Weak";
        else if (mobieMurderCount < 15) comment = "Eh";
        else if (mobieMurderCount < 20) comment = "Could be better";
        else if (mobieMurderCount < 30) comment = "Not bad";
        else if (mobieMurderCount < 40) comment = "Keep trying";
        else if (mobieMurderCount < 50) comment = "Getting there";
        else if (mobieMurderCount < 75) comment = "That's more like it";
        else comment = "Ok wow, didn't you get bored?";

        this.gameOverCommentText.text = $"({comment})";
    }

    private void SpawnMobie()
    {
        var mobies = GameObject.FindGameObjectsWithTag("Mobie").Where(m => m.GetComponent<Enemy>().currentHealth > 0);

        if (mobieSpawnIntervalRemaining > 0)
        {
            mobieSpawnIntervalRemaining -= Time.deltaTime;
            return;
        }
        else if (mobieLimit > mobies.Count())
        {
            var spawnPoints = GameObject.FindGameObjectsWithTag("MobieGenerator");
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count())];

            GameObject.Instantiate(mobie, spawnPoint.transform.position, spawnPoint.transform.rotation);

            mobieSpawnIntervalRemaining = mobieSpawnInterval;
        }
    }

    public void GameOver()
    {
        this.ui.SetActive(false);
        this.gameOverCanvas.SetActive(true);
    }
}
