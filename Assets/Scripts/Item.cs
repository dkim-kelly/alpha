using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int price;
    public string itemDescription;
    public string itemFlavorText;
    public bool isWatch;
    public bool isHeadlamp;
    public int moveSpeed;
    public int refillSpeed;
    public int sprayIncrease;
    public int refillRange;
    public int timePenalties;
    public int rangeIncrease;
    public int timeIncrease;
    public int incomeIncrease;
    
}