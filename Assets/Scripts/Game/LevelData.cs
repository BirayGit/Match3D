using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    [SerializeField] private int levelTime = 60;
    [SerializeField] private int maxItems = 25;
    [SerializeField] private bool hasBomb = false;
    public SpriteTypeSO[] spriteType;
    
    public int LevelTime { get { return levelTime; } }
    public int MaxItems { get { return maxItems; } }
    public bool HasBomb { get { return hasBomb; } }

}
