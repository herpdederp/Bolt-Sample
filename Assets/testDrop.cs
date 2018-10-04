using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class testDrop : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int slot = -1;

    public void selectSlot()
    {
        if (true)
        {

            Debug.Log("w");

            if (GetComponentInParent<inventoryController>().selectedSlot == null && transform.childCount != 0)
                GetComponentInParent<inventoryController>().selectedSlot = gameObject;
            else if (GetComponentInParent<inventoryController>().selectedSlot == gameObject)
                GetComponentInParent<inventoryController>().selectedSlot = null;
            else
            {
                if (GetComponentInParent<inventoryController>().selectedSlot)
                {

                    var evnt = swapSlots.Create(Bolt.GlobalTargets.OnlyServer);

                    if (GetComponentInParent<inventoryController>().selectedSlot.GetComponent<testDrop>())
                        evnt.from = GetComponentInParent<inventoryController>().selectedSlot.GetComponent<testDrop>().slot;
                    else evnt.from = GetComponentInParent<inventoryController>().selectedSlot.GetComponent<testDrag>().slot;
                    evnt.to = slot;
                    evnt.Send();
                    GetComponentInParent<inventoryController>().selectedSlot = null;
                   
                }
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (GetComponentInParent<inventoryController>().selectedSlot == null)
            GetComponentInParent<inventoryController>().selectedSlot = gameObject;
        else if (GetComponentInParent<inventoryController>().selectedSlot == gameObject)
            GetComponentInParent<inventoryController>().selectedSlot = null;
        else
        {
            var evnt = swapSlots.Create(Bolt.GlobalTargets.OnlyServer);
            if (GetComponentInParent<inventoryController>().selectedSlot.GetComponent<testDrop>())
                evnt.from = GetComponentInParent<inventoryController>().selectedSlot.GetComponent<testDrop>().slot;
            else evnt.from = GetComponentInParent<inventoryController>().selectedSlot.GetComponent<testDrag>().slot;
            evnt.to = slot;
            evnt.Send();
            GetComponentInParent<inventoryController>().selectedSlot = null;
            EventSystem.current.SetSelectedGameObject(null);
        }


        //var evnt = swapSlots.Create(Bolt.GlobalTargets.OnlyServer);
        // evnt.from = eventData.pointerDrag.GetComponent<testDrag>().slot;
        // evnt.to = slot;
        // evnt.Send();

        //Destroy(eventData.pointerDrag);

        // Debug.Log("A");


        //eventData.pointerDrag.transform.SetParent(transform);
        //eventData.pointerDrag.transform.position = transform.position;



        // eventData.dragging.
        //DragHandler.itemBeingDragged.transform.SetParent(transform);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //  Debug.Log("B");
    }

    public void OnPointerExit(PointerEventData eventData)
    {


        // Debug.Log("C");
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
