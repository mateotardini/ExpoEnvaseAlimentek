using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnMouseOverBTN : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    
    public GameObject childText = null, irAquiIcon; //  or make public and drag
    public TMP_Text texto, nombreEmpresaTxt, TextBotonesResultados, DescripcionTMP;
    private bool setPosition = true;
    public string textoAMostrar, nombreEmpresa;
    public string Rubro, Subrubro;


    void Start()
    {
        texto.text = "";
        nombreEmpresaTxt.text = "";

        if (TextBotonesResultados != null)
            TextBotonesResultados.text = nombreEmpresa;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        childText.SetActive(true);
        irAquiIcon.SetActive(true);
        if (setPosition)
        {
            childText.transform.position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 105f);
            irAquiIcon.transform.position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 45f);
            setPosition = false;
        }
        nombreEmpresaTxt.text = nombreEmpresa; 
        texto.text = Rubro + "\n" + Subrubro;
        DescripcionTMP.text = textoAMostrar;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        childText.SetActive(false);
        irAquiIcon.SetActive(false);
        setPosition = true;
        nombreEmpresaTxt.text = "";
        texto.text = "";
    }
}

