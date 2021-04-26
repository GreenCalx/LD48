using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCallbacks : MonoBehaviour
{
    public Canvas Menu;

    public void StartGame()
    {
        SceneManager.LoadScene("Intro");
    }

    public void StartConfigMenu()
    {
        Menu.enabled = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        Menu.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Menu.enabled)
        {
            if(Input.anyKey)
            {
                Menu.enabled = false;
            }
        }
        
    }
}
