using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public Transform player;

    private void FixedUpdate()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
        {
            player = obj.GetComponent<Player>().transform;
            
        }
        transform.position = Vector2.Lerp(transform.position, player.position, 0.1f);
        
    }
    
}
