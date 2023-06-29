using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public EntityController entity;
    [Header("Player UI")]
    public Slider health;
    public Slider mana;
    public Slider stamina;
    public Slider exp;
    public Inventory inventory;

    void Start()
    {
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
    }

    void Update()
    {
        health.value = entity.currentHealth;
        mana.value = entity.currentMana;
        stamina.value = entity.currentStamina;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            entity.currentHealth -= 1;
        }
    }

    private void Awake()
    {
        inventory = new Inventory(12);
    }
    public void DropItem(Collectable item)
    {
        Vector2 spawnLocation = transform.position;

        Vector2 spawnOffset = Random.insideUnitCircle * 0.6f;

        Collectable droppeditem = Instantiate(item, spawnLocation + spawnOffset, Quaternion.identity);

        droppeditem.rb2d.AddForce(spawnOffset * 0.5f, ForceMode2D.Impulse);
    }

        public void DropItem(Collectable item, int numToDrop)
    {
        for(int i = 0; i < numToDrop; i++)
        {
            DropItem(item);
        }
    }
}
