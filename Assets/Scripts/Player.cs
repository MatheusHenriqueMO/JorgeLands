using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory inventory;

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
}
