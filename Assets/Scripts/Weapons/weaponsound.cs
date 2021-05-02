using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponsound : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource An;
    [SerializeField] AudioClip Reload, Shoot;
    public void Start()
    {
        An = GetComponent<AudioSource>();
    }
    public void reloadsound()
    {
        An.clip = Reload;
        An.Play();
    }
    public void Shootsound()
    {
        An.clip = Shoot;
        An.Play();
    }
}
