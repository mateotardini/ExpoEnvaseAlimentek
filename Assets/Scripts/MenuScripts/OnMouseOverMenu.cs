using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class OnMouseOverMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject childText;
    public TMP_Text texto;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x + 60, Input.mousePosition.y);
        childText.transform.position = screenPosition;
        childText.SetActive(true);
        childText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = gameObject.name;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        childText.SetActive(false);
    }
}
