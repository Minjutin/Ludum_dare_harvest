using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField] GameObject endImage;

    //Open the ending screen.
    public void OpenEnding(Sprite god)
    {
        endImage.GetComponent<Image>().sprite = god;
        endImage.SetActive(true);
    }
}
