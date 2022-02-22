using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteType", menuName = "SpriteType", order = 1)]
public class SpriteTypeSO : ScriptableObject
{

    public Sprite[] itemSprites;
    
    public Sprite GetRandomSprite()
    {
        int i = Random.Range(0, itemSprites.Length);
        if (LevelManager.Instance.usedSpritesList.Contains(itemSprites[i]))
        {
            i = GenerateDifferentRandomNumber(i);
            return itemSprites[i];
        }
        else
        {
            return itemSprites[i];
        }

    }
    //test if generated number is equal to i again
    public int GenerateDifferentRandomNumber(int i)
    {
        int j = Random.Range(0, itemSprites.Length);
        while ((j == i) || (LevelManager.Instance.usedSpritesList.Contains(itemSprites[j])))
        {
            j = Random.Range(0, itemSprites.Length);
            //Debug.Log("j in while loop is: " + j);
        }
        return j;
    }
}
