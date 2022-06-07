using Assets.Script.Weapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    //游戏控制
    private GameControl gameControl;
    //玩家
    private GameObject player;

    void Start()
    {
        gameControl = GameObject.FindWithTag("GameController").GetComponent<GameControl>();
        player = GameObject.FindGameObjectWithTag("Player");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            gameControl.PropNumClear();
            this.gameObject.SetActive(false);
            SceneManager.LoadScene("Level-02");
        }
    }
}
