using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] AttackClips;
    [SerializeField]
    private AudioClip fireClip;
    [SerializeField]
    private AudioClip HeavyClip;
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Attacks()
    {
        AudioClip clip = GetRandomClip();
        source.PlayOneShot(clip);
    }

    private void Fire()
    {
        AudioClip clip = fireClip;
        source.PlayOneShot(clip);
    }

    private void Heavy()
    {
        AudioClip clip = HeavyClip;
        source.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip()
    {
        return AttackClips[UnityEngine.Random.Range(0, AttackClips.Length)];
    }


}
