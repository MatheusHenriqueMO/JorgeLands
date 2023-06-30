using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class MonsterController : MonoBehaviour
{
    [Header("Controller")]
    public EntityController entity;
    public GameManager manager;

    [Header("Patrol")]
    public Transform[] waypointList;
    public float arrivalDistance = 0.5f;
    public float waitTime = 5f;

    //privadas
    Transform targetWaipoint;
    int currentWaypoint = 0;
    float lastDistanceToTarget = 0;
    float currentWaitTime = 0;

    [Header("Experence Reward")]
    public int rewardEXP = 10;
    public int lootGoldMin = 0;
    public int lootGoldMax = 5;

    [Header("Respawn")]
    public GameObject prefab;
    public bool respawn = true;
    public float respawnTime = 10f;

    Rigidbody2D rb;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        entity.maxHealth = manager.CalculateHealth(this.entity);
        entity.maxMana = manager.CalculateMana(this.entity);
        entity.maxStamina = manager.CalculateStamina(this.entity);

        entity.currentHealth = entity.maxHealth;
        entity.currentMana = entity.maxMana;
        entity.currentStamina = entity.maxStamina;

        currentWaitTime = waitTime;
        if (waypointList.Length > 0)
        {
            targetWaipoint = waypointList[currentWaypoint];
            lastDistanceToTarget = Vector2.Distance(transform.position, targetWaipoint.position);
        }
    }
}
