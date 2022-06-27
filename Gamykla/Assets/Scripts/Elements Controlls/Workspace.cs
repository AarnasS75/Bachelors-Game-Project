using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workspace : MonoBehaviour
{
    private Player player;

    private WorkspaceElement parentElement;

    private BoxCollider2D col;
    [HideInInspector]
    public Vector2 Size;

    [HideInInspector]
    public Vector2 StartPos;

    private List<WorkspaceElement> elementsInWorkspace;

    [HideInInspector]
    public bool workspaceSelected = false;

    private void Start()
    {
        elementsInWorkspace = new List<WorkspaceElement>();

        col = GetComponent<BoxCollider2D>();
        Size = (Vector2)col.bounds.size;
        StartPos = new Vector2(col.bounds.center.x - col.bounds.size.x / 2, col.bounds.center.y + col.bounds.size.y / 2);

        player = GameObject.Find("Player").GetComponent<Player>();

        if (transform.parent != null)
        {
            parentElement = transform.parent.GetComponent<WorkspaceElement>();
        }
    }

    private void Update()
    {
        StartPos = new Vector2(col.bounds.center.x - col.bounds.size.x / 2, col.bounds.center.y + col.bounds.size.y / 2);

        CheckSize();
        CheckPositions();
        CheckTarget();
    }

    public void CheckSize()
    {
        if (parentElement != null)
        {
            Vector3 scale = transform.parent.GetChild(1).GetComponent<SpriteRenderer>().size;

            float height = 0;
            float width = 0;
            float offsetX = 0;

            if (elementsInWorkspace.Count > 0)
            {
                for (int i = 0; i < elementsInWorkspace.Count; i++)
                {
                    for (int k = 0; k < elementsInWorkspace[i].ColList.Count; k++)
                    {
                        height += elementsInWorkspace[i].ColList[k].bounds.size.y;

                        if (width < elementsInWorkspace[i].ColList[k].bounds.size.x)
                        {
                            width = elementsInWorkspace[i].ColList[k].bounds.size.x;
                        }
                    }
                }

                scale.y = height;
            }
            else
            {
                width = 3.64f;
                scale.y = 1;
            }

            if (width < 3.64f)
            {
                width = 3.64f;
            }

            offsetX = (-1.1f + (width - 1) * 0.5f) - 0.04f;

            transform.parent.GetChild(1).localScale = new Vector2(1, scale.y);

            Vector3 positionChild1 = transform.parent.GetChild(0).localPosition;
            float sizeChild1 = transform.parent.GetChild(0).GetComponent<SpriteRenderer>().size.y;
            positionChild1.y = scale.y / 2 + sizeChild1 / 2;
            transform.parent.GetChild(0).localPosition = positionChild1;

            Vector3 positionChild2 = transform.parent.GetChild(2).localPosition;
            float sizeChild2 = transform.parent.GetChild(2).GetComponent<SpriteRenderer>().size.y;
            positionChild2.y = -scale.y / 2 - sizeChild2 / 2;
            transform.parent.GetChild(2).localPosition = positionChild2;

            col.size = new Vector2(width, scale.y);
            col.offset = new Vector2(offsetX, col.offset.y);

            Size = (Vector2)col.bounds.size;
            StartPos = new Vector2(col.bounds.center.x - col.bounds.size.x / 2, col.bounds.center.y + col.bounds.size.y / 2);
        }
    }

    private void CheckPositions()
    {
        if (parentElement != null)
        {
            if (!parentElement.IsSnap)
            {
                return;
            }
        }

        if (elementsInWorkspace.Count > 0)
        {
            Vector2 elementSize = Vector2.zero;

            for (int i = 0; i < elementsInWorkspace[0].ColList.Count; i++)
            {
                elementSize += new Vector2(0, elementsInWorkspace[0].ColList[i].bounds.size.y);

                if (elementSize.x < elementsInWorkspace[0].ColList[i].bounds.size.x)
                {
                    elementSize.x = elementsInWorkspace[0].ColList[i].bounds.size.x;
                }
            }

            Vector2 newPos = new Vector2(StartPos.x + elementSize.x / 2, StartPos.y - elementSize.y / 2);
            elementsInWorkspace[0].transform.position = newPos;

            if (elementsInWorkspace.Count > 1)
            {
                float offsetY = elementSize.y;

                for (int i = 1; i < elementsInWorkspace.Count; i++)
                {
                    elementSize = Vector2.zero;
                    for (int k = 0; k < elementsInWorkspace[i].ColList.Count; k++)
                    {
                        elementSize += new Vector2(0, elementsInWorkspace[i].ColList[k].bounds.size.y);

                        if (elementSize.x < elementsInWorkspace[i].ColList[k].bounds.size.x)
                        {
                            elementSize.x = elementsInWorkspace[i].ColList[k].bounds.size.x;
                        }
                    }
                    newPos = new Vector2(StartPos.x + elementSize.x / 2, StartPos.y - elementSize.y / 2 - offsetY);
                    elementsInWorkspace[i].transform.position = newPos;

                    offsetY += elementSize.y;
                }
            }
        }
    }

    private void CheckTarget()
    {
        if (player.GetWorkspace() == this)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            int targetIndex = elementsInWorkspace.Count;

            if (player.GetIsMouseDown())
            {
                for (int i = 0; i < elementsInWorkspace.Count; i++)
                {
                    Vector3 pos = elementsInWorkspace[i].transform.position;

                    Vector2 size = Vector2.zero;

                    for (int k = 0; k < elementsInWorkspace[i].ColList.Count; k++)
                    {
                        size.y += elementsInWorkspace[i].ColList[k].size.y;

                        if (size.x < elementsInWorkspace[i].ColList[k].bounds.size.x)
                        {
                            size.x = elementsInWorkspace[i].ColList[k].bounds.size.x;
                        }
                    }
                    size /= 2;

                    if (mousePos.y <= pos.y + size.y && mousePos.y >= pos.y)
                    {
                        targetIndex = i;
                        break;
                    }
                    else if (mousePos.y >= pos.y - size.y && mousePos.y <= pos.y)
                    {
                        targetIndex = i + 1;
                        break;
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    for (int i = 0; i < elementsInWorkspace.Count; i++)
                    {
                        Vector3 pos = Vector3.zero;
                        Vector2 size = Vector2.zero;

                        for (int k = 0; k < elementsInWorkspace[i].ColList.Count; k++)
                        {
                            pos = elementsInWorkspace[i].ColList[k].bounds.center;
                            size = elementsInWorkspace[i].ColList[k].bounds.size / 2;

                            if (mousePos.x >= pos.x - size.x && mousePos.x <= pos.x + size.x &&
                                mousePos.y >= pos.y - size.y && mousePos.y <= pos.y + size.y)
                            {
                                player.SetMouseDownElement(elementsInWorkspace[i]);
                                RemoveElement(elementsInWorkspace[i]);
                                break;
                            }
                        }
                    }
                }
            }

            player.SetTargetIndex(targetIndex);
        }
    }

    public void AddElement(WorkspaceElement newElement, int index)
    {
        if (elementsInWorkspace.Count == 0 || index == elementsInWorkspace.Count)
        {
            elementsInWorkspace.Add(newElement);
        }
        else
        {
            WorkspaceElement tempElement1 = elementsInWorkspace[index];
            elementsInWorkspace[index] = newElement;

            for (int i = index + 1; i < elementsInWorkspace.Count; i++)
            {
                WorkspaceElement tempElement2 = elementsInWorkspace[i];
                elementsInWorkspace[i] = tempElement1;
                tempElement1 = tempElement2;
            }

            elementsInWorkspace.Add(tempElement1);
        }

        Workspace[] workspaces = newElement.GetComponentsInChildren<Workspace>();
        if (workspaces.Length > 0)
        {
            for (int i = 0; i < workspaces.Length; i++)
            {
                for (int k = 0; k < workspaces[i].elementsInWorkspace.Count; k++)
                {
                    workspaces[i].elementsInWorkspace[k].IsSnap = true;
                }
            }
        }

        newElement.transform.parent = transform;
        newElement.IsSnap = true;
    }

    public void RemoveElement(WorkspaceElement removedElement)
    {
        elementsInWorkspace.Remove(removedElement);

        Workspace[] workspaces = removedElement.GetComponentsInChildren<Workspace>();

        if (workspaces.Length > 0)
        {
            for (int i = 0; i < workspaces.Length; i++)
            {
                for (int k = 0; k < workspaces[i].elementsInWorkspace.Count; k++)
                {
                    workspaces[i].elementsInWorkspace[k].IsSnap = false;
                }
            }
        }
        removedElement.transform.parent = null;
        removedElement.IsSnap = false;
    }

    public int GetIndex(WorkspaceElement element)
    {
        return elementsInWorkspace.FindIndex(c => c == element);
    }

    private void OnDestroy()
    {
        if (elementsInWorkspace.Count > 0)
        {
            foreach (WorkspaceElement element in elementsInWorkspace)
            {
                Destroy(element.gameObject);
            }
        }
    }
}
