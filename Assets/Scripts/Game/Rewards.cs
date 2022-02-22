using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rewards : MonoBehaviour
{

    //public GameObject rewardBox;
    private Image rewardBoxFill;
    private GameObject[] rewardBoxes;

    private Image rewardBoxFillImage;
    private GameObject rewardBoxChild;
    
    private bool isLevelWon;
    private float currentRewardAmount;
   

    private void Start()
    {
        currentRewardAmount = 0f;
        RewardBoxFill(rewardBoxFill, currentRewardAmount);
        isLevelWon = false;
        rewardBoxes = GameObject.FindGameObjectsWithTag("RewardBox");
        Debug.Log("reward boxes length during start is: " + rewardBoxes.Length);
    }

    public void RewardBoxFill(Image image, float amount)
    {
        if (image != null)
        {
            image.fillAmount = amount;
            Debug.Log("fill changed");
        }
        else
        {
            Debug.Log("image doesnt exist");
        }
    }

    public void GiveReward()
    {
        Debug.Log("reward given");
    }

    public void IncreaseRewardPercentage()
    {
        rewardBoxes = GameObject.FindGameObjectsWithTag("RewardBox");
        Debug.Log("reward boxes length inside increase reward percentage is: " + rewardBoxes.Length);
        foreach (GameObject reward in rewardBoxes)
        {
            Debug.Log("reward boxes length in increse reward percantage is: " + rewardBoxes);
            currentRewardAmount += 0.2f;
            //Debug.Log("reward box is: " + rewards);
            //rewardBoxChild = rewards.
            //rewardBoxFill = rewards.GetComponentInChildren<Image>();
            //Debug.Log("reward box fill + " + rewardBoxFill);
            //RewardBoxFill(rewardBoxFill, currentRewardAmount);
            Transform parent = reward.transform;
            Transform child = parent.GetChild(1);
            rewardBoxChild = child.gameObject;
            rewardBoxFillImage = rewardBoxChild.GetComponent<Image>();
            RewardBoxFill(rewardBoxFillImage, currentRewardAmount);

        }
    }

    public void CheckRewardBoxFill()
    {
        if (currentRewardAmount == 1f)
        {
            GiveReward();
            currentRewardAmount = 0f;
        }
    }
    public void LevelWon()
    {
        Debug.Log("inside level won");
        IncreaseRewardPercentage();
        CheckRewardBoxFill();
    }

    public void IsLevelWon()
    {
        Debug.Log("level won is called");
        isLevelWon = true;
        if (isLevelWon)
        {
            Debug.Log("inside if");
            LevelWon();
            isLevelWon = false;
        }
    }
}
