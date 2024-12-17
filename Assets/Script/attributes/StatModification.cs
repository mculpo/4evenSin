using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModification : MonoBehaviour
{
    public PlayerController player;
    public AtributeData attributes;

    public void PermHealthModifier(int increase)
    {
        player.UpdateBaseHealth(increase);
    }

    public void PermDefenseModifier(float increase)
    {
        player.UpdateBaseDefense(increase);
    }

    public void PermStrengthModifier(float increase)
    {
        player.UpdateBaseStrength(increase);
    }

    IEnumerator TempHealthModifier(float seconds, int increase)
    {
        player.UpdateHealth(increase);

        yield return new WaitForSeconds(seconds);

        //If the the health is greater then the base health reset it to the base health
        if(player.health > player.baseHealth)
        {
            player.health = player.baseHealth;
        }
    }

    IEnumerator TempDefenseModifier(float seconds, float increase)
    {
        player.UpdateDefense(increase);

        yield return new WaitForSeconds(seconds);
        player.defense = player.baseDefense;
    }

    IEnumerator TempStrengthModifier(float seconds, float increase)
    {
        player.UpdateStrength(increase);

        yield return new WaitForSeconds(seconds);
        player.strength = player.baseStrength;
    }
}
