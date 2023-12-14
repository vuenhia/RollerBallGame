using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBag : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void addToBag(InventoryItem newItem)
    {
        
    }

    public bool doesBagHave(string itemName, int minQtyOf)
    {
        return false;
    }

    public string[] getBagItems()
    {
        return null;
    }

    public int qtyInBag(string itemName)
    {
        return 0;
    }

    public InventoryItem removeFromBag(string itemName)
    {
        return removeFromBag(itemName, 1);
    }

    public InventoryItem removeFromBag(string itemName, int qtyOf)
    {
        return null;
    }

    private void printBagItems()
    {
        string[] items = getBagItems();
        Debug.Log("Bag items (" + items.Length + "):");
        for (int bi = 0; bi < items.Length; bi++)
        {
            Debug.Log("   >" + items[bi] + ":" + qtyInBag(items[bi]));
        }
    }
}
