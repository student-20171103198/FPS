using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public float playerHp;
    private Animator playerAnimator;
    private Slider playerHealth;
    private bool colliderActive;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerHealth = GameObject.Find("PlayerHealth").GetComponent<Slider>();
        colliderActive = true;
    }

    private void Update()
    {
        if(playerHp <= 0)
        {
            if (colliderActive)
            {
                Die();
            }
            
        }
    }

    public void takeDamage(float damage)
    {
        playerHp -= damage;
        playerHealth.value = playerHp / 100f; 
    }

    public float getHp()
    {
        return playerHp;
    }
    void Die()
    {
        colliderActive = false;
        playerAnimator.SetTrigger("Die");
        Debug.Log(playerHp);
        Invoke("ReStartGame", 2);
    }
    void ReStartGame()
    {
        SceneManager.LoadScene("ReStartGame");
    }
}
