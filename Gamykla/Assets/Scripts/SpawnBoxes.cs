using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoxes : MonoBehaviour
{
    public Transform prefab;

    public float delay = 2f;
    float countDown = 1f;
    [HideInInspector]
    public bool startLevel = false;

    public int maxCount;
    [HideInInspector]
    public int count;

    [HideInInspector]
    public Transform[] waypoints;

    public float speed;

    public float assignNumber;
    public Color assignColor;
    public Sprite assignShape;
    public bool assignRotation = false;
    private void Awake()
    {
        waypoints = new Transform[transform.GetChild(0).childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(0).GetChild(i);
        }
    }
    private void Update()
    {
        if (startLevel)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        if (countDown <= 0 && maxCount > count)
        {
            Transform box = Instantiate(prefab, transform.position, Quaternion.identity);
            box.SetParent(transform);

            if (!assignNumber.Equals(0))
            {
                box.GetComponent<Box>().boxUINr.SetText($"{assignNumber}");
                box.GetComponent<Box>().boxNr = (int)assignNumber;
            }
            else
            {
                box.GetComponent<Box>().boxNr = Random.Range(1, 9);
                box.GetComponent<Box>().boxUINr.SetText($"{box.GetComponent<Box>().boxNr}");
            }
            box.GetComponentInChildren<SpriteRenderer>().color = new Color(assignColor.r, assignColor.g, assignColor.b, 1f);
            if(assignShape != null)
            {
                box.GetComponentInChildren<SpriteRenderer>().sprite = assignShape;
            }
            box.GetComponent<Box>().speed = speed;
            count++;
            countDown = delay;
        }
        countDown -= Time.deltaTime;
    }
}
