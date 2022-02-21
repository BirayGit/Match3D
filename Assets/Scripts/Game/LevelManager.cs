using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public LevelData[] LevelData;
    public LevelData currentLevelData;
    [SerializeField]private int currentLevel;
    private float levelTime;
    private float delayTime = 20f;
    private float timerDelayTime;

    private GameObject SpawnPoints;

    //[SerializeField] private TextMeshProUGUI levelTimerText;
    public List<Sprite> usedSpritesList;
    public List<GameObject> generatedItems;
    public float timerCountDown;
    public bool isCountingDown;
    public bool isGameOver;
    public bool levelStarted;
    [SerializeField] public Transform[] itemPositions;

    public static LevelManager Instance { get { return instance; } }
    public int CurrentLevel { get { return currentLevel; } set { value = currentLevel; } }

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
        currentLevel = 1;
        SpawnPoints = GameObject.FindGameObjectWithTag("SpawnPoints");
    }

    private void Update()
    {
        CountDown();
        //SetKinematic();
    }

    public SpriteTypeSO GetRandomSpriteType()
    {
        Debug.Log(currentLevelData.spriteType[Random.Range(0, currentLevelData.spriteType.Length)]);
        return currentLevelData.spriteType[Random.Range(0, currentLevelData.spriteType.Length)];
    }

    //public IEnumerator GeneratePrefabs()
    //{
    //    for (int i = 0; i < currentLevelData.MaxItems; i++)
    //    {
    //        PrefabSettings.Instance.InstantiatePrefab();
    //        yield return new WaitForSeconds(delayTime * Time.deltaTime);
    //    }
    //}
    //public IEnumerator GenerateDublicatePrefabs()
    //{
    //    for (int i = 0; i < usedSpritesList.Count; i++)
    //    {
    //        PrefabSettings.Instance.InstantiatePrefabFromList(i);
    //        yield return new WaitForSeconds(delayTime * Time.deltaTime);
    //    }
    //}

    public void GeneratePrefabs()
    {
        for (int i = 0; i < currentLevelData.MaxItems; i++)
        {
            PrefabSettings.Instance.InstantiatePrefab();
            //yield return new WaitForSeconds(delayTime * Time.deltaTime);
        }
    }
    public void GenerateDublicatePrefabs()
    {
        for (int i = 0; i < usedSpritesList.Count; i++)
        {
            PrefabSettings.Instance.InstantiatePrefabFromList(i);
            //yield return new WaitForSeconds(delayTime * Time.deltaTime);
        }
    }

    public IEnumerator GiveTime(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public void DeletePrefabsAndLists()
    {
        DeleteInstantiatedPrefabs();
        DeleteGeneratedPrefabsList();
        ResetUsedSpritesList();
        GameManager.Instance.ClearMatchedItemsList();
    }

    private void DeleteInstantiatedPrefabs()
    {
        for (int i = 0; i < SpawnPoints.transform.childCount; i++)
        {
            GameObject child = SpawnPoints.transform.GetChild(i).gameObject;
            for (int j = 0; j < child.transform.childCount; j++)
            {
                Destroy(child.transform.GetChild(j).gameObject);
            }
        }
    }
    private void DeleteGeneratedPrefabsList()
    {
        generatedItems.Clear();
    }

    public void SetSpriteType()
    {
        PrefabSettings.Instance.spriteType = GetRandomSpriteType();
    }
    public void AddToSpritesList(Sprite sprite)
    {
        usedSpritesList.Add(sprite);
    }
    public void AddToItemsList(GameObject gobj)
    {
        generatedItems.Add(gobj);
    }
    private void ResetUsedSpritesList()
    {
        usedSpritesList.Clear();
    }
    public IEnumerator StartLevelTimer()
    {
        timerDelayTime = (delayTime * (usedSpritesList.Count + 1)) * Time.deltaTime;
        yield return new WaitForSeconds(timerDelayTime);
        isCountingDown = true;
        levelStarted = true;
    }
    public void SetCurrentTime(float time)
    {
        levelTime = time;
        UIManager.Instance.LevelTimerText.text = levelTime.ToString();
    }
    public void SetTimerCountDown()
    {
        timerCountDown = levelTime;
    }
    private void DisplayLevelTime(float timer)
    {
        //timer += 1;
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);
        if (timer < 60)
        {
            int time = (int)timer;
            UIManager.Instance.LevelTimerText.text = time.ToString();
        }
        else
        {
            UIManager.Instance.LevelTimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
    public void CountDown()
    {
        if (isCountingDown)
        {
            if (timerCountDown > 0)
            {
                timerCountDown -= Time.deltaTime;
                DisplayLevelTime(timerCountDown);
                //Debug.Log(timerCountDown);
            }
            else
            {
                //Debug.Log("Time is up");
                timerCountDown = 0;
                isCountingDown = false;
                isGameOver = true;

                GameManager.Instance.giveScore = false;
                UIManager.Instance.OnGameOver();

            }
        }
    }

    public void SetBoolsFalse()
    {
        isCountingDown = false;
        levelStarted = false;
        GameManager.Instance.giveScore = false;
        isGameOver = false;
    }

    public void ContinueLevel()
    {
        SetBoolsFalse();
        SetCurrentTime(levelTime);
        SetTimerCountDown();
        StartCoroutine(StartLevelTimer());
        DisplayLevelTime(timerCountDown);
    }
    private void SetCurrentLevel()
    {
        switch (currentLevel)
        {
            case 1:
                currentLevelData = LevelData[0];
                break;
            case 2:
                currentLevelData = LevelData[1];
                break;
            case 3:
                currentLevelData = LevelData[2];
                break;
            case 4:
                currentLevelData = LevelData[3];
                break;
            case 5:
                currentLevelData = LevelData[4];
                break;
            case 6:
                currentLevelData = LevelData[5];
                break;
            case 7:
                currentLevelData = LevelData[6];
                break;
            default:
                currentLevelData = LevelData[7];
                break;
        }
    }
    public void StartLevel()
    {
        SetCurrentLevel();
        SetCurrentTime(currentLevelData.LevelTime);
        SetSpriteType();
        GameManager.Instance.ResetScore();
        GameManager.Instance.SetUpScore();
        SetTimerCountDown();
        SetBoolsFalse();
        SetSpriteType();
        //StartCoroutine(GeneratePrefabs());
        //StartCoroutine(GenerateDublicatePrefabs());
        GeneratePrefabs();
        GenerateDublicatePrefabs();
        StartCoroutine(StartLevelTimer());
        DisplayLevelTime(timerCountDown);
    }
    public void RestartLevel()
    {
        DeletePrefabsAndLists();
        GameManager.Instance.ResetLevel();
        StartLevel();
    }

    public void LoadNextLevel()
    {
        DeletePrefabsAndLists();
        GameManager.Instance.ResetLevel();
        StartLevel();
    }
    public void GameOver()
    {
        levelStarted = false;
        TouchManager touchManager = FindObjectOfType<TouchManager>();
        touchManager.DropItem();
    }

    public IEnumerator LevelVictory()
    {
        levelStarted = false; 
        IncreaseLevel();
        yield return new WaitForSeconds(14f* Time.deltaTime);
        GameManager.Instance.LevelVictory();
        UIManager.Instance.OnLevelVictory();
    }

    public void IncreaseLevel()
    {
        currentLevel++;
    }
    public void VideoBonus()
    {
        SetCurrentTime(30);
    }
}
