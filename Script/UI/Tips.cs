using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tips : MonoBehaviour
{
    private Text propTip;
    void Start()
    {
        propTip = GameObject.Find("PropTip").GetComponent<Text>();
    }

    public void TextClear()
    {
        propTip.text = "";
    }

    public void OpenBox()
    {
        propTip.fontSize = 20;
        propTip.text = "按'E'键打开道具箱";
    }

    public void GetProp()
    {
        propTip.fontSize = 20;
        propTip.text = "获得道具";
    }

    public void PortalSelect()
    {
        propTip.fontSize = 13;
        propTip.text = "'E'键继续游戏    'Q'键返回主菜单";
    }
}
