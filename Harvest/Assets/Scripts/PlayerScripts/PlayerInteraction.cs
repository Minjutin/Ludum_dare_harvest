using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    PlayerInventory inventory;

    //Chosen inventory slot
    int chosenSlot = 0;

    private void Awake()
    {
        inventory = this.GetComponent<PlayerInventory>(); //Fetch inventory
    }

    //TODO check if interaction button.
    private void InteractionButtonPressed()
    {
        //Tile tile = fetch tile we were are

        //TODO check if something has spawned on the spawnTile
        //if(tile is spawnTile) {
            //if(tile has InventoryItem){
                InventoryItem item = new InventoryItem(); //Get the item from the tile
                CollectItem(item);
        //}
        //}

        //TODO check if tile is fertile.
        //if(tile is fertileTile){
            //if tile has no plant and you have seed in your inventory slot, put it in
            //if tile has a plant and plant is ready, collect it.
            //otherwise be like "NO"
        //}

        //TODO check if you can sacrifice the fruit on the tile
        //if(you can sacrifice on this tile){
            //if you have inventory item chosen, give it to the gods
        //}
    }

    private void CollectItem(InventoryItem item)
    {
        inventory.AddItem(item);
    } //Collect item

    private void GiveItem()
    {
        if (inventory.thereIsItem(chosenSlot))
        {

        }
    }

    private void ChangeSlot()
    {
        switch (chosenSlot)
        {
            case 0:
                chosenSlot = 1;
                break;
            case 1:
                chosenSlot = 2;
                break;
            case 2:
                chosenSlot = 3;
                break;
            default:
                Debug.Log("There is no slot with number " + chosenSlot);
                break;
        }
    }
}
