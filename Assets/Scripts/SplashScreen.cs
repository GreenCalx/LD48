using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public Timer T;

    public string SceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        if ( (SceneToLoad==null) || (SceneToLoad.Length==0))
            SceneToLoad = "MenuScene";
    }

    // Update is called once per frame
    void Update()
    {
        T.Update(Time.deltaTime);

        if (Input.anyKey || T.Ended()) SceneManager.LoadScene(SceneToLoad);
    }
}
