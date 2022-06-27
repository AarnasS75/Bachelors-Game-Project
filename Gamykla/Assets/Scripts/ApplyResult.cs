using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ApplyResult : MonoBehaviour
{
    public GameObject plachelderUIIntValue, placeholderUIColorValue;

    TextMeshProUGUI intUIText, colorUIText;

    Box box;
    [HideInInspector]
    public int x = -1;
    [HideInInspector]
    public string y = "NAN";
    [HideInInspector]
    public int[] verteArray = new int[0];
    [HideInInspector]
    public string[] spalvaArray = new string[0];
    [HideInInspector]
    public int iForLoopValue = -1;
    [HideInInspector]
    public string[] formsArray = new string[0];

    [SerializeField]
    Sprite[] formSprites;

    WorkspaceResult workspaceResult;

    private void Awake()
    {
        if (placeholderUIColorValue != null)
        {
            colorUIText = placeholderUIColorValue.GetComponent<TextMeshProUGUI>();
        }
        if(plachelderUIIntValue != null)
        {
            intUIText = plachelderUIIntValue.GetComponent<TextMeshProUGUI>();
        }
        workspaceResult = GetComponent<WorkspaceResult>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            box = collision.gameObject.GetComponent<Box>();
            UpdateUIValues(collision);
            workspaceResult.CalculateWorkspaceResult();
            ResetVariables();
        }
    }
    void ResetVariables()
    {
        x = -1;
        y = "NAN";
        verteArray = new int[0];
        spalvaArray = new string[0];
        formsArray = new string[0];
        iForLoopValue = -1;
    }
    void UpdateUIValues(Collider2D boxObj)
    {
        if(plachelderUIIntValue != null)
        {
            intUIText.SetText($"int x = {box.boxNr};");
            x = box.boxNr;
        }
        
        if (placeholderUIColorValue != null)
        {
            if (boxObj.GetComponentInChildren<SpriteRenderer>().color.Equals(new Color(1f, 1f, 1f, 1f)))
            {
                colorUIText.SetText("string y = B;");
                y = "B";
            }
            else if (boxObj.GetComponentInChildren<SpriteRenderer>().color.Equals(new Color(0f, 1f, 0f, 1f)))
            {
                colorUIText.SetText("string y = Ž;");
                y = "Z";
            }
            else if (boxObj.GetComponentInChildren<SpriteRenderer>().color.Equals(new Color(0f, 0.5f, 1f, 1f)))
            {
                colorUIText.SetText("string y = M;");
                y = "M";
            }
            else if (boxObj.GetComponentInChildren<SpriteRenderer>().color.Equals(new Color(1f, 1f, 0f, 1f)))
            {
                colorUIText.SetText("string y = G;");
                y = "G";
            }
            else if (boxObj.GetComponentInChildren<SpriteRenderer>().color.Equals(new Color(1f, 0f, 0f, 1f)))
            {
                colorUIText.SetText("string y = R;");
                y = "R";
            }
        }
    }
    public void PakeistiSpalva(string color)
    {
        switch (color)
        {
            case "R":
                box.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
                break;
            case "G":
                box.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
                break;
            case "M":
                box.gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(0, 0.5f, 1, 1);
                break;
            case "Z":
                box.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;
                break;
            case "B":
                box.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                break;
        }
    }
    public void PakeistiVerte(int value)
    {
        box.boxUINr.SetText($"{value}");
        box.boxNr = value;
    }
    public void ResetValues()
    {
        if (placeholderUIColorValue != null)
        {
            colorUIText.SetText("string y = ;");
        }
        if(plachelderUIIntValue != null)
        {
            intUIText.SetText("int x = ;");
        }
        x = -1;
        y = "NAN";
        verteArray = new int[0];
        spalvaArray = new string[0];
        formsArray = new string[0];
        iForLoopValue = -1;
    }
    public void Pakeistiforma(string newSprite)
    {
        switch (newSprite)
        {
            case "L":
                box.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = formSprites[0];
                break;
            case "T":
                box.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = formSprites[1];
                break;
            case "U":
                box.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = formSprites[2];
                break;
        }
    }
    public void PasuktiDetale(float kampas)
    {
        box.transform.eulerAngles = new Vector3(0, 0, kampas);
        var transforms = box.transform.GetComponentsInChildren<Transform>();
        foreach (var item in transforms)
        {
            if (item.CompareTag("Finish"))
            {
                item.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, -kampas);
            }
        }

        box.finalRotation = kampas;
    }
}
