using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ItemManager itemManager;

    public Int32 CalculateHealth(Player player)
    {
        // Formula: (resistence * 10) + (level * 4) + 10
        Int32 result = (player.entity.resistence * 10) + (player.entity.level * 4) + 10;
        Debug.LogFormat("CalculateHealth: {0}", result);
        return result;
    }

    public Int32 CalculateMana(Player player)
    {
        // Formula: (intelligence * 10) + (level * 4) + 5
        Int32 result = (player.entity.intelligence * 10) + (player.entity.level * 4) + 5;
        Debug.LogFormat("CalculateMana: {0}", result);
        return result;
    }

    public Int32 CalculateStamina(Player player)
    {
        // Formula: (resistence * willpower) + (level * 4) + 4
        Int32 result = (player.entity.resistence + player.entity.willPower) + (player.entity.level * 2) + 5;
        Debug.LogFormat("CalculateStamina: {0}", result);
        return result;
    }

    public Int32 CalculateDamage(Player player, int weaponDamage)
    {
        // Formula: (strength * 2) + (weaponDamage) + (level * 3) + Random(1-20)
        System.Random rnd = new System.Random();
        Int32 result = (player.entity.strength * 2) + weaponDamage + (player.entity.level * 3) + rnd.Next(1, 20);
        Debug.LogFormat("CalculateDamage: {0}", result);
        return result;
    }

    public Int32 CalculateDefence(Player player, int armorDefence)
    {
        // Formula: (resistence * 2) + (level * 3) + armorDefence
        System.Random rnd = new System.Random();
        Int32 result = (player.entity.resistence * 2) + (player.entity.level * 3) + armorDefence;
        Debug.LogFormat("CalculateDefence: {0}", result);
        return result;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

        itemManager = GetComponent<ItemManager>();
    }
}
