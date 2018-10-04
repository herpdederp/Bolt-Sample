using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class inventoryController : MonoBehaviour
{
    public GameObject selectedSlot;
    public itemDB ItemDB;

    public List<GameObject> mySlots;
    public playerController PlayerController;
    public GameObject slot;
    public GameObject item;

    // Use this for initialization
    void Start()
    {
        ItemDB = GameObject.Find("ItemDB").GetComponent<itemDB>();


        /*
        float x = -300f;
        for (int i = 0; i < 5; i++)
        {
            x += 110f;
            instantiateSlot(new Vector3(x, 0, 0));
        }

        instantiateItem(1);
        */
    }

    public void refreshItem(int i)
    {

        if (PlayerController.state.items[i].ID != 0)
            instantiateItem(i, PlayerController.state.items[i].ID, PlayerController.state.items[i].quantity);
        else clearItem(i);

    }

    public void refreshItems()
    {
        for (int i = 0; i < mySlots.Count; i++)
        {
            refreshItem(i);
        }
    }

    public void clearItem(int slotID)
    {
        if (mySlots.Count <= slotID)
        {
            //Debug.LogError("Doesn't exist");
            return;
        }

        foreach (Transform child in mySlots[slotID].transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void instantiateItem(int slotID, int ID, int quantity)
    {
        if (mySlots.Count <= slotID)
            return; //doesnt exist

        //clear the children
        foreach (Transform child in mySlots[slotID].transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        //check if slot exists and is available (on server, this is just ui logic)

        GameObject newItem = Instantiate(item);
        itemData ItemData = lookUpID(ID);
        if (ItemData != null)
        {
            if (ItemData.icon != null)
                newItem.GetComponent<Image>().sprite = ItemData.icon;
        }

        if (quantity > 1)
            newItem.transform.GetChild(0).GetComponent<Text>().text = quantity.ToString();
        newItem.GetComponent<testDrag>().slot = slotID;
        newItem.transform.SetParent(mySlots[slotID].transform);
        newItem.GetComponent<RectTransform>().localPosition = Vector3.zero;
        // newSlot.GetComponent<RectTransform>().localPosition = position;
        //  mySlots.Add(newSlot);
    }

    public void instantiateSlot(Vector3 position, int ID)
    {
        GameObject newSlot = Instantiate(slot);
        newSlot.GetComponent<testDrop>().slot = ID;
        newSlot.transform.SetParent(transform);
        newSlot.GetComponent<RectTransform>().localPosition = position;
        mySlots.Add(newSlot);


    }

    public void instantiateSlots(int count)
    {
        mySlots.Clear();
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        float x = -300f;
        for (int i = 0; i < count; i++)
        {
            x += 110f;
            instantiateSlot(new Vector3(x, 0, 0), i);

            if (i == 0)
                EventSystem.current.SetSelectedGameObject(mySlots[0]);
        }
        //need to regenrate items at the end?
        refreshItems();
    }

    public itemData lookUpID(int ID)
    {
        foreach (itemData ItemData in ItemDB.itemDatabase)
        {
            if (ItemData.ID == ID)
            {
                return ItemData;

            }
        }

        return null;

    }
}
