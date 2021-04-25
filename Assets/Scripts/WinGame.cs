using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class WinGame : MonoBehaviour
{
    BoxCollider2D bc2d;

    // Start is called before the first frame update
    void Start()
    {
        bc2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D( Collider2D iCol )
    {
        if (!!iCol.GetComponent<PlayerController>())
            call_end();
    }

    void call_end()
    {
        SceneManager.LoadScene("Victory");
    }
}
