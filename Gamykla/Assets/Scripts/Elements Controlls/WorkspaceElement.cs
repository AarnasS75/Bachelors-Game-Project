using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkspaceElement : MonoBehaviour
{
    public bool IsSnap { get; set; }
    public List<BoxCollider2D> ColList { get; private set; }

    private void Start() 
    {
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        ColList = new List<BoxCollider2D>();

        if (col == null)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).CompareTag("Element Part"))
                {
                    ColList.Add(transform.GetChild(i).GetComponent<BoxCollider2D>());
                }
            }
        }
        else
        {
            ColList.Add(col);
        }
    }
}
