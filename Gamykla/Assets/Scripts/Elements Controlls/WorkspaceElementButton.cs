using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkspaceElementButton : MonoBehaviour
{
    public GameObject prefab;
    Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnMouseDown()
    {
        player.SetMouseDownButtonElement(this);
    }
}
