using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public GameObject mObjectToFollow;
    // Start is called before the first frame update
    void Start()
    {
        var ParentPos = mObjectToFollow.transform.position;
        transform.position = new Vector3(ParentPos.x, ParentPos.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (mObjectToFollow != null)
        {
            var ParentPos = mObjectToFollow.transform.position;
            transform.position = new Vector3(ParentPos.x, ParentPos.y, transform.position.z);
        }
    }
}
