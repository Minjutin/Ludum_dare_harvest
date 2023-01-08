using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//If you are in top of this tile and press actionbutton, you will sacrifice the item on your hand. 

public class SacrificeTile : TileDaddy
{
    public Enums.God god { get; private set; }

    public SacrificeTile(Enums.God god2)
    {        
        god = god2;
    }
}
