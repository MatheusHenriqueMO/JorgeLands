using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbar_UI : MonoBehaviour
{
    [SerializeField] private List<Slot_UI> toolbarSlots = new List<Slot_UI>();

    private Slot_UI selectSlot;

    private void Start()
    {
        SelectSlot(0);
    }

    private void Update()
    {
        CheckAlphaNumericKeys();
    }

    public void SelectSlot(int index)
    {
        if(toolbarSlots.Count == 6){
            if(selectSlot != null)
            {
                selectSlot.SetHighlight(false);
            }
            selectSlot = toolbarSlots[index];
            selectSlot.SetHighlight(true);
            Debug.Log("Selecione o Index: " + toolbarSlots.IndexOf(selectSlot));
        }
    }

    public int GetIndex()
    {
        return  toolbarSlots.IndexOf(selectSlot);
    }

     public string GetItem()
    {
        return selectSlot.inventory.slots[selectSlot.slotID].itemName;
    }
    private void CheckAlphaNumericKeys()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            SelectSlot(0);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            SelectSlot(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            SelectSlot(2);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)){
            SelectSlot(3);
        }
        if(Input.GetKeyDown(KeyCode.Alpha5)){
            SelectSlot(4);
        }
        if(Input.GetKeyDown(KeyCode.Alpha6)){
            SelectSlot(5);
        }
    }
}
