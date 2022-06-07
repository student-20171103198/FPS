using Assets.Script.Weapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //玩家
    private PlayerHp playerHp;
    //玩家背包
    private List<int> playerItems;
    private float hp;
    public static float playerDamage = 10f;
    //道具
    private int isAddAttack = 0;
    private int isHp = 0;
    private int isAddHp = 0;
    void Start()
    {
        playerHp = GameObject.FindWithTag("Player").GetComponent<PlayerHp>();
        playerItems = new List<int>();
    }

    public void AddItem(int itemId)
    {
        if(!playerItems.Contains(itemId))
        {
            playerItems.Add(itemId);
        }
    }
    public void HasItem(int itemId)
    {
        switch(itemId)
        {
            case 1:
                isAddAttack += 1;
                playerDamage += isAddAttack * 10;
                break;
            case 2:
                isHp += 1;
                playerHp.playerHp += 20;
                break;
            case 3:
                isAddHp += 1;
                break;
        }
    }

    public float getDamage()
    {
        return playerDamage;
    }
}
