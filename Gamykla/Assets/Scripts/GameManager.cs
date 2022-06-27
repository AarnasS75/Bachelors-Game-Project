using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject errorMessage;

    public GameObject resultsMenu;
    public GameObject resultsDarkBG;
    TextMeshProUGUI efektyvumasText;

    GameObject[] spawners;
    GameObject[] results;
    GameObject[] mainWorkspaces;

    public Sprite stopImage;
    public Sprite startImage;
    public Button button;
    int clickCount = 0;

    [HideInInspector]
    public bool inputEnabled = true;

    int x = 0;
    public int t = 0;
    int p = 0;
    [HideInInspector]
    public int boxesEvaluated = 0;

    int totalBoxCount = 0;

    int correctCount = 0;
    float E = 0;

    [HideInInspector]
    public bool levelEnded = false;
    [HideInInspector]
    public bool tutorialEnded;

    private void Awake()
    {
        resultsMenu.SetActive(false);
        efektyvumasText = resultsMenu.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        if(instance == null)
        {
            instance = this;
        }
        spawners = GameObject.FindGameObjectsWithTag("Spawner");
        results = GameObject.FindGameObjectsWithTag("Result");
        mainWorkspaces = GameObject.FindGameObjectsWithTag("Main Workspace");
        foreach (var item in mainWorkspaces)
        {
            x += Mathf.FloorToInt(item.GetComponent<BoxCollider2D>().size.y / 0.6f);
        }
        foreach (var item in spawners)
        {
            totalBoxCount += item.GetComponent<SpawnBoxes>().maxCount;
        }
    }
    private void Start()
    {
        var tutorial = GameObject.FindGameObjectWithTag("Tutorial");
        if (tutorial != null)
        {
            tutorialEnded = false;
        }
        else
        {
            tutorialEnded = true;
        }
    }
    private void Update()
    {
        if (!levelEnded)
        {
            if (boxesEvaluated == totalBoxCount)
            {
                foreach (var resultMAchine in results)
                {
                    correctCount += resultMAchine.GetComponent<EvaluateResult>().correctResultCount;
                }
                EndLevel();
            }
        }
    }
    public void EndTutorialAndEnableInput(bool condition)
    {
        tutorialEnded = condition;
        inputEnabled = condition;
    }
    void EndLevel()
    {
        CalculateEfficiency();
        OpenResultsMenu();
        levelEnded = true;
    }

    void OpenResultsMenu()
    {
        resultsMenu.SetActive(true);
        resultsDarkBG.SetActive(true);
        inputEnabled = false;

        FindObjectOfType<AudioManager>().Play("LevelCompleted");

        if (correctCount == boxesEvaluated)
        {
            
            efektyvumasText.SetText($"Kodo efektyvumas: {Mathf.FloorToInt(E)}");

            if (Mathf.FloorToInt(E) >= 10 && Mathf.FloorToInt(E) <= 40)
            {
                resultsMenu.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
                resultsMenu.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
                resultsMenu.transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(false);
            }
            else if (Mathf.FloorToInt(E) > 40 && Mathf.FloorToInt(E) <= 80)
            {
                resultsMenu.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
                resultsMenu.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);
                resultsMenu.transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(false);
            }
            else if (Mathf.FloorToInt(E) > 80)
            {
                resultsMenu.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
                resultsMenu.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);
                resultsMenu.transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(true);
            }
        }
        else
        {
            resultsMenu.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
            resultsMenu.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
            resultsMenu.transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(false);

            efektyvumasText.SetText($"Reikalinga teisingai sukurti detalių: {boxesEvaluated}" + "\n" + $"Teisingai sukurta detalių: {correctCount}");

            resultsMenu.transform.GetChild(3).GetComponent<Button>().interactable = false;
        }

        
    }
    public void CloseResultsMenu()
    {
        resultsMenu.GetComponent<Animator>().Play("ResultsMenuExit");
    }
    public void PlayGame()
    {
        if (tutorialEnded)
        {
            if (clickCount == 0)
            {
                inputEnabled = false;
                button.image.sprite = stopImage;
                foreach (var item in spawners)
                {
                    item.GetComponent<SpawnBoxes>().startLevel = true;
                }
                clickCount = 1;
            }
            else
            {
                inputEnabled = true;
                button.image.sprite = startImage;
                StopGame();
            }
        }
    }
    public void DisplayErrorMessage(Transform workspace, string message, bool autoClose = false)
    {
        FindObjectOfType<AudioManager>().Play("Error");
        var error = Instantiate(errorMessage, new Vector2(workspace.transform.position.x, workspace.transform.position.y), Quaternion.identity, workspace.parent);
        error.GetComponent<TextMeshPro>().SetText(message);

        if (autoClose)
        {
            error.GetComponent<Animator>().Play("ErrorMessage");
            error.GetComponent<Animator>().SetBool("autoClose", true);
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    void CalculateEfficiency()
    {
        int correctCount = 0;

        #region Get All Workspace Children
        foreach (var mainWorkspace in mainWorkspaces)
        {
            if(mainWorkspace.transform.childCount > 0)
            {
                var childrenTransforms = mainWorkspace.GetComponentsInChildren<WorkspaceElement>();
                foreach (var child in childrenTransforms)
                {
                    p++;
                }
            }
        }
        #endregion

        #region Get Correct Count
        foreach (var result in results)
        {
            EvaluateResult evaluateResult = result.GetComponent<EvaluateResult>();
            correctCount += evaluateResult.correctResultCount;
        }
        #endregion

        float e1 = x - t;
        float e2 = p - t;

        E = (e1 - e2) / e1;
        E *= 100;
    }

    void StopGame()
    {
        Time.timeScale = 1;

        correctCount = 0;
        boxesEvaluated = 0;
        clickCount = 0;

        foreach (var item in spawners)
        {
            item.GetComponent<SpawnBoxes>().startLevel = false;
        }

        GameObject[] box;
        box = GameObject.FindGameObjectsWithTag("Box");

        for (int i = 0; i < box.Length; i++)
        {
            Destroy(box[i]);
        }
        foreach (var item in results)
        {
            item.GetComponent<EvaluateResult>().intRenderer.sprite = null;
            item.GetComponent<EvaluateResult>().spalvaRenderer.sprite = null;
            item.GetComponent<EvaluateResult>().shapeRenderer.sprite = null;
            item.GetComponent<EvaluateResult>().rotationRenderer.sprite = null;
            item.GetComponent<EvaluateResult>().correctResultCount = 0;
        }
        foreach (var item in spawners)
        {
            item.GetComponent<SpawnBoxes>().count = 0;
        }
        foreach (var item in mainWorkspaces)
        {
            item.GetComponent<ApplyResult>().ResetValues();
        }

        var errors = GameObject.FindGameObjectsWithTag("ErrorMessage");
        foreach (var item in errors)
        {
            Destroy(item);
        }
    }
}
