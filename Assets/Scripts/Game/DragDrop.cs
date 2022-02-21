using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Transform itemTransform;
    //private CanvasGroup canvasGroup;
    private Vector3 defaultPos;

    public bool droppedOnCorrectSlot;

    private void Awake()
    {
        itemTransform = GetComponent<Transform>();
        //canvasGroup = GetComponent<CanvasGroup>();
    }
    private void Start()
    {
       
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        Debug.Log("default pos on onpointer down is: " + defaultPos);
        defaultPos = itemTransform.position;
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Makes sure dragged object doesn't block raycasting so a drop can be detected.
        //canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //event.delta containts movement delta of mouse movement since the last frame.
        //transform.position += eventData.useDragThreshold;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //canvasGroup.blocksRaycasts = true;
        Debug.Log("default pos is: " + defaultPos);
        if (droppedOnCorrectSlot == false)
        {
            transform.position = defaultPos;
        }
    }
}
