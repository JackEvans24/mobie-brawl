using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ui;
    public GameObject gameOverCanvas;

    public Text murderValueText;
    public Text gameOverMurderValueText;
    public Text gameOverCommentText;
    private int mobieMurderCount;

    private void Update()
    {
        if (this.gameOverCanvas.activeInHierarchy && Input.GetButton("Jump"))
            RestartLevel();
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

    public void GameOver()
    {
        this.ui.SetActive(false);
        this.gameOverCanvas.SetActive(true);
    }
}
