using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject[] gameObjectPrefab;  //游戏预制体，用于创建怪物
    public GameObject[] gameObjectPropPrefab;  //游戏预制体，用于创建道具箱
    public GameObject gameObjectPortal;

    private Target target;
    //怪物的创建频率
    public float createSpeed;
    private float enemyCreateTime;
    //道具创建的频率
    public float createPropSpeed;
    private float propCreateTime;
    //道具创建的上限
    public float maxPropNum;
    private float currentPropNum;
    //游戏时间
    private float gameTime;
    //玩家
    private Transform player;
    //玩家击杀计数
    public float playerKilledNum;
    private PlayerKilledNum killedNum;

    private void Start()
    {
        target = GetComponent<Target>();
        killedNum = GetComponent<PlayerKilledNum>();
        player = GameObject.FindWithTag("Player").transform;
        playerKilledNum = 0;
        gameObjectPortal.SetActive(false);
    }

    private void Update()
    {
        if(Time.time - enemyCreateTime > createSpeed)
        {
            //创建敌人
            var enemyobj = GameObject.Instantiate(gameObjectPrefab[Random.Range(0, gameObjectPrefab.Length)]);
            enemyobj.transform.position = new Vector3((player.position.x + Random.Range(-8f, 8f)), 0, (player.position.z + Random.Range(-8f, 8f)));
            enemyobj.SetActive(true);
            enemyCreateTime = Time.time;
        }

        if (Time.time - propCreateTime > createPropSpeed && currentPropNum < maxPropNum)
        {
            //创建道具
            var propobj = GameObject.Instantiate(gameObjectPropPrefab[Random.Range(0, gameObjectPropPrefab.Length)]);
            propobj.transform.position = new Vector3((player.position.x + Random.Range(-20f, 20f)), 0, (player.position.z + Random.Range(-20f, 20f)));
            propobj.SetActive(true);
            currentPropNum += 1;
            propCreateTime = Time.time;
        }

        playerKilledNum = killedNum.GetKilledNum();
        Debug.Log(playerKilledNum);
        if(Time.time - gameTime > 10)
        {
            //开启传送门
            gameObjectPortal.SetActive(true);
            gameTime = Time.time;
        }
    }

    public void PropNumClear()  //道具计数清零
    {
        currentPropNum = 0;
    }
}
