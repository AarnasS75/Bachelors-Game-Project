using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform button;

    private void Start()
    {
        button.GetComponent<Animator>().Play("HoverOff");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        FindObjectOfType<AudioManager>().Play("Hover");
        button.GetComponent<Animator>().Play("HoverButton");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        button.GetComponent<Animator>().Play("HoverOff");
    }
}
