using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Data_Stand : MonoBehaviour
{
    [SerializeField] private string NumeroDeStand;
    [SerializeField] private AnalitycsTest analytics;

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


    /*
     Comment:  Llama a la funcion ConnectDB para obtener la informacion de la base de datos el stand, obteniendo nombe, rubro, ubicacion, descripcion,
     y urls para los links.
     Pre: Se ejecuta cuando el usuario clickea sobre el stand designado.
     Post: Obtiene la informacion y la parsea.
    */
    public void GetDataDB(GameObject panelStand)
    {
        WWWForm form = new WWWForm();
        form.AddField("NumeroDeStand", NumeroDeStand);

        StartCoroutine(Main.Instance.ConnectDB("https://teckdes.com/ExpoVirtual/VirtualExpo/GetDataStandFromDB.php", form, (data) => {
            string nombreEmpresa = data.Split('|')[0];
            string rubro = data.Split('|')[1];
            string ubicacion = data.Split('|')[2];
            string descripcion = data.Split('|')[3];
            string urlPagina = data.Split('|')[4];
            string urlPDF = data.Split('|')[5];
            string urlConsulta = data.Split('|')[6];
            Debug.Log("Nombre: "+ nombreEmpresa);
            SetDataToStand(panelStand, nombreEmpresa, rubro, ubicacion, descripcion, urlPagina, urlPDF, urlConsulta);
        }));
    }

    /*
     Comment:  Recibe la informacion sobre el stand y le hace un display sobre el canvas del usuario.
     Pre: GameObject panelStand y strings nombreEmpresa, rubro, ubicacion, descripcion, urls.
     Post: Display en canvas de la informacion.
    */
    public void SetDataToStand(GameObject panelStand, string nombreEmpresa, string rubro, string ubicacion, string descripcion, string urlPDF, string urlPagina, string urlConsulta)
    {
        Transform p_transform = panelStand.transform;
        p_transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = nombreEmpresa;
        p_transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = rubro;
        p_transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = descripcion;
        panelStand.SetActive(true);

        p_transform.GetChild(3).gameObject.GetComponent<Button>().onClick.AddListener(delegate{OpenPDF(urlPDF, nombreEmpresa);});
        p_transform.GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(delegate{OpenPaginaWeb(urlPagina, nombreEmpresa);});
        p_transform.GetChild(5).gameObject.GetComponent<Button>().onClick.AddListener(delegate{OpenConsulta(urlConsulta, nombreEmpresa);});
        p_transform.GetChild(6).gameObject.GetComponent<Button>().onClick.AddListener(delegate{Cerrar(panelStand);});
        
        CheckLinks(p_transform, urlPDF, urlPagina, urlConsulta);
    }


    /*
     Comment:  Comprueba que los links no sean nulos y asi activa o desactiva los botones.
     Pre: Botones y links.
     Post: Desactiva aquellos que botones no posean links.
    */
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