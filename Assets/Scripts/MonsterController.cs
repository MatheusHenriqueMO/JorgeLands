using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class MonsterController : MonoBehaviour
{
    [Header("Controller")]
    public EntityController entity;
    public GameManager manager;

    [Header("Patrol")]
    public List<Transform> waypointList;
    public float arrivalDistance = 0.5f;
    public float waitTime = 5f;
    public int waypointID;

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

    [Header("UI")]
    public Slider healthSlider;

    Rigidbody2D rb2D;
    Animator animator;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        entity.maxHealth = manager.CalculateHealth(this.entity);
        entity.maxMana = manager.CalculateMana(this.entity);
        entity.maxStamina = manager.CalculateStamina(this.entity);

        entity.currentHealth = entity.maxHealth;
        entity.currentMana = entity.maxMana;
        entity.currentStamina = entity.maxStamina;

        healthSlider.value = entity.currentHealth;
        healthSlider.maxValue = entity.maxHealth;

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("waypoint"))
        {
            int id = obj.GetComponent<WaypointID>().ID;
            if(id == waypointID){
                waypointList.Add(obj.transform);
            }
        }

        currentWaitTime = waitTime;
        if (waypointList.Count > 0)
        {
            targetWaipoint = waypointList[currentWaypoint];
            lastDistanceToTarget = Vector2.Distance(transform.position, targetWaipoint.position);
        }
    }

    void Update()
    {
        if (entity.dead)
        {
            return;
        }
        healthSlider.value = entity.currentHealth;
        if (entity.currentHealth <= 0)
        {
            entity.currentHealth = 0;
            Die();
        }
        if (!entity.inCombat)
        {
            if (waypointList.Count > 0)
            {
                Patrol();
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
        else
        {
            if (entity.attackTimer > 0)
            {
                entity.attackTimer -= Time.deltaTime;
            }
            if (entity.attackTimer < 0)
            {
                entity.attackTimer = 0;
            }
            if (entity.target != null && entity.inCombat)
            {
                if (!entity.combatCoroutine)
                {
                    StartCoroutine(Attack());
                }
            }
            else
            {
                entity.combatCoroutine = false;
                StopCoroutine(Attack());
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && !entity.dead)
        {
            entity.inCombat = true;
            entity.target = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            entity.inCombat = false;
            entity.target = null;
        }
    }

    void Patrol()
    {
        if (entity.dead)
        {
            return;
        }

        float distanceToTarget = Vector2.Distance(transform.position, targetWaipoint.position);
        if (distanceToTarget <= arrivalDistance || distanceToTarget >= lastDistanceToTarget)
        {
            animator.SetBool("isWalking", false);

            if (currentWaitTime <= 0)
            {
                currentWaypoint++;

                if (currentWaypoint >= waypointList.Count)
                {
                    currentWaypoint = 0;
                }

                targetWaipoint = waypointList[currentWaypoint];
                lastDistanceToTarget = Vector2.Distance(transform.position, targetWaipoint.position);

                currentWaitTime = waitTime;
            }
            else
            {
                currentWaitTime -= Time.deltaTime;
            }
        }
        else
        {
            animator.SetBool("isWalking", true);
            lastDistanceToTarget = distanceToTarget;
        }

        Vector2 direction = (targetWaipoint.position - transform.position).normalized;
        animator.SetFloat("input_x", direction.x);
        animator.SetFloat("input_y", direction.y);

        rb2D.MovePosition(rb2D.position + direction * (entity.speed * Time.fixedDeltaTime));

    }

    IEnumerator Attack()
    {
        entity.combatCoroutine = true;
        while (true)
        {
            yield return new WaitForSeconds(entity.cooldown);

            if (entity.target != null && !entity.target.GetComponent<Player>().entity.dead)
            {
                animator.SetBool("attack", true);
                float distance = Vector2.Distance(entity.target.transform.position, transform.position);

                if (distance <= entity.attackDistance)
                {
                    int dmg = manager.CalculateDamage(entity, entity.damage);
                    int targetDef = manager.CalculateDefence(entity.target.GetComponent<Player>().entity, entity.target.GetComponent<Player>().entity.defense);
                    int dmgResult = dmg - targetDef;

                    if (dmgResult < 0)
                    {
                        dmgResult = 0;
                    }

                    Debug.LogFormat("Inimigo atacou o Player, dmg: {0}", dmgResult);
                    entity.target.GetComponent<Player>().entity.currentHealth -= dmgResult;
                }
            }
        }
    }

    void Die()
    {
        entity.dead = true;
        entity.inCombat = false;
        entity.target = null;

        animator.SetBool("isWalking", false);

        //Fazer função de exp
        //manager.GainEXP(rewardEXP);

        Debug.LogFormat("Inimigo morreu!: {0}", entity.name);
        StopAllCoroutines();
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        GameObject newMonster = Instantiate(prefab, transform.position, transform.rotation, null);
        newMonster.name = prefab.name;
        newMonster.GetComponent<MonsterController>().entity.dead = false;
        newMonster.GetComponent<MonsterController>().entity.combatCoroutine = false;

        Destroy(this.gameObject);
    }
}
