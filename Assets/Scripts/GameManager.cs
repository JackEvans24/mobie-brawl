using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text murderValueText;
    private int mobieMurderCount;

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
        this.murderValueText.text = this.mobieMurderCount.ToString();
    }
}
