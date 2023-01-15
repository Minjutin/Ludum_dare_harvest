using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscToExit : MonoBehaviour
{

    bool isEnd = false;

    private void OnEnable()
    {
        StartCoroutine(LetThemExit());
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if (this.name == "End" && isEnd)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(Statics.p1interaction) || Input.GetKey(Statics.p2interaction) || Input.GetKey(Statics.p1inventory) || Input.GetKey(Statics.p2inventory))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    // Update is called once per frame
    IEnumerator LetThemExit()
    {
        yield return new WaitForSeconds(1f);
        isEnd = true;

    }
}
