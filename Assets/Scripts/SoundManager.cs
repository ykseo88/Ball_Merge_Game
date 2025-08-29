using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource BGMSource;
    public float BGMVolume;
    public float SFXVolume;
    public Queue<AudioSource> audioSources = new Queue<AudioSource>();
    [SerializeField] private GameObject audioSourcePrefab;
    // Start is called before the first frame update

    void Start()
    {
        GameManager.instance.soundManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSound(AudioClip clip)
    {
        for (int i = 0; i < audioSources.Count; i++)
        {
            AudioSource temp = audioSources.Dequeue();
            if (!temp.isPlaying)
            {
                audioSources.Enqueue(temp);
                temp.volume = SFXVolume;
                temp.PlayOneShot(clip);
                return;
            }
            audioSources.Enqueue(temp);
        }

        GameObject tempAudio = Instantiate(audioSourcePrefab, transform, true);
        tempAudio.transform.TryGetComponent(out AudioSource audioSource);
        audioSources.Enqueue(audioSource);
        audioSource.clip = clip;
        audioSource.volume = SFXVolume;
        audioSource.PlayOneShot(clip);
    }
}
