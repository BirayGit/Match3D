using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrop : MonoBehaviour, IDropHandler
{
    private DragDrop dragDrop;

    private void Awake()
    {
        dragDrop = FindObjectOfType<DragDrop>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");

        //snaps the dragged item into position
        Debug.Log("value of dropped on correct slot is: " + dragDrop.droppedOnCorrectSlot);
        if (eventData.pointerDrag != null && dragDrop.droppedOnCorrectSlot)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }

        //eventData.pointerDrag.GetComponent<DragDropMouse>().droppedOnCorrectSlot = true;
    }

}
