using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Box : MonoBehaviour
{
    [HideInInspector]
    public float speed = 2.6f;
    private Transform target;
    private int waypointIndex = 0;

    public TextMeshProUGUI boxUINr;
    [HideInInspector]
    public int boxNr = -1;
    [HideInInspector]
    public float finalRotation;
    SpawnBoxes spawner;


    private void Start()
    {
        spawner = GetComponentInParent<SpawnBoxes>();
        target = spawner.waypoints[0];
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        if (transform.position == target.position)
        {
            GetNextWayPoint();
        }
    }
    void GetNextWayPoint()
    {
        if(waypointIndex >= spawner.waypoints.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        waypointIndex++;
        target = spawner.waypoints[waypointIndex];
    }
}
