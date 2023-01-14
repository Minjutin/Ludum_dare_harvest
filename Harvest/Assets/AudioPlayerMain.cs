using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerMain : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource swap, plant, sacrifice, pick;
    
    public void PlaySwap()
    {
        swap.Play();
    }
    public void PlayPlant()
    {
        plant.Play();
    }
    public void PlaySacrifice()
    {
        sacrifice.Play();
    }
    public void PlayPick()
    {
        pick.Play();
    }
}
