using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainCamera;
    public float panSpeed = 3f;
    private bool skipIntro = false;

    private void FixedUpdate()
    {
        mainCamera.transform.position = new Vector3(
            mainCamera.transform.position.x + panSpeed,
            mainCamera.transform.position.y,
            mainCamera.transform.position.z
        );
    }

    public void StartGame()
    {
        SceneManager.LoadScene(this.skipIntro ? (int)Scenes.MainGame : (int)Scenes.Tutorial);
    }

    public void ToggleSkipIntro()
    {
        this.skipIntro = !this.skipIntro;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
