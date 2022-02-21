using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabSettings : MonoBehaviour
{
    private static PrefabSettings instance;
    public Movement movement;
    public SpriteTypeSO spriteType;

    public GameObject itemPrefab;
    public GameObject matchedItemImagePrefab;
    //public GameObject itemParent;
    public GameObject itemSpawnPoint1;
    public GameObject itemSpawnPoint2;
    public Transform grid;

    public MeshRenderer itemMeshRenderer;

    GameObject instantiatedItem;
    GameObject itemImageObject;
    SpriteRenderer itemSpriteRenderer;
    Image itemImage;

    public static PrefabSettings Instance { get { return instance; } }
    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            Debug.LogError("Multiple instances of UIManager, destroying the duplicate");
        }
        else
        {
            instance = this;
        }

    }

    private void Start()
    {
        //itemParent = GameObject.FindGameObjectWithTag("Items");
        //spriteType = LevelManager.Instance.GetRandomSpriteType();
    }

    public void InstantiatePrefab()
    {
        instantiatedItem = Instantiate(itemPrefab, itemSpawnPoint1.transform);
        //material
        itemSpriteRenderer = instantiatedItem.GetComponentInChildren<SpriteRenderer>();
        itemSpriteRenderer.sprite = spriteType.GetRandomSprite();
        instantiatedItem.name = itemSpriteRenderer.sprite.ToString();
        itemMeshRenderer = instantiatedItem.GetComponent<MeshRenderer>();
        //itemMeshRenderer.

        LevelManager.Instance.AddToSpritesList(itemSpriteRenderer.sprite);

        LevelManager.Instance.AddToItemsList(instantiatedItem);
    }

    public void InstantiateMatchedItemImagePrefab(Sprite sprite)
    {
        instantiatedItem = Instantiate(matchedItemImagePrefab, grid);
        itemImageObject = instantiatedItem.transform.GetChild(1).gameObject;
        itemImage = itemImageObject.GetComponentInChildren<Image>();
        Debug.Log(sprite);
        itemImage.sprite = sprite;
    }

    public void InstantiatePrefabFromList(int i)
    {
        instantiatedItem = Instantiate(itemPrefab, itemSpawnPoint2.transform);
        itemSpriteRenderer = instantiatedItem.GetComponentInChildren<SpriteRenderer>();
        itemSpriteRenderer.sprite = LevelManager.Instance.usedSpritesList[i];
        instantiatedItem.name = itemSpriteRenderer.sprite.ToString();
        LevelManager.Instance.AddToItemsList(instantiatedItem);

        if (i == (LevelManager.Instance.currentLevelData.MaxItems - 1))
        {
            
            if (movement != null)
            {
                StartCoroutine(movement.MoveCards());
            }
            StartCoroutine(LevelManager.Instance.StartLevelTimer());
        }
    }

    

}
