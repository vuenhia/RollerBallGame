using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryItem : MonoBehaviour, IComparable<InventoryItem>
{

    [SerializeField]
    private string itemName = "";

    [SerializeField]
    private int quantity = 0;

    public InventoryItem(string newName, int qty)
    {
        itemName = newName;
        quantity = qty;
    }

    public int CompareTo(InventoryItem otherItem)
    {
        if (otherItem == null) return 1;
        return itemName.CompareTo(otherItem.itemName);
    }

    public void deductQuantity(int amt)
    {

    }

    public void commitQuantity(int amt)
    {
 
    }

    public int getQuantity()
    {
        return quantity;
    }

    public string getItemName()
    {
        return itemName;
    }
}
