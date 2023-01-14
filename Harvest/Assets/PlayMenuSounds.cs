using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMenuSounds : MonoBehaviour
{

    [SerializeField] AudioSource click;
    [SerializeField] AudioSource choose;
    public void PlayClick()
    {
        click.Play();
    }

    public void PlayChoose()
    {
        choose.Play();
    }
}
