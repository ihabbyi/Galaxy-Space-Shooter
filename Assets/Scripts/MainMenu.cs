using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void One_Player()
    {
        SceneManager.LoadScene("Single Player");
    }

    public void Two_Players()
    {
        SceneManager.LoadScene("Co_OP Mode");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
