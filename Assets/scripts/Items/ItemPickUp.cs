using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public int GetItem()
    {


        return item.id;
    }
}
