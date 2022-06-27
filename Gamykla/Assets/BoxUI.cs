using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoxUI : MonoBehaviour
{
    public float speed;
    Transform target;
    public int waypointIndex = 0;

    [HideInInspector]
    public int boxNr = -1;
    [HideInInspector]
    public float finalRotation;

    MenuSpawner spawner;

    Color[] colors = { Color.white, Color.red, Color.green, Color.white, new Color(0f, 0.5f, 1f,1f), Color.white ,Color.yellow };

    private void Start()
    {
        GetComponent<SpriteRenderer>().material.color = colors[Random.Range(0, colors.Length)];
        spawner = GetComponentInParent<MenuSpawner>();
        target = spawner.waypoints[waypointIndex];
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
        if (waypointIndex >= spawner.waypoints.Length - 1)
        {
            spawner.SpawnNew();
            Destroy(gameObject);
            return;
        }
        waypointIndex++;
        target = spawner.waypoints[waypointIndex];
    }
}
