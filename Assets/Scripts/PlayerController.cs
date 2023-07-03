using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 [RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    public Player player;
    public Animator playerAnimator;
    public GameManager manager;
    float input_x = 0;
    float input_y = 0;
    bool isWalking = false;
    public Toolbar_UI toolbarUI;
 
    // Start is called before the first frame update
    void Start()
    {
        isWalking = false;   
        player = GetComponent<Player>();
    }
 
    // Update is called once per frame
    void Update()
    {
        input_x = Input.GetAxisRaw("Horizontal");
        input_y = Input.GetAxisRaw("Vertical");
        isWalking = (input_x != 0 || input_y != 0);
 
        if (isWalking)
        {
            var move = new Vector3(input_x, input_y, 0).normalized;
            transform.position += move * player.entity.speed * Time.deltaTime;
            playerAnimator.SetFloat("input_x", input_x);
            playerAnimator.SetFloat("input_y", input_y);
        }
 
        playerAnimator.SetBool("isWalking", isWalking);
        if (player.entity.attackTimer < 0){
            player.entity.attackTimer = 0;
        }
        else{
            player.entity.attackTimer -= Time.deltaTime;
        }
        if(player.entity.attackTimer == 0 && !isWalking){
            if (Input.GetButtonDown("Fire1")){
                player.entity.attackTimer = player.entity.cooldown;
                Attack();
                playerAnimator.SetTrigger("attack");
            }
        }
        
    

        int selectedIndex = toolbarUI.GetIndex();
        string itemName = toolbarUI.GetItem();
        if(selectedIndex == 0)
        {
            if(itemName == "Ax")
            {
                playerAnimator.SetBool("WithAx", true);
            }
            else
            {
                playerAnimator.SetBool("WithAx", false);
            }
        }
    }

    void Attack(){
        if(player.entity.target == null){
            return;
        }
        MonsterController monster = player.entity.target.GetComponent<MonsterController>();
        if(monster.entity.dead){
            player.entity.target = null;
            return;
        }

        float distance = Vector2.Distance(transform.position, player.entity.target.transform.position);

        if(distance <= player.entity.attackDistance){

            int dmg = manager.CalculateDamage(player.entity, player.entity.damage);
            int targetDef = manager.CalculateDefence(monster.entity, monster.entity.defense);
            int dmgResult = dmg - targetDef;

            if (dmgResult < 0)
            {
                dmgResult = 0;
            }

            Debug.LogFormat("Player atacou, dmg: {0}", dmgResult);
            monster.entity.currentHealth -= dmgResult;
            monster.entity.target = this.gameObject;
        
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.transform.tag == "Enemy"){
            player.entity.target = other.transform.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.transform.tag == "Enemy"){
            player.entity.target = null;
        }
    }
}