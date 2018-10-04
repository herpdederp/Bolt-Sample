using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryCallbacks : Bolt.GlobalEventListener
{
    public playerController PlayerController;

    public void Start()
    {
        PlayerController = GameObject.Find("inventoryEntity").GetComponent<playerController>();
    }

    public override void OnEvent(swapSlots evnt)
    {
        PlayerController.swapSlots(evnt);
    }
    public override void OnEvent(destroySlot evnt)
    {
        PlayerController.state.items[evnt.slot].ID = 0;
        PlayerController.myInventory.refreshItem(evnt.slot);
    }
}
