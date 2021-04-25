using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeepnessWidget : MonoBehaviour
{
    public TextMeshProUGUI ValueText;
    public GameObject EndMarker;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var GrdDist = Player.transform.position.y - EndMarker.transform.position.y;
        ValueText.text = GrdDist.ToString();
        
    }
}
