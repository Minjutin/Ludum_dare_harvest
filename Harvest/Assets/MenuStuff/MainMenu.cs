using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject tutorialScreen;
    [SerializeField] GameObject selector, play, exit;

    GameObject currentObject;

    private void Start()
    {
        selector = GameObject.Find("Selector");
        play = GameObject.Find("Play");
        exit = GameObject.Find("Exit");

        currentObject = play;

        //Edit size
        currentObject.GetComponent<TextMeshProUGUI>().fontSize = 25;
        //Edit selector place.
        Vector3 newPos = new Vector3(selector.transform.position.x, currentObject.transform.position.y+1, selector.transform.position.z);

        selector.transform.position = newPos;
    }

    private void Update()
    {
        //If any move button is pressed
        if (Input.GetKeyDown(Statics.p1down) || Input.GetKeyDown(Statics.p1up) || Input.GetKeyDown(Statics.p2down) || Input.GetKeyDown(Statics.p2up))
        {
            
            if(currentObject == play)
            {
                play.GetComponent<TextMeshProUGUI>().fontSize = 18;
                currentObject = exit;
            }

            else if(currentObject == exit)
            {
                exit.GetComponent<TextMeshProUGUI>().fontSize = 18;
                currentObject = play;
            }

            //Edit size
            currentObject.GetComponent<TextMeshProUGUI>().fontSize = 25;

            //Edit selector place.
            Vector3 newPos = new Vector3(selector.transform.position.x, currentObject.transform.position.y+1, selector.transform.position.z);

            selector.transform.position = newPos;
        }
        
        

        //If any interaction button is pressed
        if(Input.GetKeyDown(Statics.p2interaction) || Input.GetKeyDown(Statics.p2inventory) ||Input.GetKeyDown(Statics.p1interaction) ||Input.GetKeyDown(Statics.p1inventory) || Input.GetKeyDown(KeyCode.Space))
        {

            Camera.main.GetComponent<PlayMenuSounds>().PlayClick();

            if(currentObject == play)
            {
                OpenSelection();
                this.gameObject.SetActive(false);
            }

            if(currentObject == exit)
            {
                ExitGame();
            }

            
        }
    }

    // Start is called before the first frame update
    public void ExitGame()
    {
        #if (UNITY_STANDALONE)
            Application.Quit();
        #elif (UNITY_WEBGL)
            Application.OpenURL("about:blank");
        #endif
    }

    // Update is called once per frame
    public void OpenSelection()
    {
        tutorialScreen.SetActive(true);
    }
}
