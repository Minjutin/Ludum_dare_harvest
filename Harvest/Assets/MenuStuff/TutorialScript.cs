using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    int currentSlide = 0;

    [SerializeField] List<Sprite> slides;
    [SerializeField] GameObject chooser;

    Image thisSprite;

    PlayMenuSounds click;

    // Update is called once per frame
    private void Start()
    {
        thisSprite = transform.Find("Background").gameObject.GetComponent<Image>();

        click = Camera.main.GetComponent<PlayMenuSounds>();
    }

    void Update()
    {
        //Continue if any interaction button is pressed
        if (Input.GetKeyDown(Statics.p2interaction) || Input.GetKeyDown(Statics.p2inventory) || Input.GetKeyDown(Statics.p1interaction) || Input.GetKeyDown(Statics.p1inventory) || Input.GetKeyDown(KeyCode.Space))
        {
            click.PlayClick();
            

            //If there are any tutorial slides left
            if(currentSlide < slides.Count - 1)
            {
                currentSlide++;
                thisSprite.sprite = slides[currentSlide];
            }

            //Otherwise open the character choose

            else
            {
                chooser.SetActive(true);
                this.gameObject.SetActive(false);
            }


        }
    }
}
