using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class ResultadosBusquedaScript : MonoBehaviour
{
    public string nombreEmpresa, Rubro, Subrubro, Descripcion;
    public TMP_Text NombreEmpresaTxt, RubroTxt, DescripcionTxt;
    public MinimapScript mapScript;

    /*void OnEnable()
    {
        if (nombreEmpresa != "")
            NombreEmpresaTxt.text = nombreEmpresa;
        else if (nombreEmpresa == "")
            NombreEmpresaTxt.text = this.gameObject.name;
        RubroTxt.text = Rubro;
        DescripcionTxt.text = Descripcion;
    }*/

    public void SetData(string nombreEmpresa,string rubro, string descripcion, string numerodestand) {
        NombreEmpresaTxt.text = nombreEmpresa;
        RubroTxt.text = rubro;
        DescripcionTxt.text = descripcion;
        this.gameObject.GetComponent<Button>().onClick.AddListener(() => mapScript.GoToStand(numerodestand));
    }
}
