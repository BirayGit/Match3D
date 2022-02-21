using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    //public GameObject itemPrefab;
    public float randomMovementX, randomMovementXSP1, randomMovementXSP2;
    public float randomMovementZ, randomRotationY;
    private float delay = 10f;
    [SerializeField]private float randomY = 10f;
    private int itemPositionListCount;


    // Start is called before the first frame update
    void Start()
    {
        randomMovementXSP1 = Random.Range(-0.4f, 2.8f);
        randomMovementXSP2 = Random.Range(-2.8f, 0.4f);
    }

    public IEnumerator MoveCards()
    {
        itemPositionListCount = LevelManager.Instance.itemPositions.Length;
        Debug.Log("test");
        for (int i = 0; i < LevelManager.Instance.generatedItems.Count; i++)
        {
            Debug.Log("test");
            int j = i % itemPositionListCount;
            int k = i / itemPositionListCount;
            int random = Random.Range(0, itemPositionListCount);

            GameObject currentItem = LevelManager.Instance.generatedItems[i];
            if (currentItem != null)
            {
                currentItem.transform.parent = LevelManager.Instance.itemPositions[j];

                randomMovementX = Random.Range(-0.3f, 0.3f);
                randomMovementZ = Random.Range(-0.4f, 0.4f);
                randomRotationY = Random.Range(-20, 20);
                //currentItem.transform.parent = LevelManager.Instance.itemPositions[random];
                //Debug.Log("i is: " + i + " j is: " + j);
                GameObject child = currentItem.transform.GetChild(0).gameObject;
                
                //LeanTween.moveLocal(currentItem, new Vector3(randomMovementX, 0 + (0.2f * k), randomMovementZ), delay * Time.deltaTime);
                LeanTween.moveLocal(currentItem, new Vector3(randomMovementX, 0.7f, randomMovementZ), delay * Time.deltaTime);

                LeanTween.rotateY(child, randomRotationY, 0.2f * Time.deltaTime);
                yield return new WaitForSeconds(delay * Time.deltaTime);
            }
            
        }
    }
}
