using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System.Linq;

public class Busqueda : MonoBehaviour
{
    public GameObject[] BotonesDePabellones, SubRubrosDropdown;
    public Image barraDebajoInput;
    public TMP_Text textoSobreInput;
    public Color32 CeroResultados, Resultados, AntesDeBuscar;
    public TMP_Dropdown dropdown;
    public string RubroABuscar, SubrubroABuscar;
    public int ContadorDeResultados;
    public TMP_InputField InputBusqueda;
    public TMP_Text NumeroDeResultados;
    public GameObject Mapa, ResultadosBusquedaPanel;

    public Transform ContentBusqueda;
    public GameObject PrefabResult;

    private void OnEnable()
    {
        ResultadosBusquedaPanel.SetActive(false);
        barraDebajoInput.color = AntesDeBuscar;
        textoSobreInput.color = AntesDeBuscar;
        NumeroDeResultados.text = "";
        InputBusqueda.text = "";
        dropdown.value = 0;
        ContadorDeResultados = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            BuscarEnMapa();
    }
    public void InputRubro(int value)
    {
        switch (value)
        {
            case 0:
                RubroABuscar = "";
                for (int i = 0; i < 4; i++)
                {
                    SubRubrosDropdown[i].SetActive(false);
                }
                break;
            case 1:
                RubroABuscar = "Envases y embalajes";
                break;
            case 2:
                RubroABuscar = "Máquinas, equipos y accesorios";
                break;
            case 3:
                RubroABuscar = "Materias primas e insumos";
                break;
            case 4:
                RubroABuscar = "Servicios";
                break;
        }
    }

    public void BuscarEnMapa()
    {
        if (InputBusqueda.text != "")
        {
            foreach (Transform child in ContentBusqueda)
                Destroy(child.gameObject);

            string search = InputBusqueda.text;
            StartCoroutine(Search_In_DB(search));
            //BuscarEnMapaPorInput();
        }
        else if (RubroABuscar != "")
        {
            for (int i = 0; i < BotonesDePabellones[4].transform.childCount; i++)
            {
                ResultadosBusquedaScript script = BotonesDePabellones[4].transform.GetChild(i).GetComponent<ResultadosBusquedaScript>();
                if (script != null && RubroABuscar == script.Rubro)
                {
                    BotonesDePabellones[4].transform.GetChild(i).gameObject.SetActive(true);
                    ContadorDeResultados = ContadorDeResultados + 1;
                }
                else if (script.Rubro == null || script.Rubro != RubroABuscar)
                    BotonesDePabellones[4].transform.GetChild(i).gameObject.SetActive(false);
            }
            if (ContadorDeResultados != 1)
                NumeroDeResultados.text = ContadorDeResultados + " resultados de '" + RubroABuscar + "'.";
            else
                NumeroDeResultados.text = ContadorDeResultados + " resultado de '" + RubroABuscar + "'.";
            dropdown.value = 0;
            ContadorDeResultados = 0;
            ResultadosBusquedaPanel.SetActive(true);
        }
    }

    IEnumerator Search_In_DB(string Search) {
        WWWForm form = new WWWForm();
        form.AddField("Search", Search);

        using (UnityWebRequest www = UnityWebRequest.Post("https://teckdes.com/ExpoVirtual/VirtualExpo/SearchInDB.php", form))
        {
            yield return www.SendWebRequest();
            Debug.Log(www.downloadHandler.text);
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                int ContadorResultados = int.Parse(www.downloadHandler.text.Split('|').Last());

                for (int i = 0; i < ContadorResultados; i++)
                {
                    GameObject Result = Instantiate(PrefabResult, ContentBusqueda);
                    ResultadosBusquedaScript script = Result.GetComponent<ResultadosBusquedaScript>();
                    script.SetData(www.downloadHandler.text.Split('|')[i * 4], www.downloadHandler.text.Split('|')[i * 4 + 1], www.downloadHandler.text.Split('|')[i * 4 + 2], www.downloadHandler.text.Split('|')[i * 4 + 3]);
                }

                if (ContadorResultados == 0)
                {
                    NumeroDeResultados.text = "No hay resultados de '" + InputBusqueda.text + "'.";
                    barraDebajoInput.color = CeroResultados;
                    textoSobreInput.color = CeroResultados;
                }
                else if (ContadorResultados != 1)
                {
                    NumeroDeResultados.text = ContadorResultados + " resultados de '" + InputBusqueda.text + "'.";
                    barraDebajoInput.color = Resultados;
                    textoSobreInput.color = Resultados;
                }
                else
                {
                    NumeroDeResultados.text = ContadorResultados + " resultado de '" + InputBusqueda.text + "'.";
                    barraDebajoInput.color = Resultados;
                    textoSobreInput.color = Resultados;
                }
                ResultadosBusquedaPanel.SetActive(true);
                InputBusqueda.text = "";
                dropdown.value = 0;
            }
        }
    }

    public void Cerrar() {
        ResultadosBusquedaPanel.SetActive(false);
    }

}
