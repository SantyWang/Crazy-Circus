using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    // 游戏开始
    public AudioClip start;
    // 循环背景音乐
    public AudioClip background;
    // 游戏结束音效
    public AudioClip end;
    // 陷阱
    public AudioClip trap;
    // 道具
    public AudioClip prop;
    // 左脚
    public AudioClip footLeft;
    // 右脚
    public AudioClip footRight;
    // 跳跃
    public AudioClip jump;

    private AudioSource audioSource;
    private AudioSource effectSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        effectSource = GameObject.Find("AudioEffect").GetComponent<AudioSource>();

        // this.PlayBackground();

        // Invoke("PlayGameEnd", 5f);
        // Invoke("PlayTrap", 6f);
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource.isPlaying)
        {
            return;
        }
        if (audioSource.clip == end)
        {
            audioSource.clip = start;
            audioSource.Play();
        }
        else
        {
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

    public void PlayFootLeft()
    {
        if (footLeft != null) {
            effectSource.PlayOneShot(footLeft, Random.Range(0.05f, 0.1f));
        }
    }

    public void PlayFootRight()
    {
        if (footRight != null) {
            effectSource.PlayOneShot(footRight, Random.Range(0.05f, 0.1f));
        }
    }

    public void PlayJump()
    {
        if (jump != null) {
            effectSource.PlayOneShot(jump, 1);
        }
    }

    public void PlayGameEnd()
    {
        audioSource.Stop();
        audioSource.clip = end;
        audioSource.Play();
    }
}
