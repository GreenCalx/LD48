using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCallbacks : MonoBehaviour
{
    public Canvas Menu;

    public Canvas Help;

    public void StartGame()
    {
        SceneManager.LoadScene("Intro");
    }

    public void StartConfigMenu()
    {
        Menu.enabled = true;
    }

    public void StartHelp()
    {
        Help.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        Menu.enabled = false;
        Help.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            StartGame();
        }

        if(Menu.enabled || Help.enabled)
        {
            if(Input.anyKey)
            {
                Menu.enabled = false;
                Help.enabled = false;
            }
        }
        
    }
}
