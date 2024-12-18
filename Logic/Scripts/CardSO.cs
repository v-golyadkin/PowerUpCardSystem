using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card", fileName = "New Card")]
public class CardSO : ScriptableObject
{
    public Sprite cardImage; // card image
    public string cardText; // card text
    public CardEffect effectType; // effect
    public float effectValue; // effect value
    public bool isUnique; // if is unique, can be only one
    public float unlockLevel = 1; // level requirement for unlocking

}

public enum CardEffect
{
    DamageIncrease,
    AttackSpeedIncrease,
    HealthIncrease, 
    EvasionIncrease,
    ArmourIncrease
}
