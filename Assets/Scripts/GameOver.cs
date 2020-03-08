using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameManager manager;
    public Image background;
    public Text gameOverText;
    public Text youMurderedText;
    public Text murderCountText;
    public Text mobiesText;
    public Text commentText;
    public Text restartText;
    private Text[] textObjects;

    public float backgroundAlpha = 100f;
    public float textShowInterval = 0.3f;
    private bool showTextStarted;

    void Start()
    {
        this.textObjects = new[] {
            gameOverText,
            youMurderedText,
            murderCountText,
            mobiesText,
            commentText,
            restartText
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (background.color.a <= backgroundAlpha && !showTextStarted) {
            showTextStarted = true;
            StartCoroutine(ShowNextText(0));
        }
    }

    IEnumerator ShowNextText(int i)
    {
        if (i >= textObjects.Length)
            yield break;

        yield return new WaitForSecondsRealtime(textShowInterval);

        this.textObjects[i].enabled = true;
        StartCoroutine(ShowNextText(++i));
    }
}
