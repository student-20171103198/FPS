using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private GameObject gameControl;
    private PlayerKilledNum killedNum;
    //敌人血量
    public float health;
    private Transform enemyTransform;
    //动画控制器
    private Animator enemyAnimator;
    private float tempTime;
    //弹药包
    public GameObject ammoPack;

    private void Start()
    {
        gameControl = GameObject.FindGameObjectWithTag("GameController");
        killedNum = gameControl.GetComponent<PlayerKilledNum>();
        enemyAnimator = GetComponent<Animator>();
        tempTime = 1;
        health +=  Time.time / 5;
    }
    void Update()
    {
        //Debug.Log(health);
        enemyTransform = this.transform;
    }
    public void takeDamage(float amount)
    {
        enemyAnimator.SetTrigger("Take Damage");
        health = health - amount;
        if(health <= 0f)
        {
            Die();
        }
    }
    void Die()
    {
        enemyAnimator.SetTrigger("Die");
        killedNum.KilledAdd();
        this.gameObject.tag = "Untagged";
        Destroy(this.gameObject,10f);
    }
    
}
