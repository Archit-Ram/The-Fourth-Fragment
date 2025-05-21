using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource Source;
    public AudioSource BackgroundMusicSource;
    public AudioClip[] audioClips;
    public AudioClip[] BackgroundMusicClips;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(BackgroundMusicClips.Length > 0)
        {
            StartCoroutine(PlayBackgroundMusic());
        }
        BackgroundMusicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayAudioOnce(int index)
    {
        Source.loop = false;
        Source.PlayOneShot(audioClips[index]);
    }

    public void PlayAudioLoop(int index)
    {
        if ((!Source.isPlaying) || (!(Source.clip == audioClips[index])))
        {
            Source.clip = audioClips[index];
            Source.loop = true;
            Source.Play();
        }
    }

    public void StopPlaying()
    {
        Source.Stop();
    }

    IEnumerator PlayBackgroundMusic()
    {
        float MaxIndex = audioClips.Length;
        int randomValue = Random.Range(0, BackgroundMusicClips.Length);
        BackgroundMusicSource.clip = BackgroundMusicClips[randomValue];
        BackgroundMusicSource.Play();
        yield return new WaitForSeconds(30f);
        StartCoroutine(PlayBackgroundMusic());
    }
}
