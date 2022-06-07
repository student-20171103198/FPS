using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKilledNum : MonoBehaviour
{
    private float playerKilled = 0;


    public void KilledAdd()
    {
        playerKilled += 1;
    }

    public float GetKilledNum()
    {
        return playerKilled;
    }
}
