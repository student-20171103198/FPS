using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpProp : MonoBehaviour
{
    public int propId;

    private GameObject player;
    private PlayerInventory playerInventory;
    private Tips proptips;
    private Text tip;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = player.GetComponent<PlayerInventory>();
        proptips = GameObject.Find("PropTip").GetComponent<Tips>();
        tip = GameObject.Find("PropTip").GetComponent<Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInventory.HasItem(propId);
            proptips.GetProp();
            Invoke("ClearText", 3);
            Destroy(this.gameObject);
            
        }
    }

    private void ClearText()
    {
        tip.text = "";
    }
}
