using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 [RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    public Player player;
    public Animator playerAnimator;
    float input_x = 0;
    float input_y = 0;
    bool isWalking = false;
 
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
 
        if (Input.GetButtonDown("Fire1"))
            playerAnimator.SetTrigger("attack");
    }
}