using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    // 游戏开始
    public AudioClip start;
    // 循环背景音乐
    public AudioClip background;
    // 陷阱
    public AudioClip trap;
    // 道具
    public AudioClip prop;

    private AudioSource audioSource;
    private AudioSource effectSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        effectSource = GameObject.Find("AudioEffect").GetComponent<AudioSource>();

        this.PlayBackground();

        // Invoke("PlayBackground", 5f);
        // Invoke("PlayTrap", 6f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying && audioSource.clip != null) {
            audioSource.clip = background;
            audioSource.Play();
        }
    }

    // 播放背景音乐，重复执行会重新播放
    public void PlayBackground() {
        audioSource.Stop();
        audioSource.clip = start;
        audioSource.Play();
    }

    public void PlayTrap()
    {
        if (trap != null) {
            effectSource.PlayOneShot(trap, 1);
        }
    }

    public void PlayProp()
    {
        if (prop != null) {
            effectSource.PlayOneShot(prop, 1);
        }
    }
}
