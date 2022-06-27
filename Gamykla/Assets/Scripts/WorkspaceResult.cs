using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorkspaceResult : MonoBehaviour
{
    GameObject[] childrenArray;
    ApplyResult applyResult;

    private void OrderWorkspaceElements()
    {
        applyResult = transform.root.GetChild(0).GetComponent<ApplyResult>();

        var list = new List<GameObject>();

        foreach (Transform child in transform)
        {
            list.Add(child.gameObject);
        }
        childrenArray = list.ToArray();
        childrenArray = childrenArray.OrderByDescending(go => go.transform.position.y).ToArray();
    }
    public void CalculateWorkspaceResult()
    {
        OrderWorkspaceElements();

        for (int i = 0; i < childrenArray.Length; i++)
        {
            switch (childrenArray[i].tag)
            {
                #region Variables
                case "int":
                    if (applyResult.plachelderUIIntValue != null && childrenArray[i].GetComponent<ElementScript>().stringValue.Equals(applyResult.plachelderUIIntValue.transform.parent.name))
                    {
                        string message = "KLAIDA: Kintamasis pavadinimu \"X\" jau sukurtas";
                        GameManager.instance.DisplayErrorMessage(transform, message);
                        break;
                    }
                    else
                    {
                        if (applyResult.x != -1)
                        {
                            string message = "KLAIDA: Kintamasis pavadinimu \"X\" jau sukurtas";
                            GameManager.instance.DisplayErrorMessage(transform, message);
                            return;
                        }
                        else
                        {
                            applyResult.x = childrenArray[i].GetComponent<ElementScript>().elementIntValue;
                        }
                    }
                    break;
                case "color":
                    if (applyResult.placeholderUIColorValue != null && childrenArray[i].GetComponent<ElementScript>().stringValue.Equals(applyResult.placeholderUIColorValue.transform.parent.name))
                    {
                        break;
                    }
                    else
                    {
                        if (applyResult.y != "NAN")
                        {
                            string message = "KLAIDA: Kintamasis pavadinimu \"Y\" jau sukurtas";
                            GameManager.instance.DisplayErrorMessage(transform, message);
                            return;
                        }
                        else
                        {
                            applyResult.y = childrenArray[i].GetComponent<ElementScript>().elementColorValue;
                        }
                    }
                    break;
                #endregion
                #region Set
                case "setInt":
                    if (!applyResult.x.Equals(-1))
                    {
                        if (childrenArray[i].name.StartsWith("new"))
                        {
                            applyResult.x = childrenArray[i].GetComponent<ElementScript>().elementIntValue;
                        }
                        else if (childrenArray[i].name.StartsWith("plusplus"))
                        {
                            applyResult.x++;
                        }
                        else if (childrenArray[i].name.StartsWith("itox"))
                        {
                            applyResult.x = applyResult.iForLoopValue;
                        }
                    }
                    else
                    {
                        string message = "KLAIDA: Kintamasis pavadinimu \"X\" nėra sukurtas";
                        GameManager.instance.DisplayErrorMessage(transform, message);
                        return;
                    }
                    break;
                case "setColor":
                    if (!applyResult.y.Equals("NAN"))
                    {
                        if (childrenArray[i].name.StartsWith("set"))
                        {
                            applyResult.y = childrenArray[i].GetComponent<ElementScript>().elementColorValue;
                        }
                    }
                    else
                    {
                        string message = "KLAIDA: Kintamasis pavadinimu \"Y\" nėra sukurtas";
                        GameManager.instance.DisplayErrorMessage(transform, message);
                        return;
                    }
                    break;
                #endregion
                #region IF Functions
                case "if":
                    if (childrenArray[i].name.Contains("Less"))
                    {
                        if(applyResult.x != -1)
                        {
                            if (applyResult.x < childrenArray[i].GetComponent<ElementScript>().elementIntValue)
                            {
                                childrenArray[i].transform.GetChild(3).GetComponent<WorkspaceResult>().CalculateWorkspaceResult();
                            }
                        }
                        else
                        {
                            string message = $"KLAIDA: Operacija \"if(x < {childrenArray[i].GetComponent<ElementScript>().elementIntValue})\" negalima, kintamasis pavadinimu \"X\" nėra sukurtas";
                            GameManager.instance.DisplayErrorMessage(transform, message);
                            return;
                        }
                    }
                    else if (childrenArray[i].name.Contains("More"))
                    {
                        if (applyResult.x != -1)
                        {
                            if (applyResult.x > childrenArray[i].GetComponent<ElementScript>().elementIntValue)
                            {
                                childrenArray[i].transform.GetChild(3).GetComponent<WorkspaceResult>().CalculateWorkspaceResult();
                            }
                        }
                        else
                        {
                            string message = $"KLAIDA: Operacija \"if(x > {childrenArray[i].GetComponent<ElementScript>().elementIntValue})\" negalima, kintamasis pavadinimu \"X\" nėra sukurtas";
                            GameManager.instance.DisplayErrorMessage(transform, message);
                            return;
                        }
                    }
                    else if (childrenArray[i].name.Contains("MoreLess"))
                    {
                        if (applyResult.x != -1)
                        {
                            if (applyResult.x >= childrenArray[i].GetComponent<ElementScript>().elementIntValue)
                            {
                                childrenArray[i].transform.GetChild(3).GetComponent<WorkspaceResult>().CalculateWorkspaceResult();
                            }
                        }
                        else
                        {
                            string message = $"KLAIDA: Operacija \"if(x >= {childrenArray[i].GetComponent<ElementScript>().elementIntValue})\" negalima, kintamasis pavadinimu \"X\" nėra sukurtas";
                            GameManager.instance.DisplayErrorMessage(transform, message);
                            return;
                        }
                    }
                    else if (childrenArray[i].name.StartsWith("xEquals"))
                    {
                        if (applyResult.x != -1)
                        {
                            if (applyResult.x == childrenArray[i].GetComponent<ElementScript>().elementIntValue)
                            {
                                childrenArray[i].transform.GetChild(3).GetComponent<WorkspaceResult>().CalculateWorkspaceResult();
                            }
                        }
                        else
                        {
                            string message = $"KLAIDA: Operacija \"if(x == {childrenArray[i].GetComponent<ElementScript>().elementIntValue})\" negalima, kintamasis pavadinimu \"X\" nėra sukurtas";
                            GameManager.instance.DisplayErrorMessage(transform, message);
                            return;
                        }
                    }
                    else if (childrenArray[i].name.StartsWith("xNotEqual"))
                    {
                        if (applyResult.x != -1)
                        {
                            if (applyResult.x != childrenArray[i].GetComponent<ElementScript>().elementIntValue)
                            {
                                childrenArray[i].transform.GetChild(3).GetComponent<WorkspaceResult>().CalculateWorkspaceResult();
                            }
                        }
                        else
                        {
                            string message = $"KLAIDA: Operacija \"if(x != {childrenArray[i].GetComponent<ElementScript>().elementIntValue})\" negalima, kintamasis pavadinimu \"X\" nėra sukurtas";
                            GameManager.instance.DisplayErrorMessage(transform, message);
                            return;
                        }
                    }
                    else if (childrenArray[i].name.StartsWith("xPlus2"))
                    {
                        if (applyResult.x != -1)
                        {
                            if (applyResult.x + 2 < childrenArray[i].GetComponent<ElementScript>().elementIntValue)
                            {
                                childrenArray[i].transform.GetChild(3).GetComponent<WorkspaceResult>().CalculateWorkspaceResult();
                            }
                        }
                        else
                        {
                            string message = $"KLAIDA: Operacija \"if(x + 2 < {childrenArray[i].GetComponent<ElementScript>().elementIntValue})\" negalima, kintamasis pavadinimu \"X\" nėra sukurtas";
                            GameManager.instance.DisplayErrorMessage(transform, message);
                            return;
                        }
                    }
                    else if (childrenArray[i].name.StartsWith("iEquals"))
                    {
                        if (applyResult.iForLoopValue != -1)
                        {
                            if (applyResult.iForLoopValue.Equals(childrenArray[i].GetComponent<ElementScript>().elementIntValue))
                            {
                                childrenArray[i].transform.GetChild(3).GetComponent<WorkspaceResult>().CalculateWorkspaceResult();
                            }
                        }
                        else
                        {
                            string message = $"KLAIDA: Operacija \"if(i == {childrenArray[i].GetComponent<ElementScript>().elementIntValue})\" negalima, kintamasis pavadinimu \"i\" nėra sukurtas";
                            GameManager.instance.DisplayErrorMessage(transform, message);
                            return;
                        }
                    }
                    else if (childrenArray[i].name.StartsWith("is"))
                    {
                        if (!applyResult.y.Equals("NAN"))
                        {
                            if (applyResult.y.Equals(childrenArray[i].GetComponent<ElementScript>().elementColorValue))
                            {
                                childrenArray[i].transform.GetChild(3).GetComponent<WorkspaceResult>().CalculateWorkspaceResult();
                            }
                        }
                        else
                        {
                            string message = $"KLAIDA: Operacija \"if(y == {childrenArray[i].GetComponent<ElementScript>().elementColorValue})\" negalima, kintamasis pavadinimu \"Y\" nėra sukurtas";
                            GameManager.instance.DisplayErrorMessage(transform, message);
                            return;
                        }
                    }
                    else if (childrenArray[i].name.StartsWith("not"))
                    {
                        if (!applyResult.y.Equals("NAN"))
                        {
                            if (!applyResult.y.Equals(childrenArray[i].GetComponent<ElementScript>().elementColorValue))
                            {
                                childrenArray[i].transform.GetChild(3).GetComponent<WorkspaceResult>().CalculateWorkspaceResult();
                            }
                        }
                        else
                        {
                            string message = $"KLAIDA: Operacija \"if(y != {childrenArray[i].GetComponent<ElementScript>().elementColorValue})\" negalima, kintamasis pavadinimu \"Y\" nėra sukurtas";
                            GameManager.instance.DisplayErrorMessage(transform, message);
                            return;
                        }
                    }
                    else if (childrenArray[i].name.Contains("compareForValue"))
                    {
                        if (!applyResult.iForLoopValue.Equals(-1))
                        {
                            if (applyResult.iForLoopValue.Equals(childrenArray[i].GetComponent<ElementScript>().elementIntValue))
                            {
                                childrenArray[i].transform.GetChild(3).GetComponent<WorkspaceResult>().CalculateWorkspaceResult();
                            }
                        }
                        else
                        {
                            string message = "KLAIDA: Operacija FOR negalima, kintamasis pavadinimu \"i\" nėra sukurtas";
                            GameManager.instance.DisplayErrorMessage(transform, message);
                            return;
                        }
                    }
                    break;
                #endregion
                #region Void methods
                case "void":
                    if (childrenArray[i].name.Contains("spalvaY"))
                    {
                        if (!applyResult.y.Equals("NAN"))
                        {
                            applyResult.PakeistiSpalva(applyResult.y);
                        }
                        else
                        {
                            string message = "KLAIDA: Operacija void PakeistiSpalvą(y) negalima, kintamasis pavadinimu \"Y\" nėra sukurtas";
                            GameManager.instance.DisplayErrorMessage(transform, message);
                            return;
                        }
                    }
                    else if (childrenArray[i].name.Contains("valueX"))
                    {
                        if (applyResult.x != -1)
                        {
                            applyResult.PakeistiVerte(applyResult.x);
                        }
                        else
                        {
                            string message = "KLAIDA: Operacija void PakeistiVertę(x) negalima, kintamasis pavadinimu \"X\" nėra sukurtas";
                            GameManager.instance.DisplayErrorMessage(transform, message);
                            return;
                        }
                    }
                    else if (childrenArray[i].name.Contains("spalvaArrayX"))
                    {
                        if (applyResult.x != -1 && applyResult.spalvaArray.Length != 0)
                        {
                            if (applyResult.x < applyResult.spalvaArray.Length)
                            {
                                applyResult.PakeistiSpalva(applyResult.spalvaArray[applyResult.x]);
                            }
                            else
                            {
                                string message = "KLAIDA: Operacija PakeistiSpalvą(spalva[x]) negalima. Nurodyta X reikšmė viršyja masyvo \"spalva\" ilgį";
                                GameManager.instance.DisplayErrorMessage(transform, message);
                                return;
                            }
                        }
                        else
                        {
                            if (applyResult.spalvaArray.Length == 0)
                            {
                                string message = "KLAIDA: Operacija PakeistiSpalvą(spalva[x]) negalima, masyvas pavadinimu \"spalva\" nėra sukurtas";
                                GameManager.instance.DisplayErrorMessage(transform, message);
                                return;
                            }
                            else if (applyResult.x == -1)
                            {
                                string message = "KLAIDA: Operacija PakeistiSpalvą(spalva[x]) negalima, kintamasis pavadinimu \"X\" nėra sukurtas";
                                GameManager.instance.DisplayErrorMessage(transform, message);
                                return;
                            }
                        }
                    }
                    else if (childrenArray[i].name.Contains("valueArrayX"))
                    {
                        if (applyResult.x != -1 && applyResult.verteArray.Length != 0)
                        {
                            if (applyResult.x < applyResult.verteArray.Length)
                            {
                                applyResult.PakeistiVerte(applyResult.verteArray[applyResult.x]);
                            }
                            else
                            {
                                string message = "KLAIDA: Operacija PakeistiVertę(sk[x]) negalima. Nurodyta X reikšmė viršyja masyvo \"sk\" ilgį";
                                GameManager.instance.DisplayErrorMessage(transform, message);
                                return;
                            }
                        }
                        else
                        {
                            if (applyResult.verteArray.Length == 0)
                            {
                                string message = "KLAIDA: Operacija PakeistiSpalvą(sk[x]) negalima, masyvas pavadinimu \"sk\" nėra sukurtas";
                                GameManager.instance.DisplayErrorMessage(transform, message);
                                return;
                            }
                            else if (applyResult.x == -1)
                            {
                                string message = "KLAIDA: Operacija PakeistiVertę(sk[x]) negalima, kintamasis pavadinimu \"X\" nėra sukurtas";
                                GameManager.instance.DisplayErrorMessage(transform, message);
                                return;
                            }
                        }
                    }
                    else if (childrenArray[i].name.Contains("spalvaI"))
                    {
                        if (applyResult.spalvaArray.Length != 0 && applyResult.iForLoopValue != -1)
                        {
                            if(applyResult.iForLoopValue < applyResult.spalvaArray.Length)
                            {
                                applyResult.PakeistiSpalva(applyResult.spalvaArray[applyResult.iForLoopValue]);
                            }
                            else
                            {
                                string message = "KLAIDA: Operacija PakeistiSpalva(spalva[i]) negalima. Nurodyta i reikšmė viršyja masyvo \"spalva\" ilgį";
                                GameManager.instance.DisplayErrorMessage(transform, message);
                                return;
                            }
                        }
                        else
                        {
                            if (applyResult.spalvaArray.Length == 0)
                            {
                                string message = "KLAIDA: Operacija PakeistiSpalvą(spalva[i]) negalima, masyvas pavadinimu \"spalva\" nėra sukurtas";
                                GameManager.instance.DisplayErrorMessage(transform, message);
                                return;
                            }
                            else if (applyResult.iForLoopValue == -1)
                            {
                                string message = "KLAIDA: Operacija PakeistiVertę(spalva[i]) negalima, kintamasis pavadinimu \"i\" nėra sukurtas";
                                GameManager.instance.DisplayErrorMessage(transform, message);
                                return;
                            }

                        }
                    }
                    else if (childrenArray[i].name.Contains("valueI"))
                    {
                        if (applyResult.verteArray.Length != 0 && applyResult.iForLoopValue != -1)
                        {
                            applyResult.PakeistiVerte(applyResult.verteArray[applyResult.iForLoopValue]);
                        }
                        else
                        {
                            if (applyResult.verteArray.Length == 0)
                            {
                                string message = "KLAIDA: Operacija PakeistiVertę(sk[i]) negalima, masyvas pavadinimu \"sk\" nėra sukurtas";
                                GameManager.instance.DisplayErrorMessage(transform, message);
                                return;
                            }
                            else if (applyResult.iForLoopValue == -1)
                            {
                                string message = "KLAIDA: Operacija PakeistiVertę(sk[i]) negalima, kintamasis pavadinimu \"i\" nėra sukurtas";
                                GameManager.instance.DisplayErrorMessage(transform, message);
                                return;
                            }
                        }
                    }
                    else if (childrenArray[i].name.Contains("PakeisitFormaArrayX"))
                    {
                        if (applyResult.x != -1 && applyResult.formsArray.Length != 0)
                        {
                            if(applyResult.x < applyResult.formsArray.Length)
                            {
                                applyResult.Pakeistiforma(applyResult.formsArray[applyResult.x]);
                            }
                            else
                            {
                                string message = "KLAIDA: Operacija PakeistiFormą(forma[x]) negalima. Nurodyta X reikšmė viršyja masyvo \"forma\" ilgį";
                                GameManager.instance.DisplayErrorMessage(transform, message);
                                return;
                            }
                        }
                        else
                        {
                            if (applyResult.x == -1)
                            {
                                string message = "KLAIDA: Operacija PakeistiFormą(forma[x]) negalima, kintamasis pavadinimu \"X\" nėra sukurtas";
                                GameManager.instance.DisplayErrorMessage(transform, message);
                                return;
                            }
                            else if (applyResult.formsArray.Length == 0)
                            {
                                string message = "KLAIDA: Operacija PakeistiFormą(forma[x]) negalima, masyvas pavadinimu \"forma\" nėra sukurtas";
                                GameManager.instance.DisplayErrorMessage(transform, message);
                                return;
                            }
                        }
                    }
                    else if (childrenArray[i].name.Contains("pasuktiDetaleX"))
                    {
                        if (applyResult.x != -1)
                        {
                            applyResult.PasuktiDetale(applyResult.x);
                        }
                        else
                        {
                            string message = "KLAIDA: Operacija PasuktiDetalę(x) negalima, kintamasis pavadinimu \"X\" nėra sukurtas";
                            GameManager.instance.DisplayErrorMessage(transform, message);
                            return;
                        }
                    }
                    break;
                #endregion
                #region Arrays
                case "array":
                    if (childrenArray[i].name.Contains("IntArray"))
                    {
                        if (applyResult.verteArray.Length == 0)
                        {
                            applyResult.verteArray = childrenArray[i].GetComponent<ElementScript>().valueArray;
                        }
                        else
                        {
                            string message = "KLAIDA: Masyvas pavadinimu \"sk\" jau sukurtas";
                            GameManager.instance.DisplayErrorMessage(transform, message);
                            return;
                        }
                    }
                    else if (childrenArray[i].name.Contains("colorArray"))
                    {
                        if (applyResult.spalvaArray.Length == 0)
                        {
                            applyResult.spalvaArray = childrenArray[i].GetComponent<ElementScript>().colorArray;
                        }
                        else
                        {
                            string message = "KLAIDA: Masyvas pavadinimu \"spalva\" jau sukurtas";
                            GameManager.instance.DisplayErrorMessage(transform, message);
                            return;
                        }
                        
                    }
                    else if (childrenArray[i].name.Contains("formArray"))
                    {
                        if (applyResult.formsArray.Length == 0)
                        {
                            applyResult.formsArray = childrenArray[i].GetComponent<ElementScript>().formosArray;
                        }
                        else
                        {
                            string message = "KLAIDA: Masyvas pavadinimu \"forma\" jau sukurtas";
                            GameManager.instance.DisplayErrorMessage(transform, message);
                            return;
                        }
                    }
                    break;
                #endregion
                #region For Functions
                case "for":
                    if (childrenArray[i].name.Contains("ForColors"))
                    {
                        if (applyResult.spalvaArray.Length == 0)
                        {
                            string message = "KLAIDA: Operacija FOR negalima, masyvas pavadinimu \"spalva\" nėra sukurtas";
                            GameManager.instance.DisplayErrorMessage(transform, message);
                            return;
                        }
                        else
                        {
                            for (int k = 0; k < applyResult.spalvaArray.Length; k++)
                            {
                                applyResult.iForLoopValue = k;
                                childrenArray[i].transform.GetChild(3).GetComponent<WorkspaceResult>().CalculateWorkspaceResult();
                            }
                        }
                        
                    }
                    else if (childrenArray[i].name.Contains("ForInts"))
                    {
                        if (applyResult.verteArray.Length == 0)
                        {
                            string message = "KLAIDA: Operacija FOR negalima, masyvas pavadinimu \"sk\" nėra sukurtas";
                            GameManager.instance.DisplayErrorMessage(transform, message);
                            return;
                        }
                        else
                        {
                            for (int k = 0; k < applyResult.verteArray.Length; k++)
                            {
                                applyResult.iForLoopValue = k;
                                childrenArray[i].transform.GetChild(3).GetComponent<WorkspaceResult>().CalculateWorkspaceResult();
                            }
                        }
                    }
                    else if (childrenArray[i].name.Contains("ForForms"))
                    {
                        if (applyResult.formsArray.Length == 0)
                        {
                            string message = "KLAIDA: Operacija FOR negalima, masyvas pavadinimu \"forma\" nėra sukurtas";
                            GameManager.instance.DisplayErrorMessage(transform, message);
                            return;
                        }
                        else
                        {
                            for (int k = 0; k < applyResult.formsArray.Length; k++)
                            {
                                applyResult.iForLoopValue = k;
                                childrenArray[i].transform.GetChild(3).GetComponent<WorkspaceResult>().CalculateWorkspaceResult();
                            }
                        }
                    }
                    break;
                    #endregion
            }
        }
    }
}
