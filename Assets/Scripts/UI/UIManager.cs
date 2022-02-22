using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    private static UIManager instance;
    [SerializeField] private SpawnPoints spawnPoints;
    private GameObject mainMenuBackGround;
    private Image mainMenuBackGroundImage;

    private GameObject gameMenuBackGround;
    private Image gameMenuBackGroundImage;
    GameObject[] pauseButtons;

    [SerializeField] private TextMeshProUGUI curentLevelText, levelTimerText, currentStarsText, alignCountText, hintCountText, reshuffleCountText;

    public TextMeshProUGUI LevelTimerText { get { return levelTimerText; } set { value = levelTimerText; } }
    public TextMeshProUGUI CurrentStarsText { get { return currentStarsText; } set { value = currentStarsText; } }

    public static UIManager Instance { get { return instance; } }

    private GameManager gameManager;
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

        OnShowMainMenu();
    }

    public enum MenuState
    {
        MainMenu, Game, GameOver, Settings, Pause, Tutorial, LevelVictory
    }

    public MenuState currentState;
    public GameObject MainMenuPanel;
    public GameObject GamePanel;
    public GameObject GameOverPanel;
    public GameObject SettingsPanel;
    //public GameObject TutorialPanel;
    public GameObject PausePanel;
    public GameObject LevelVictoryPanel;

    private Canvas gameMenuCanvas;

    

    public void OnShowMainMenu()
    {
        currentState = MenuState.MainMenu;        

        MainMenuPanel.SetActive(true);
        GamePanel.SetActive(false);

        mainMenuBackGround = GameObject.FindGameObjectWithTag("Background-MainMenu");
        mainMenuBackGroundImage = mainMenuBackGround.GetComponent<Image>();

        SettingsPanel.SetActive(false);
        GamePanel.SetActive(false);
    }

    public void OnBackToMainMenu()
    {
        LevelManager.Instance.DeletePrefabsAndLists();
        GameManager.Instance.ResetLevel();
        OnExitPause();
        OnShowMainMenu();
    }

    public void OnPlay()
    {
        currentState = MenuState.Game;
        GamePanel.SetActive(true);

        gameMenuBackGround = GameObject.FindGameObjectWithTag("Background-GameMenu");
        gameMenuBackGroundImage = gameMenuBackGround.GetComponent<Image>();
        gameMenuCanvas = GamePanel.GetComponent<Canvas>();

        MainMenuPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        LevelVictoryPanel.SetActive(false);
        LevelManager.Instance.isGameOver = false;
        SetUpUI();
        LevelManager.Instance.StartLevel();
    }

    public void OnNextLevel()
    {
        currentState = MenuState.Game;
        GamePanel.SetActive(true);

        gameMenuBackGround = GameObject.FindGameObjectWithTag("Background-GameMenu");
        gameMenuBackGroundImage = gameMenuBackGround.GetComponent<Image>();
        gameMenuCanvas = GamePanel.GetComponent<Canvas>();

        MainMenuPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        LevelVictoryPanel.SetActive(false);
        GameManager.Instance.isWon = false;
        UnpauseThePanel();
        InteractableMenuButtons(true);
        SetUpUI();
        LevelManager.Instance.LoadNextLevel();
    }
    public void OnShowSettings()
    {
        currentState = MenuState.Settings;
        SettingsPanel.SetActive(true);

        PauseThePanelMainMenu();
    }

    public void OnExitSettings()
    {
        currentState = MenuState.MainMenu;
        MainMenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);

        UnpauseThePanelMainMenu();
    }

    public void OnGameOver()
    {
        if (LevelManager.Instance.isGameOver)
        {
            LevelManager.Instance.GameOver();
            
            currentState = MenuState.GameOver;
            GameOverPanel.SetActive(true);
            PausePanel.SetActive(false);
            LevelVictoryPanel.SetActive(false);
            PauseThePanel();
            InteractableMenuButtons(false);
        }
        
    }
    public void OnLevelVictory()
    {
        currentState = MenuState.LevelVictory;
        LevelVictoryPanel.SetActive(true);
        GameOverPanel.SetActive(false);
        PausePanel.SetActive(false);
        PauseThePanel();
        InteractableMenuButtons(false);
    }
    public void OnShowPause()
    {
        currentState = MenuState.Pause;
        PausePanel.SetActive(true);
        GameOverPanel.SetActive(false);
        LevelVictoryPanel.SetActive(false);
        PauseThePanel();
        InteractableMenuButtons(false);
    }

    public void OnExitPause()
    {
        currentState = MenuState.Game;
        GamePanel.SetActive(true);
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        UnpauseThePanel();
        InteractableMenuButtons(true);
    }

    public void OnContinue()
    {
        LevelManager.Instance.VideoBonus();
        LevelManager.Instance.ContinueLevel();
        OnExitPause();
    }

    public void OnRestart()
    {
        LevelManager.Instance.RestartLevel();
        OnExitPause();
        InteractableMenuButtons(true);
        SetUpUI();
        //OnPlay();
    }

    public void SetUpUI()
    {
        curentLevelText.text = LevelManager.Instance.CurrentLevel.ToString();
        currentStarsText.text = GameManager.Instance.CurrentStars.ToString();
        alignCountText.text = GameManager.Instance.AlignCount.ToString();
        hintCountText.text = GameManager.Instance.HintCount.ToString();
        reshuffleCountText.text = GameManager.Instance.ReshuffleCount.ToString();

    }
    public void PauseThePanel()
    {
        LevelManager.Instance.levelStarted = false;
        Time.timeScale = 0f;
        if (gameMenuCanvas != null)
        {
            gameMenuCanvas.sortingLayerName = "DraggedItem";
            gameMenuBackGroundImage.color = new Color(0, 0, 0, 0.6f);
        }
        
    }

    public void UnpauseThePanel()
    {
        LevelManager.Instance.levelStarted = true;
        Time.timeScale = 1f;
        if (gameMenuCanvas != null)
        {
            gameMenuCanvas.sortingLayerName = "UI";
            gameMenuBackGroundImage.color = new Color(1, 1, 1, 1);
        }
        
    }

    public void PauseThePanelMainMenu()
    {
        LevelManager.Instance.levelStarted = false;
        Time.timeScale = 0f;
        mainMenuBackGroundImage.color = new Color(0, 0, 0, 0.6f);
    }

    public void UnpauseThePanelMainMenu()
    {
        LevelManager.Instance.levelStarted = true;
        Time.timeScale = 1f;
        //gameMenuCanvas.sortingLayerName = "UI";
        mainMenuBackGroundImage.color = new Color(1, 1, 1, 1);
    }

    public void InteractableMenuButtons(bool selection)
    {
        pauseButtons = GameObject.FindGameObjectsWithTag("InteractableButtons");
        foreach (GameObject item in pauseButtons)
        {
            Button itemButtons = item.GetComponent<Button>();
            itemButtons.interactable = selection;

        }
    }
}
