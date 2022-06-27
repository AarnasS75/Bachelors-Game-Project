using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    List<Workspace> workspaceList = new List<Workspace>();

    private Workspace selectedWorkspace;
    private GameObject usedElementObj;

    GameObject[] mainWorkspaces;

    private bool isMouseDown;
    private bool isMouseInWorkspace;

    private int targetIndex;

    Vector3 mousePos;

    private void Start() 
    {
        mainWorkspaces = GameObject.FindGameObjectsWithTag("Main Workspace");
        for (int i = 0; i < mainWorkspaces.Length; i++)
        {
            workspaceList.Add(mainWorkspaces[i].GetComponent<Workspace>());
        }
    }

    private void Update()
    {

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        GameObject[] workspaces = GameObject.FindGameObjectsWithTag("Workspace");
        workspaceList.Clear();
        for (int i = 0; i < mainWorkspaces.Length; i++)
        {
            workspaceList.Add(mainWorkspaces[i].GetComponent<Workspace>());
        }
        for (int i = 0; i < workspaces.Length; i++)
        {
            if (workspaces[i].transform.parent.GetComponent<WorkspaceElement>().IsSnap)
            {
                workspaceList.Add(workspaces[i].GetComponent<Workspace>());
            }
        }
        for (int i = 0; i < mainWorkspaces.Length; i++)
        {
            if (mousePos.x >= workspaceList[i].StartPos.x && mousePos.x <= workspaceList[i].StartPos.x + workspaceList[i].Size.x &&
               mousePos.y <= workspaceList[i].StartPos.y && mousePos.y >= workspaceList[i].StartPos.y - workspaceList[i].Size.y)
            {
                SetMouseInWorkspace(workspaceList[i]);
            }
            else if (selectedWorkspace == workspaceList[i])
            {
                SetMouseOutWorkspace();
            }
        }

        if (workspaceList.Count > mainWorkspaces.Length)
        {
            for (int i = mainWorkspaces.Length; i < workspaceList.Count; i++)
            {
                WorkspaceElement workspaceElement = workspaceList[i].transform.parent.GetComponent<WorkspaceElement>();
                if (workspaceElement)
                {
                    if (workspaceElement.IsSnap &&
                        mousePos.x >= workspaceList[i].StartPos.x && mousePos.x <= workspaceList[i].StartPos.x + workspaceList[i].Size.x &&
                        mousePos.y <= workspaceList[i].StartPos.y && mousePos.y >= workspaceList[i].StartPos.y - workspaceList[i].Size.y)
                    {
                        SetMouseInWorkspace(workspaceList[i]);
                    }
                    else if (selectedWorkspace == workspaceList[i])
                    {
                        SetMouseOutWorkspace();
                    }
                }
                else
                {
                    if (mousePos.x >= workspaceList[i].StartPos.x && mousePos.x <= workspaceList[i].StartPos.x + workspaceList[i].Size.x &&
                        mousePos.y <= workspaceList[i].StartPos.y && mousePos.y >= workspaceList[i].StartPos.y - workspaceList[i].Size.y)
                    {
                        SetMouseInWorkspace(workspaceList[i]);
                    }
                    else if (selectedWorkspace == workspaceList[i])
                    {
                        SetMouseOutWorkspace();
                    }
                }
            }
        }

        if (usedElementObj != null)
        {
            usedElementObj.transform.position = mousePos;

            if (isMouseDown)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    if (isMouseInWorkspace)
                    {
                        CheckIfElementFits();
                    }
                    else
                    {
                        Destroy(usedElementObj);
                    }

                    if (usedElementObj != null)
                    {
                        SetRenderPosition(usedElementObj, 3);
                        usedElementObj = null;
                    }

                    isMouseDown = false;
                }
            }
        }
    }

    void CheckIfElementFits()
    {
        Transform mainWorkspaceTransform = selectedWorkspace.transform.root.GetChild(0);

        selectedWorkspace.GetComponent<Workspace>().workspaceSelected = true;

        float elementsInWorkspaceHeight = 0;
        float objectInUseHeight = 0;

        #region Used Element Height
        if (usedElementObj.transform.childCount > 0)
        {
            objectInUseHeight = usedElementObj.transform.GetChild(1).localScale.y + 1.2f;
        }
        else
        {
            objectInUseHeight = usedElementObj.GetComponent<BoxCollider2D>().size.y;
        }
        #endregion

        if (mainWorkspaceTransform.transform.childCount > 0)
        {
            for (int i = 0; i < mainWorkspaceTransform.transform.childCount; i++)
            {
                Transform mainChild = mainWorkspaceTransform.transform.GetChild(i);

                if (mainChild.transform.childCount > 0)
                {
                    if (selectedWorkspace.transform.IsChildOf(mainChild))
                    {
                        Workspace[] childrenWorkspaces = mainChild.transform.GetChild(3).transform.GetComponentsInChildren<Workspace>();

                        float heightDifference = 0;

                        foreach (Workspace item in childrenWorkspaces)
                        {
                            if (item.workspaceSelected)
                            {
                                int index = Array.IndexOf(childrenWorkspaces, item);

                                if (childrenWorkspaces[index].transform.childCount > 0)
                                {
                                    heightDifference = objectInUseHeight + childrenWorkspaces[index].transform.parent.transform.GetChild(1).transform.localScale.y + 1.2f;
                                }
                                else
                                {
                                    heightDifference = objectInUseHeight + 1.2f;
                                }

                                while (index > 0)
                                {
                                    if (childrenWorkspaces[index - 1].transform.childCount > 1)
                                    {
                                        heightDifference = Mathf.Abs(childrenWorkspaces[index - 1].transform.parent.transform.GetChild(1).transform.localScale.y -
                                                                     (childrenWorkspaces[index - 1].transform.parent.transform.GetChild(1).transform.localScale.y - heightDifference))
                                            + 1.2f;
                                    }
                                    else
                                    {
                                        heightDifference += 1.2f;
                                    }

                                    index--;
                                }

                                elementsInWorkspaceHeight += heightDifference - objectInUseHeight;
                            }
                        }
                    }
                    else
                    {
                        elementsInWorkspaceHeight += mainChild.transform.GetChild(1).transform.localScale.y + 1.2f;
                    }
                }
                else
                {
                    elementsInWorkspaceHeight += mainChild.GetComponent<BoxCollider2D>().size.y;
                }
            }
        }

        if (elementsInWorkspaceHeight + objectInUseHeight > mainWorkspaceTransform.GetComponent<Workspace>().Size.y)
        {
            Destroy(usedElementObj);
            FindObjectOfType<AudioManager>().Play("Error");
            GameManager.instance.DisplayErrorMessage(mainWorkspaceTransform, "Nėra vietos įterpti elementą", true);
        }
        else
        {
            selectedWorkspace.AddElement(usedElementObj.GetComponent<WorkspaceElement>(), targetIndex);
            selectedWorkspace.GetComponent<Workspace>().workspaceSelected = false;
        }
    }

    void SetRenderPosition(GameObject obj, int position)
    {
        if(obj.transform.childCount > 0)
        {
            var items = obj.GetComponentsInChildren<SpriteRenderer>();
            foreach (var item in items)
            {
                item.sortingOrder = position;
            }
        }
        else
        {
            obj.GetComponent<SpriteRenderer>().sortingOrder = position;
        }
    }

    public void SetMouseDownButtonElement(WorkspaceElementButton element)
    {
        if (GameManager.instance.inputEnabled)
        {
            isMouseDown = true;
            usedElementObj = Instantiate(element.prefab);
            SetRenderPosition(usedElementObj, 10);
        }
    }

    public void SetMouseDownElement(WorkspaceElement element)
    {
        if (GameManager.instance.inputEnabled)
        {
            isMouseDown = true;
            usedElementObj = element.gameObject;
            SetRenderPosition(usedElementObj, 10);
        }
    }

    public bool GetIsMouseDown() => isMouseDown;

    public void SetMouseInWorkspace(Workspace workspace)
    {
        if (GameManager.instance.inputEnabled)
        {
            this.selectedWorkspace = workspace;
            isMouseInWorkspace = true;
        }
       
    }

    public void SetMouseOutWorkspace()
    {
        if (GameManager.instance.inputEnabled)
        {
            selectedWorkspace = null;
            isMouseInWorkspace = false;
        }
    }

    public void SetTargetIndex(int value) => targetIndex = value;

    public Workspace GetWorkspace() => selectedWorkspace;
}
