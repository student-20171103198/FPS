using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal_End : MonoBehaviour
{
    public float[] portalWeight = { 1000, 2000 };

    private GameObject player;
    private Tips proptips;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        proptips = GameObject.Find("PropTip").GetComponent<Tips>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            proptips.PortalSelect();
            if (other.gameObject == player && Input.GetKeyDown(KeyCode.E))
            {
                proptips.TextClear();
                Debug.Log(other.gameObject.name);
                RandomPortal();
            }
            else if (other.gameObject == player && Input.GetKeyDown(KeyCode.Q))
            {
                proptips.TextClear();
                Debug.Log(other.gameObject.name);
                SceneManager.LoadScene("StartGame");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        proptips.TextClear();
    }

    private void RandomPortal()
    {
        float num = Random.Range(0, portalWeight[0] + portalWeight[1]);
        if (num > 0 && num <= portalWeight[0])
            SceneManager.LoadScene("Level-01");
        else
            SceneManager.LoadScene("Level-02");
    }
}
