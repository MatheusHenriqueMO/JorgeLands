using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public Sprite profile;
    public string speechText;
    public string actorName;
    public string playerTag;
    public float radious;
    private DialogueControl dc;

    private void Start()
    {
        dc = FindObjectOfType<DialogueControl>();
    }

    private void FixedUpdate()
    {
        Interact();
    }

    public void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radious);
        
        if (hit.CompareTag(playerTag))
            {
                dc.Speech(profile, speechText, actorName);
            }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radious);
    }
}
