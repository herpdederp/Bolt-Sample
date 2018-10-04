using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class testDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, IPointerDownHandler
{
    public int slot = -1;
    Transform startParent;
    Vector3 startPosition;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("w");

        if (eventData.button == PointerEventData.InputButton.Left)
            Debug.Log("Left click");
        else if (eventData.button == PointerEventData.InputButton.Middle)
            Debug.Log("Middle click");
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right click");
            var evnt = destroySlot.Create(Bolt.GlobalTargets.OnlyServer);
            evnt.slot = eventData.pointerDrag.GetComponent<testDrag>().slot;
            evnt.Send();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Q");
        GetComponent<Image>().raycastTarget = false;
        startParent = transform.parent;
        startPosition = transform.position;

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
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("W");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponentInParent<inventoryController>().selectedSlot = null;

        Debug.Log("E");
        GetComponent<Image>().raycastTarget = true;

        transform.localPosition = Vector3.zero;

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("we");
    }
}
