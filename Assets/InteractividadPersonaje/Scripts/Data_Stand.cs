using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class Data_Stand : MonoBehaviour
{
    string URLPagina, URLPDF, URLConsulta;
    string NumeroDeStand;
    string NombreEmpresa, Rubro, Ubicacion, Descripcion;
    public AnalitycsTest analytics;

    private void Awake()
    {
        NumeroDeStand = this.transform.parent.name.Substring(0, 4);
    }

    private void Start()
    {
        NombreEmpresa = gameObject.name.Replace("_", " ");
    }

    public void OpenConsulta()
    {
#if UNITY_WEBGL
    Application.ExternalEval("window.open('" + URLConsulta + "' , '_blank')");
#else
        Application.OpenURL(URLConsulta);
#endif
        analytics.ClickConsultas();
    }

    public void OpenPaginaWeb() {
#if UNITY_WEBGL
        Application.ExternalEval("window.open('" + URLPagina + "' , '_blank')");
#else
        Application.OpenURL(URLPagina);
#endif
        analytics.ClickPaginaWeb();
    }

    public void OpenPDF()
    {
#if UNITY_WEBGL
        Application.ExternalEval("window.open('" + URLPDF + "' , '_blank')");
#else
        Application.OpenURL(URLPDF);
#endif
        analytics.ClickFolleto();
    }

    public void Cerrar() {

    }

    public void SetDataToStand(GameObject panelStand)
    {
        panelStand.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = NombreEmpresa;
        panelStand.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Rubro;
        panelStand.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = Descripcion;
        panelStand.SetActive(true);

        panelStand.transform.GetChild(3).gameObject.GetComponent<Button>().onClick.AddListener(OpenPDF);
        panelStand.transform.GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(OpenPaginaWeb);
        panelStand.transform.GetChild(5).gameObject.GetComponent<Button>().onClick.AddListener(OpenConsulta);
        panelStand.transform.GetChild(6).gameObject.GetComponent<Button>().onClick.AddListener(Cerrar);
        CheckLinks(panelStand.transform);
    }

    void CheckLinks(Transform panelStand)
    {
        if (string.IsNullOrEmpty(URLPDF))
            panelStand.GetChild(3).gameObject.GetComponent<Button>().interactable = false;
        else
            panelStand.transform.GetChild(3).gameObject.GetComponent<Button>().interactable = true;
        if (string.IsNullOrEmpty(URLPagina))
            panelStand.GetChild(4).gameObject.GetComponent<Button>().interactable = false;
        else
            panelStand.GetChild(4).gameObject.GetComponent<Button>().interactable = true;
        if (string.IsNullOrEmpty(URLConsulta))
            panelStand.GetChild(5).gameObject.GetComponent<Button>().interactable = false;
        else
            panelStand.GetChild(5).gameObject.GetComponent<Button>().interactable = true;
    }

    public void SetDataDB(GameObject panelStands)
    {
        StartCoroutine(GetDataStandFromDB(NumeroDeStand, panelStands));   
    }

    public IEnumerator GetDataStandFromDB(string NumeroDeStand, GameObject panelStands) {
        WWWForm form = new WWWForm();
        form.AddField("NumeroDeStand", NumeroDeStand);

        using (UnityWebRequest www = UnityWebRequest.Post("https://expovirtual.com.ar/VirtualExpo/GetDataStandFromDB.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Rubro = www.downloadHandler.text.Split('|')[0];
                Ubicacion = www.downloadHandler.text.Split('|')[1];
                Descripcion = www.downloadHandler.text.Split('|')[2];
                URLPagina = www.downloadHandler.text.Split('|')[3];
                URLPDF = www.downloadHandler.text.Split('|')[4];
                URLConsulta = www.downloadHandler.text.Split('|')[5];
                SetDataToStand(panelStands);
            }
        }
    }
}