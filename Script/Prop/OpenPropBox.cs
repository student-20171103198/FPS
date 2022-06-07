using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenPropBox : MonoBehaviour
{
    public GameObject[] propObjectPrefab;  //随机道具预制体，用于创建随机道具
    private Tips proptips;

    private void OnTriggerEnter(Collider other)
    {
        proptips = GameObject.Find("PropTip").GetComponent<Tips>();
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        //打开道具箱
        if (other.gameObject.tag == "Player")
        {
            proptips.OpenBox();
            if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
            {
                proptips.TextClear();
                CreateProp();
                this.gameObject.SetActive(false);
                //Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        proptips.TextClear();
    }

    private void CreateProp()  //随机生成道具
    {
        //创建
        var obj = GameObject.Instantiate(propObjectPrefab[Random.Range(0,propObjectPrefab.Length)]);
        obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
    }
}
