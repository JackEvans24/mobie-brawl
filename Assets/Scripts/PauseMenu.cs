using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseMenuCanvas;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel")) {
            if (isPaused) {
                this.Resume();
            } else {
                this.Pause();
            }
        }
    }

    void Pause() {
        isPaused = true;
        Time.timeScale = 0;
        pauseMenuCanvas.SetActive(true);
    }

    public void Resume() {
        isPaused = false;
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart() {
        isPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene((int)Scenes.MainGame);
    }

    public void Quit() {
        isPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene((int)Scenes.MainMenu);
    }
}
