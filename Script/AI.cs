using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//状态
public enum EnemyState
{
    idle,
    patrol,
    walk,
    die,
    attack
}

public class AI : MonoBehaviour
{
    //初始
    Target enemyTarget;
    public EnemyState currentState;
    //动画控制器
    private Animator enemyAnimator;
    //玩家
    private Transform player;
    //导航
    private NavMeshAgent agent;

    private void Awake()
    {
        currentState = EnemyState.idle;
        enemyTarget = GetComponent<Target>();
        enemyAnimator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }
    private void Update()
    {
        //玩家与敌人的距离
        float distance = Vector3.Distance(player.position, transform.position);
        switch (currentState)
        {
            case EnemyState.idle:
                if (enemyTarget.health <= 0)
                {
                    //死亡判断
                    currentState = EnemyState.die;
                }
                else
                {
                    //站立状态判断
                    if (distance > 3 && distance <= 7)
                    {
                        currentState = EnemyState.walk;
                    }
                    enemyAnimator.SetBool("Walk Forward", false);
                    //导航停止
                    agent.isStopped = true;
                }               
                break;
            case EnemyState.walk:
                 if (enemyTarget.health <= 0)
                {
                    //死亡判断
                    currentState = EnemyState.die;
                }
                else
                {
                    //追踪状态
                    if (distance > 7)
                    {
                        currentState = EnemyState.idle;
                    }
                    else if(distance < 2.5)
                    {
                        currentState = EnemyState.attack;
                    }
                    enemyAnimator.SetBool("Walk Forward", true);
                    //导航开始
                    agent.isStopped = false;
                    agent.SetDestination(player.position);
                }                
                break;
            case EnemyState.attack:
                if (enemyTarget.health <= 0)
                {
                    //死亡判断
                    currentState = EnemyState.die;
                }
                else
                {
                    if (distance > 3 && distance < 7)
                    {
                        currentState = EnemyState.walk;
                    }
                    enemyAnimator.SetBool("Walk Forward", false);
                    enemyAnimator.SetTrigger("Stab Attack");
                    //导航停止
                    agent.isStopped = true;
                }
                break;
            case EnemyState.die:
                agent.isStopped = true;
                break;
        }
    }
}
