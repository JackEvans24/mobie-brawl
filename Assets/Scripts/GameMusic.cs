using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameMusic : MonoBehaviour
{
    public AudioClip startClip;
    public AudioClip loopClip;

    void Start()
    {
        var audio = GetComponent<AudioSource>();

        audio.clip = startClip;
        audio.Play();
    }

    void FixedUpdate()
    {
        var audio = GetComponent<AudioSource>();
        if (!audio.isPlaying)
        {
            audio.clip = loopClip;
            audio.loop = true;
            audio.Play();
        }
    }
}
