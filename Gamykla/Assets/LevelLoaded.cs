using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoaded : MonoBehaviour
{
    public GameObject tutorial;

    public GameObject[] objects;
    List<Transform> children = new List<Transform>();

    private void Start()
    {
        foreach (Transform item in transform)
        {
            children.Add(item);
        }

        foreach (var item in objects)
        {
            item.SetActive(false);
        }
    }
    public void EnableUIAndButtons()
    {
        if(tutorial != null)
        {
            tutorial.SetActive(true);
            GameManager.instance.inputEnabled = false;
        }

        foreach (var item in objects)
        {
            item.SetActive(true);
            transform.DetachChildren();
        }

        var buttons = Resources.FindObjectsOfTypeAll<Button>();

        foreach (var btn in buttons)
        {
            btn.onClick.AddListener(delegate { FindObjectOfType<AudioManager>().Play("Click"); });
        }
    }
    public void CollectChildrenAndExit()
    {
        foreach (var item in objects)
        {
            item.SetActive(false);
        }
        foreach (var item in children)
        {
            item.SetParent(transform);
        }
        GetComponent<Animator>().Play("ExitLevel");
    }
    public void LoadNewScene()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 10)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
