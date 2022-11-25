using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class Data_Stand : MonoBehaviour
{
    string NumeroDeStand;
    public AnalitycsTest analytics;

    private void Awake()
    {
        NumeroDeStand = this.transform.parent.name.Substring(0, 4);
    }

#region Buttons Panel Stand
    public void OpenConsulta(string url, string nombreEmpresa)
    {
        #if UNITY_WEBGL
        Application.ExternalEval("window.open('" + url + "' , '_blank')");
        #else
        Application.OpenURL(url);
        #endif
        analytics.ClickConsultas(nombreEmpresa);
    }

    public void OpenPaginaWeb(string url, string nombreEmpresa) {
#if UNITY_WEBGL
        Application.ExternalEval("window.open('" + url + "' , '_blank')");
#else
        Application.OpenURL(url);
#endif
        analytics.ClickPaginaWeb(nombreEmpresa);
    }

    public void OpenPDF(string url, string nombreEmpresa)
    {
#if UNITY_WEBGL
        Application.ExternalEval("window.open('" + url + "' , '_blank')");
#else
        Application.OpenURL(url);
#endif
        analytics.ClickFolleto(nombreEmpresa);
    }

    public void Cerrar(GameObject panelStand) {
        panelStand.SetActive(false);
    }
#endregion

#region Get Data from DB
    public void SetDataDB(GameObject panelStand)
    {
        StartCoroutine(GetDataStandFromDB(NumeroDeStand, panelStand));   
    }

    public IEnumerator GetDataStandFromDB(string NumeroDeStand, GameObject panelStand) {
        WWWForm form = new WWWForm();
        form.AddField("NumeroDeStand", NumeroDeStand);

        using (UnityWebRequest www = UnityWebRequest.Post("https://teckdes.com/ExpoVirtual/VirtualExpo/GetDataStandFromDB.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string nombreEmpresa = www.downloadHandler.text.Split('|')[0];
                string rubro = www.downloadHandler.text.Split('|')[1];
                string ubicacion = www.downloadHandler.text.Split('|')[2];
                string descripcion = www.downloadHandler.text.Split('|')[3];
                string urlPagina = www.downloadHandler.text.Split('|')[4];
                string urlPDF = www.downloadHandler.text.Split('|')[5];
                string urlConsulta = www.downloadHandler.text.Split('|')[6];
                SetDataToStand(panelStand, nombreEmpresa, rubro, ubicacion, descripcion, urlPagina, urlPDF, urlConsulta);
            }
        }
    }

    public void SetDataToStand(GameObject panelStand, string nombreEmpresa, string rubro, string ubicacion, string descripcion, string urlPDF, string urlPagina, string urlConsulta)
    {
        panelStand.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = nombreEmpresa;
        panelStand.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = rubro;
        panelStand.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = descripcion;
        panelStand.SetActive(true);

        panelStand.transform.GetChild(3).gameObject.GetComponent<Button>().onClick.AddListener(delegate{OpenPDF(urlPDF, nombreEmpresa);});
        panelStand.transform.GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(delegate{OpenPaginaWeb(urlPagina, nombreEmpresa);});
        panelStand.transform.GetChild(5).gameObject.GetComponent<Button>().onClick.AddListener(delegate{OpenConsulta(urlConsulta, nombreEmpresa);});
        panelStand.transform.GetChild(6).gameObject.GetComponent<Button>().onClick.AddListener(delegate{Cerrar(panelStand);});
        CheckLinks(panelStand.transform, urlPDF, urlPagina, urlConsulta);
    }
#endregion

    void CheckLinks(Transform panelStand, string urlPDF, string urlPagina, string urlConsulta)
    {
        if (string.IsNullOrEmpty(urlPDF))
            panelStand.GetChild(3).gameObject.GetComponent<Button>().interactable = false;
        else
            panelStand.transform.GetChild(3).gameObject.GetComponent<Button>().interactable = true;

        if (string.IsNullOrEmpty(urlPagina))
            panelStand.GetChild(4).gameObject.GetComponent<Button>().interactable = false;
        else
            panelStand.GetChild(4).gameObject.GetComponent<Button>().interactable = true;

        if (string.IsNullOrEmpty(urlConsulta))
            panelStand.GetChild(5).gameObject.GetComponent<Button>().interactable = false;
        else
            panelStand.GetChild(5).gameObject.GetComponent<Button>().interactable = true;
    }
}