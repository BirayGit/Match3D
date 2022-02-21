using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class TouchManager : MonoBehaviour
{
    GameObject selectedItem;
    SortingGroup draggedItemSortingLayer;
    Rigidbody selectedItemRB;
    Vector3 selectedItemLocalPosition;
    bool isDragging;
    

    private void Start()
    {
        isDragging = false;
        LevelManager.Instance.levelStarted = false;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && LevelManager.Instance.levelStarted)
        {
            //Debug.Log("touch detected");


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                Debug.Log("item clicked");
                selectedItem = hit.collider.gameObject;
                isDragging = true;

                draggedItemSortingLayer = selectedItem.GetComponent<SortingGroup>();
                SetSortingLayer("DraggedItem");

                selectedItemRB = selectedItem.GetComponent<Rigidbody>();
                SetRBForDragging();

                selectedItemLocalPosition = selectedItem.transform.localPosition;
                selectedItem.transform.localPosition = new Vector3(selectedItemLocalPosition.x, 0.7f, selectedItemLocalPosition.z);
            }
        }

        if (isDragging && Input.GetMouseButton(0))
        {
            if (selectedItem != null)
            {
                Plane movementPlane = new Plane(Vector3.up, selectedItem.transform.position);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                float distance = 0f;
                if (movementPlane.Raycast(ray, out distance))
                {
                    Vector3 position = ray.GetPoint(distance);
                    selectedItem.transform.position = position;
                }
            }
            

        }

        if (Input.GetMouseButtonUp(0) && LevelManager.Instance.levelStarted)
        {
            isDragging = false;
            //Debug.Log("getmousebuttonup is called");
            //if (!EventSystem.current.IsPointerOverGameObject())

            if (draggedItemSortingLayer != null && selectedItem != null)
            {
                RaycastHit hit;
                float maxRange = 7f;
                Vector3 direction = new Vector3(0, -1, 0);
                Debug.DrawRay(selectedItem.transform.position, direction * maxRange, Color.green);
                //Debug.Log("drawing the ray");
                if (Physics.Raycast(selectedItem.transform.position, direction, out hit, maxRange))
                {
                    //Debug.Log("collided item: " + hit.collider.name);
                    if (selectedItem.name == hit.collider.name)
                    {
                        //Debug.Log("same object");
                        GameManager.Instance.giveScore = true;
                        Debug.Log("giving score");
                        GameManager.Instance.IncreaseStars();
                        GameManager.Instance.MatchedPairs(selectedItem, hit.collider.gameObject);
                        //Destroy(selectedItem);
                        //Destroy(hit.collider.gameObject);
                    }
                    Debug.Log("dropping the item");
                    
                }
                DropItem();
            }

        }
    }

    private Vector3 MousePosition()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
    }

    private void ResetDraggedItemValues()
    {
        selectedItemRB = null;
        selectedItem = null;
        draggedItemSortingLayer = null;
    }

    private void SetSortingLayer(string name)
    {
        draggedItemSortingLayer.sortingLayerName = name;
    }

    private void SetRBForDragging()
    {
        //selectedItemRB.isKinematic = true;
        selectedItemRB.useGravity = false;
    }

    private void SetRBForDropping()
    {
        selectedItemRB.isKinematic = false;
        selectedItemRB.useGravity = true;
    }

    public void DropItem()
    {
        if (selectedItem != null)
        {
            SetSortingLayer("Item");
            SetRBForDropping();
            //CheckCollison();
            //Debug.Log("dragged item: " + selectedItem);
            ResetDraggedItemValues();
        }
        
    }
}