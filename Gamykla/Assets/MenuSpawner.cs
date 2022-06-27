using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSpawner : MonoBehaviour
{
    public GameObject[] prefab;
    public float speed;
    [HideInInspector]
    public Transform[] waypoints;
    public int layer;
    public float scale;

    float[] angles = { 90, -90, 180, 0 };

    private void Awake()
    {
        waypoints = new Transform[transform.GetChild(0).childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(0).GetChild(i);
        }
    }
    public void SpawnNew()
    {
        GameObject obj = Instantiate(prefab[Random.Range(0, prefab.Length)], transform.position, Quaternion.identity, transform);
        obj.GetComponent<BoxUI>().speed = speed;
        obj.GetComponent<SpriteRenderer>().sortingOrder = layer;
        obj.transform.localScale = new Vector2(scale, scale);
        obj.transform.eulerAngles = new Vector3(0, 0, angles[Random.Range(0, angles.Length)]);
    }
}
