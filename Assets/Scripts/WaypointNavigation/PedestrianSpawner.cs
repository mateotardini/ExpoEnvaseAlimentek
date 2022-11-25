using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianSpawner : MonoBehaviour
{

    public GameObject[] pedestrianPrefab;
    public int pedestriansToSpawn;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());

    }

    IEnumerator Spawn()
    {
        int count = 0;
        while(count < pedestriansToSpawn)
        {
            GameObject obj = Instantiate(pedestrianPrefab[Random.Range(0,4)]);
            Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            obj.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
            obj.transform.position = child.position;

            yield return new WaitForSeconds(0.1f);

            count++;
        }
    }
}
