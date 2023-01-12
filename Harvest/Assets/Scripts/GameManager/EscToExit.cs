using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscToExit : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if(this.name == "End")
        {
            if (Input.GetKey(KeyCode.Space)){
                SceneManager.LoadScene(0);
            }
        }
    }
}
