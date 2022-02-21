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
            Debug.Log("Item sprite is already generated. Getting a new sprite");
            Debug.Log("Sprite is: " + (i + 1));
            i = GenerateDifferentRandomNumber(i);
            Debug.Log("Sprite after random generation is: " + (i + 1));
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
        Debug.Log("i is: " + i);
        int j = Random.Range(0, itemSprites.Length);
        Debug.Log("j is: " + j);
        while ((j == i) || (LevelManager.Instance.usedSpritesList.Contains(itemSprites[j])))
        {
            Debug.Log("j inside while is: " + j);
            Debug.Log((LevelManager.Instance.usedSpritesList.Contains(itemSprites[j])));
            j = Random.Range(0, itemSprites.Length);
            //Debug.Log("j in while loop is: " + j);
        }
        return j;
    }
}
