using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Networking;

public class AnalitycsTest : MonoBehaviour
{
    public string CompanyName, QuienesDescargaronFolletos;

    private void Start()
    {
        CompanyName = this.gameObject.name.Replace(".", "_");
        CompanyName = CompanyName.Replace(" ", "_");
        CompanyName = CompanyName.Replace("(", "_");
        CompanyName = CompanyName.Replace(")", "_");
        QuienesDescargaronFolletos = UserInfo.UserName + " | " + UserInfo.Name + " | " + UserInfo.Empresa + " | " + UserInfo.Email; 
    }
    // Start is called before the first frame update

    #region Visitas
    public void VisitasAlStand() {
        StartCoroutine(VisitasAlStandPhP(CompanyName));
        StartCoroutine(ClickFolletoDatosPhP(CompanyName, QuienesDescargaronFolletos));
    }

    public IEnumerator VisitasAlStandPhP(string CompanyName)
    {
        WWWForm form = new WWWForm();
        form.AddField("CompanyName", CompanyName);
        form.AddField("VisitasAlStand", 1);

        using (UnityWebRequest www = UnityWebRequest.Post("https://expovirtual.com.ar/VirtualExpo/AnalyticsTablaPorEmpresa.php", form))
        {
            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
        
        using (UnityWebRequest www = UnityWebRequest.Post("https://expovirtual.com.ar/VirtualExpo/Analytics.php", form))
        {
            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
    #endregion

    #region Folletos
    public void ClickFolleto()
    {
        StartCoroutine(ClickFolletoPhP(CompanyName));
    }

    public IEnumerator ClickFolletoPhP(string CompanyName) {

        WWWForm form = new WWWForm();
        form.AddField("CompanyName", CompanyName);
        form.AddField("Folletos", 1);

        using (UnityWebRequest www = UnityWebRequest.Post("https://expovirtual.com.ar/VirtualExpo/AnalyticsTablaPorEmpresa.php", form))
        {
            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
//                Debug.Log(www.downloadHandler.text);
            }
        }

        using (UnityWebRequest www = UnityWebRequest.Post("https://expovirtual.com.ar/VirtualExpo/Analytics.php", form))
        {
            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
//                Debug.Log(www.downloadHandler.text);
            }
        }
    }
    #endregion

    #region Web
    public void ClickPaginaWeb()
    {
        StartCoroutine(ClickWebPhP(CompanyName));
    }

    public IEnumerator ClickWebPhP(string CompanyName)
    {
        WWWForm form = new WWWForm();
        form.AddField("CompanyName", CompanyName);
        form.AddField("Web", 1);

        using (UnityWebRequest www = UnityWebRequest.Post("https://expovirtual.com.ar/VirtualExpo/AnalyticsTablaPorEmpresa.php", form))
        {
            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
 //               Debug.Log(www.downloadHandler.text);
            }
        }

        using (UnityWebRequest www = UnityWebRequest.Post("https://expovirtual.com.ar/VirtualExpo/Analytics.php", form))
        {
            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
//                Debug.Log(www.downloadHandler.text);
            }
        }
    }
    #endregion

    #region Consultas
    public void ClickConsultas()
    {
        StartCoroutine(ClickConsultasPhP(CompanyName));
    }

    public IEnumerator ClickConsultasPhP(string CompanyName)
    {
        WWWForm form = new WWWForm();
        form.AddField("CompanyName", CompanyName);
        form.AddField("Consultas", 1);

        using (UnityWebRequest www = UnityWebRequest.Post("https://expovirtual.com.ar/VirtualExpo/AnalyticsTablaPorEmpresa.php", form))
        {
            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
//                Debug.Log(www.downloadHandler.text);
            }
        }

        using (UnityWebRequest www = UnityWebRequest.Post("https://expovirtual.com.ar/VirtualExpo/Analytics.php", form))
        {
            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
//                Debug.Log(www.downloadHandler.text);
            }
        }
    }
    #endregion

    #region Chats
    public void ChatsIniciados(string companyName)
    {
        StartCoroutine(ChatsIniciadosPhP(companyName));
    }

    public IEnumerator ChatsIniciadosPhP(string companyName)
    {
        companyName = companyName.Replace(".", "_");
        companyName = companyName.Replace(" ", "_");
        companyName = companyName.Replace("(", "_");
        companyName = companyName.Replace(")", "_");
        WWWForm form = new WWWForm();
        form.AddField("CompanyName", companyName);
        form.AddField("ChatsIniciados", 1);

        using (UnityWebRequest www = UnityWebRequest.Post("https://expovirtual.com.ar/VirtualExpo/AnalyticsTablaPorEmpresa.php", form))
        {
            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }

        using (UnityWebRequest www = UnityWebRequest.Post("https://expovirtual.com.ar/VirtualExpo/Analytics.php", form))
        {
            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                
            }
        }
    }
    #endregion

    #region Videos
    public void ClickVideos()
    {
        StartCoroutine(ClickVideosPhP(CompanyName));
    }

    public IEnumerator ClickVideosPhP(string CompanyName)
    {
        WWWForm form = new WWWForm();
        form.AddField("CompanyName", CompanyName);
        form.AddField("Videos", 1);

        using (UnityWebRequest www = UnityWebRequest.Post("https://expovirtual.com.ar/VirtualExpo/AnalyticsTablaPorEmpresa.php", form))
        {
            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
            }
        }

        using (UnityWebRequest www = UnityWebRequest.Post("https://expovirtual.com.ar/VirtualExpo/Analytics.php", form))
        {
            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
            }
        }
    }
    #endregion
    public IEnumerator ClickFolletoDatosPhP(string CompanyName, string QuienesDescargaronFolletos)
    {

        WWWForm form = new WWWForm();
        form.AddField("CompanyName", CompanyName);
        form.AddField("QuienesDescargaronFolletos", QuienesDescargaronFolletos);

        using (UnityWebRequest www = UnityWebRequest.Post("https://expovirtual.com.ar/VirtualExpo/Analytics.php", form))
        {
            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
            }
        }
    }
}

