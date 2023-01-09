using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject selectionScreen;

    // Start is called before the first frame update
    void ExitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    public void OpenSelection()
    {
        selectionScreen.SetActive(true);
    }
}
