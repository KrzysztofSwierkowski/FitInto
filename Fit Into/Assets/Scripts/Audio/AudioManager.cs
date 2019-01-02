using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private Sound _theme;

    [SerializeField]
    private Sound _wall;

    // Start is called before the first frame update
    void Awake()
    {
        InitializeSound(_theme);
        InitializeSound(_wall);
    }

    private void InitializeSound(Sound sound)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = sound.clip;
        audioSource.volume = sound.volume;
        audioSource.pitch = sound.pitch;
        audioSource.loop = sound.loop;
        sound.source = audioSource;
    }

    public void Start()
    {
        ResetMusic();
    }

    public void ResetMusic()
    {
        _theme.source.Stop();
        _theme.source.Play();
    }

    public void PlayWall()
    {
        _wall.source.Play();
    }

}
