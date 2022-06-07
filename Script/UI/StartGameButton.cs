using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    public GameObject btn;

    private void Start()
    {
        Text text = GetComponent<Text>();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Click()
    {
        SceneManager.LoadScene("Level-01");
    }

    public void CloseClick()
    {
        Application.Quit();
    }
}
