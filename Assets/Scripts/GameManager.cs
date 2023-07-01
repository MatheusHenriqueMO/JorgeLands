using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ItemManager itemManager;
    //public TileManager tileManager;
    public UI_Manager uiManager;

    public Player player;

    public Int32 CalculateHealth(EntityController entity)
    {
        // Formula: (resistence * 10) + (level * 4) + 10
        Int32 result = (entity.resistence * 10) + (entity.level * 4) + 10;
        Debug.LogFormat("CalculateHealth: {0}", result);
        return result;
    }

    public Int32 CalculateMana(EntityController entity)
    {
        // Formula: (intelligence * 10) + (level * 4) + 5
        Int32 result = (entity.intelligence * 10) + (entity.level * 4) + 5;
        Debug.LogFormat("CalculateMana: {0}", result);
        return result;
    }

    public Int32 CalculateStamina(EntityController entity)
    {
        // Formula: (resistence * willpower) + (level * 4) + 4
        Int32 result = (entity.resistence + entity.willPower) + (entity.level * 2) + 5;
        Debug.LogFormat("CalculateStamina: {0}", result);
        return result;
    }

    public Int32 CalculateDamage(EntityController entity, int weaponDamage)
    {
        // Formula: (strength * 2) + (weaponDamage) + (level * 3) + Random(1-20)
        System.Random rnd = new System.Random();
        Int32 result = (entity.strength * 2) + weaponDamage + (entity.level * 3) + rnd.Next(1, 20);
        Debug.LogFormat("CalculateDamage: {0}", result);
        return result;
    }

    public Int32 CalculateDefence(EntityController entity, int armorDefence)
    {
        // Formula: (resistence * 2) + (level * 3) + armorDefence
        System.Random rnd = new System.Random();
        Int32 result = (entity.resistence * 2) + (entity.level * 3) + armorDefence;
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
        uiManager = GetComponent<UI_Manager>();

        player = FindAnyObjectByType<Player>();
    }
}
