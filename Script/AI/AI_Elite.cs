using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_elite : MonoBehaviour
{

    //敌人状态
    public EnemyState currentState;
    Target enemyTarget;
    public float[] actionWeight = { 1000, 2000 };
    private float powerfulAttackTime;
    private float lastActionTime;
    
    private Animator enemyAnimator; //动画控制器
    
    private Transform player;   //玩家
    
    private NavMeshAgent agent; //导航

    private Vector3 startPosition;
    private float distanceToPlayer;  //与玩家距离
    private float distanceToStart;  //与初始距离

    private Quaternion targetRotation;  //目标朝向
    public float walkRadius = 7f;  //追击半径
    public float patrolRadius = 5f;  //巡逻半径
    public float attackRadius = 2.5f;  //攻击半径

    public float walkSpeed;  //追击速度
    public float turnSpeed;  //转身速度

    void Awake()
    {
        currentState = EnemyState.idle;
        enemyTarget = GetComponent<Target>();
        enemyAnimator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        startPosition = transform.position;
    }


    void Update()
    {

        distanceToPlayer = Vector3.Distance(player.position, transform.position);

        //死亡判断
        if (enemyTarget.health <= 0f)
        {
            currentState = EnemyState.die;
        }
        else
        {
            switch (currentState)
            {
                case EnemyState.idle:
                    if (distanceToPlayer > 3 && distanceToPlayer <= walkRadius)
                        currentState = EnemyState.walk;
                    if (Time.time - lastActionTime > 5)
                    {
                        RandomAction();
                        lastActionTime = Time.time;
                    }
                    //导航停止
                    agent.isStopped = true;
                    break;

                case EnemyState.patrol:
                    if (distanceToPlayer > 3 && distanceToPlayer <= walkRadius)
                        currentState = EnemyState.walk;
                    transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed);
                    if (Time.time - lastActionTime > 5)
                    {
                        RandomAction();
                        lastActionTime = Time.time;
                    }
                    //导航停止
                    agent.isStopped = true;
                    break;

                case EnemyState.walk:
                    if (distanceToPlayer > walkRadius)
                        RandomAction();
                    else if (distanceToPlayer < attackRadius)
                        currentState = EnemyState.attack;
                    enemyAnimator.SetBool("Walk Forward", true);
                    //导航开始
                    agent.isStopped = false;
                    agent.SetDestination(player.position);
                    break;

                case EnemyState.attack:
                    if (distanceToPlayer > 3 && distanceToPlayer <= walkRadius)
                        currentState = EnemyState.walk;
                    enemyAnimator.SetBool("Walk Forward", false);
                    if (Time.time - powerfulAttackTime > 5)
                    {
                        enemyAnimator.SetTrigger("Smash Attack");
                        powerfulAttackTime = Time.time;
                    }
                    else
                        enemyAnimator.SetTrigger("Stab Attack");
                    //导航停止
                    agent.isStopped = true;
                    break;

                case EnemyState.die:
                    //导航停止
                    walkSpeed = 0;
                    agent.isStopped = true;
                    break;
            }

        }
    }

    private void RandomAction()
    {
        float num = Random.Range(0, actionWeight[0] + actionWeight[1]);
        //根据权重判断敌人待机状态
        if (num > actionWeight[0] && num <= actionWeight[0] + actionWeight[1])
        {
            currentState = EnemyState.patrol;
            targetRotation = Quaternion.Euler(0, Random.Range(1, 5) * 90, 0);
            enemyAnimator.SetBool("Walk Forward", true);
        }
        else
        {
            currentState = EnemyState.idle;
            enemyAnimator.SetBool("Walk Forward", false);
        }
    }
}
