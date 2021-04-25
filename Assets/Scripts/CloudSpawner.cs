using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject[] Clouds;
    public Timer SpawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        SpawnTimer.Start(); 
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTimer.Update(Time.deltaTime);

        if (SpawnTimer.Ended())
        {
            var CloudToSpawnIdx = Random.Range(0, Clouds.Length);
            var CloudToSpawnScale = Random.Range(5, 15);
            var UnitRand = Random.insideUnitSphere;
            UnitRand = new Vector3(UnitRand.x, -Mathf.Abs(UnitRand.y), UnitRand.z);
            var CloudToSpawnPos = UnitRand * CloudToSpawnScale * 2;
            var CloudToSpawn = GameObject.Instantiate(Clouds[CloudToSpawnIdx]);
            var CloudToSpawnScaleVec = new Vector3(CloudToSpawnScale, CloudToSpawnScale, 0);
            CloudToSpawn.transform.localScale = new Vector3(CloudToSpawnScale, CloudToSpawnScale, 0);
            var AddPosition = new Vector3(CloudToSpawnScaleVec.x * 2 * ((Random.value > 0.5f) ? 1 : -1), CloudToSpawnScaleVec.y * -2, 0);
            CloudToSpawn.transform.position = transform.position + CloudToSpawnPos + AddPosition ;
            CloudToSpawn.transform.position = new Vector3(CloudToSpawn.transform.position.x, CloudToSpawn.transform.position.y, 0);

            CloudToSpawn.GetComponent<SpriteRenderer>().sortingOrder = Random.Range(-10, 10);
        }
    }
}
