using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource BGMSource;
    public AudioSource effectSource;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.soundManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
