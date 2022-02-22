using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]float currentStars;
    [SerializeField]float totalStars;
    [SerializeField]int currentMatchedPairs;
    int alignCount;
    int hintCount;
    int reshuffleCount;
    public bool giveScore, isWon;
    float time = 12f;
    GameObject matchedItems;

    [SerializeField]private Rewards rewards;

    [SerializeField]List<GameObject> matchedItemsList;

    public static GameManager Instance { get { return instance; } }

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

    // Start is called before the first frame update
    void Start()
    {
        giveScore = false;
        isWon = false;
        FreshGame();
        matchedItems = GameObject.FindGameObjectWithTag("MatchedItems");
    }

    public void FreshGame()
    {
        LevelManager.Instance.CurrentLevel = 1;
        totalStars = 0;
        currentStars = 0;
        alignCount = 3;
        hintCount = 3;
        reshuffleCount = 3;
    }

    public void MatchedPairs(GameObject gObj1, GameObject gObj2)
    {
        Vector3 scale = new Vector3(0.53f, 0.016f, 0.27f);
        Vector3 resetYRotation = new Vector3(0, 0, 0);
        Vector3 spawn2Position = new Vector3(0.5f, 0, 3.4f);
        Vector3 spawn1Position = new Vector3(2.82f, 0, 3.4f);

        Vector3 matchedItemLocationRight = new Vector3(1.65f, 0.5f, 1.6f);

        gObj1.transform.parent = matchedItems.transform;
        LeanTween.moveLocal(gObj1, matchedItemLocationRight, time * Time.deltaTime);
        AddMatchedItemsToList(gObj1);

        gObj1.transform.LeanScale(scale, time * Time.deltaTime);
        GameObject child = gObj1.transform.GetChild(0).gameObject;
        GameObject itemImage = child.transform.GetChild(0).gameObject;

        Sprite itemImageSprite = itemImage.GetComponent<SpriteRenderer>().sprite;
        PrefabSettings.Instance.InstantiateMatchedItemImagePrefab(itemImageSprite);

        LeanTween.rotateY(child, 0, 0.2f * Time.deltaTime);
        LeanTween.moveLocalX(child, -1.70f, 0.2f * Time.deltaTime);
        Destroy(gObj1);
        Destroy(gObj2);
        currentMatchedPairs++;
        CheckVictory();
    }
    public void AddMatchedItemsToList(GameObject gObj)
    {
        matchedItemsList.Add(gObj);
    }

    public void ClearMatchedItemsList()
    {
        matchedItemsList.Clear();
    }

    public void IncreaseStars()
    {
        if (giveScore)
        {
            currentStars += 1;
            UIManager.Instance.CurrentStarsText.text = currentStars.ToString();
            giveScore = false;
        }
    }

    public void SetUpScore()
    {
        currentStars = 0;
    }

    public void ResetLevel()
    {
        ResetScore();
        ResetCurrentMatchedPairs();
        ResetCurrentMatchedPairsImage();
        ClearMatchedItemsList();
    }  

    public void ResetCurrentStars()
    {
        currentStars = 0;
    }
    public void ResetCurrentMatchedPairs()
    {
        currentMatchedPairs = 0;
    }
    public void ResetCurrentMatchedPairsImage()
    {
        Transform grid = PrefabSettings.Instance.grid;
        for (int i = 0; i < grid.childCount; i++)
        {
            Destroy(grid.GetChild(i).gameObject);
        }
    }
    public void ResetScore()
    {
        ResetCurrentStars();
        ResetCurrentMatchedPairs();
    }
   
    public void LevelVictory()
    {
        totalStars += currentStars;
        currentStars = 0;
    }

    public void CheckVictory()
    {
        int spriteCount = LevelManager.Instance.usedSpritesList.Count;
        Debug.Log("sprite count is: " + spriteCount);
        if (currentMatchedPairs == spriteCount)
        {
            Debug.Log("level is won");
            isWon = true;
            StartCoroutine(LevelManager.Instance.LevelVictory());
            
        }
    }
    public float CurrentStars { get { return currentStars; } set { currentStars = value; } }
    public int AlignCount { get { return alignCount; } set { alignCount = value; } }
    public int HintCount { get { return hintCount; } set { hintCount = value; } }
    public int ReshuffleCount { get { return reshuffleCount; } set { reshuffleCount = value; } }
}
