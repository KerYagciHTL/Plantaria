using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Plant
{
    public string name; 
    public int rating = -1;
    public int attack;
    public int durability;
    public int speed;
    public int defense;
    public Rarity rarity;
    public Type type;
    public float probability;
    
    public void SetUp()
    {
        rating = Mathf.RoundToInt((attack + durability + speed + defense) / 4f);
        probability = GetProbabilityFromRarity();
    }

    private float GetProbabilityFromRarity()
    {
        switch (rarity)
        {
            case Rarity.Common:
                return 73.95f;
            case Rarity.Uncommon:
                return 20f;
            case Rarity.Rare:
                return 5f;
            case Rarity.Epic:
                return 1f;
            case Rarity.Legendary:
                return 0.05f;
            default:
                return 0f;
        }
    }
}