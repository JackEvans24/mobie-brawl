using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static IEnumerator RestartInSeconds(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        GameManager.RestartLevel();
    }

    public static void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
