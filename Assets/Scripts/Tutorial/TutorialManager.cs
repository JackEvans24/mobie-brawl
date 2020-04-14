using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public Rigidbody2D text;
    private bool tutorialFinished;

    void Update()
    {
        if (tutorialFinished && Input.GetButton("Jump")) {
            this.StartGame();
        } else {
            var mobies = GameObject.FindGameObjectsWithTag("Mobie").Where(m => m.GetComponent<Enemy>().currentHealth > 0);

            if (mobies.Count() <= 0) {
                this.tutorialFinished = true;
                this.text.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

    private void StartGame() {
        SceneManager.LoadScene((int)Scenes.MainGame);
    }
}
