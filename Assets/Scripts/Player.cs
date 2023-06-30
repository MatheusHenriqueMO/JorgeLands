using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public EntityController entity;

    [Header("Player Regen System")]
    public bool regenHPEnable = true;
    public float regenHPTime = 5f;
    public int regenHPValue = 5;
    public bool regenMPEnable = true;
    public float regenMPTime = 10f;
    public int regenMPValue = 5;

    [Header("Game Manager")]
    public GameManager manager;

    [Header("Player UI")]
    public Slider health;
    public Slider mana;
    public Slider stamina;
    public Slider exp;
    public Inventory inventory;

    void Start()
    {
        if (manager == null)
        {
            Debug.LogError("É necessario anexar o game manager ao player");
            return;
        }

        entity.maxHealth = manager.CalculateHealth(this);
        entity.maxMana = manager.CalculateMana(this);
        entity.maxStamina = manager.CalculateStamina(this);

        Int32 dmg = manager.CalculateDamage(this, 10); // usado no player
        Int32 def = manager.CalculateDefence(this, 5); // usado no inimigo

        entity.currentHealth = entity.maxHealth;
        entity.currentMana = entity.maxMana;
        entity.currentStamina = entity.maxStamina;

        health.maxValue = entity.maxHealth;
        health.value = health.maxValue;

        mana.maxValue = entity.maxMana;
        mana.value = mana.maxValue;

        stamina.maxValue = entity.maxStamina;
        stamina.value = stamina.maxValue;

        exp.value = 0;

        //Start Regeneration
        StartCoroutine(RegenHealth());
        StartCoroutine(RegenMana());
    }

    void Update()
    {
        health.value = entity.currentHealth;
        mana.value = entity.currentMana;
        stamina.value = entity.currentStamina;

        //Tirando vida TESTE (RETIRAR DEPOIS)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            entity.currentHealth -= 10;
            entity.currentMana -= 5;
        }
    }

    private void Awake()
    {
        inventory = new Inventory(12);
    }
    public void DropItem(Collectable item)
    {
        Vector2 spawnLocation = transform.position;

        Vector2 spawnOffset = UnityEngine.Random.insideUnitCircle * 0.6f;

        Collectable droppeditem = Instantiate(item, spawnLocation + spawnOffset, Quaternion.identity);

        droppeditem.rb2d.AddForce(spawnOffset * 0.5f, ForceMode2D.Impulse);
    }

    public void DropItem(Collectable item, int numToDrop)
    {
        for (int i = 0; i < numToDrop; i++)
        {
            DropItem(item);
        }
    }

    IEnumerator RegenHealth(){
        while(true){
            if(regenHPEnable){
                if(entity.currentHealth < entity.maxHealth){
                    Debug.LogFormat("Regenerando HP...");
                    entity.currentHealth += regenHPValue;
                    yield return new WaitForSeconds(regenHPTime);
                }
                else{
                    yield return null;
                }
            }
            else{
                yield return null;
            }
            
        }
    }

    IEnumerator RegenMana(){
        while(true){
            if(regenMPEnable){
                if(entity.currentMana < entity.maxMana){
                    Debug.LogFormat("Regenerando MP...");
                    entity.currentMana += regenMPValue;
                    yield return new WaitForSeconds(regenMPTime);
                }
                else{
                    yield return null;
                }
            }
            else{
                yield return null;
            }
            
        }
    }
}
