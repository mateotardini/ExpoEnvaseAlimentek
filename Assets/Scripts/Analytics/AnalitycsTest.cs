using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Networking;

public class AnalitycsTest : MonoBehaviour
{
    public string QuienesDescargaronFolletos;

    private void Start()
    {
        QuienesDescargaronFolletos = UserInfo.UserName + " | " + UserInfo.Name + " | " + UserInfo.Empresa + " | " + UserInfo.Email; 
    }

    #region Visitas
    /*public void VisitasAlStand() {
        StartCoroutine(VisitasAlStandPhP(nombreEmpresa));
        StartCoroutine(ClickFolletoDatosPhP(nombreEmpresa, QuienesDescargaronFolletos));
    }*/

    public IEnumerator VisitasAlStandPhP(string nombreEmpresa)
    {
        WWWForm form = new WWWForm();
        form.AddField("CompanyName", nombreEmpresa);
        form.AddField("VisitasAlStand", 1);

        using (UnityWebRequest www = UnityWebRequest.Post("https://teckdes.com/ExpoVirtual/VirtualExpo/AnalyticsTablaPorEmpresa.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else{ Debug.Log(www.downloadHandler.text);}
        }
        
        using (UnityWebRequest www = UnityWebRequest.Post("https://teckdes.com/ExpoVirtual/VirtualExpo/Analytics.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else{ Debug.Log(www.downloadHandler.text);}
        }
    }
    #endregion

    #region Folletos
    public void ClickFolleto(string nombreEmpresa)
    {
        nombreEmpresa = nombreEmpresa.Replace(" ", "_");
        nombreEmpresa = nombreEmpresa.Replace(".", "_");
        nombreEmpresa = nombreEmpresa.Replace("(", "_");
        nombreEmpresa = nombreEmpresa.Replace(")", "_");
        StartCoroutine(ClickFolletoPhP(nombreEmpresa));
    }

    public IEnumerator ClickFolletoPhP(string nombreEmpresa) {

        WWWForm form = new WWWForm();
        form.AddField("CompanyName", nombreEmpresa);
        form.AddField("Folletos", 1);

        using (UnityWebRequest www = UnityWebRequest.Post("https://teckdes.com/ExpoVirtual/VirtualExpo/AnalyticsTablaPorEmpresa.php", form))
        {
            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else{ Debug.Log(www.downloadHandler.text);}
        }

        using (UnityWebRequest www = UnityWebRequest.Post("https://teckdes.com/ExpoVirtual/VirtualExpo/Analytics.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else{ Debug.Log(www.downloadHandler.text);}
        }
    }
    #endregion

    #region Web
    public void ClickPaginaWeb(string nombreEmpresa)
    {
        nombreEmpresa = nombreEmpresa.Replace(" ", "_");
        nombreEmpresa = nombreEmpresa.Replace(".", "_");
        nombreEmpresa = nombreEmpresa.Replace("(", "_");
        nombreEmpresa = nombreEmpresa.Replace(")", "_");
        StartCoroutine(ClickWebPhP(nombreEmpresa));
    }

    public IEnumerator ClickWebPhP(string nombreEmpresa)
    {
        WWWForm form = new WWWForm();
        form.AddField("CompanyName", nombreEmpresa);
        form.AddField("Web", 1);

        using (UnityWebRequest www = UnityWebRequest.Post("https://teckdes.com/ExpoVirtual/VirtualExpo/AnalyticsTablaPorEmpresa.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else{ Debug.Log(www.downloadHandler.text);}
        }

        using (UnityWebRequest www = UnityWebRequest.Post("https://teckdes.com/ExpoVirtual/VirtualExpo/Analytics.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else{ Debug.Log(www.downloadHandler.text);}
        }
    }
    #endregion

    #region Consultas
    public void ClickConsultas(string nombreEmpresa)
    {
        nombreEmpresa = nombreEmpresa.Replace(" ", "_");
        nombreEmpresa = nombreEmpresa.Replace(".", "_");
        nombreEmpresa = nombreEmpresa.Replace("(", "_");
        nombreEmpresa = nombreEmpresa.Replace(")", "_");
        StartCoroutine(ClickConsultasPhP(nombreEmpresa));
    }

    public IEnumerator ClickConsultasPhP(string nombreEmpresa)
    {
        WWWForm form = new WWWForm();
        form.AddField("CompanyName", nombreEmpresa);
        form.AddField("Consultas", 1);

        using (UnityWebRequest www = UnityWebRequest.Post("https://teckdes.com/ExpoVirtual/VirtualExpo/AnalyticsTablaPorEmpresa.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else{ Debug.Log(www.downloadHandler.text);}
        }

        using (UnityWebRequest www = UnityWebRequest.Post("https://teckdes.com/ExpoVirtual/VirtualExpo/Analytics.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else{ Debug.Log(www.downloadHandler.text);}
        }
    }
    #endregion

    #region Chats
    public void ChatsIniciados(string nombreEmpresa)
    {
        nombreEmpresa = nombreEmpresa.Replace(" ", "_");
        nombreEmpresa = nombreEmpresa.Replace(".", "_");
        nombreEmpresa = nombreEmpresa.Replace("(", "_");
        nombreEmpresa = nombreEmpresa.Replace(")", "_");
        StartCoroutine(ChatsIniciadosPhP(nombreEmpresa));
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

        using (UnityWebRequest www = UnityWebRequest.Post("https://teckdes.com/ExpoVirtual/VirtualExpo/AnalyticsTablaPorEmpresa.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else{ Debug.Log(www.downloadHandler.text);}
        }

        using (UnityWebRequest www = UnityWebRequest.Post("https://teckdes.com/ExpoVirtual/VirtualExpo/Analytics.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else{ Debug.Log(www.downloadHandler.text);}
        }
    }
    #endregion

    #region Videos
    public void ClickVideos(string nombreEmpresa)
    {
        nombreEmpresa = nombreEmpresa.Replace(" ", "_");
        nombreEmpresa = nombreEmpresa.Replace(".", "_");
        nombreEmpresa = nombreEmpresa.Replace("(", "_");
        nombreEmpresa = nombreEmpresa.Replace(")", "_");
        StartCoroutine(ClickVideosPhP(nombreEmpresa));
    }

    public IEnumerator ClickVideosPhP(string nombreEmpresa)
    {
        WWWForm form = new WWWForm();
        form.AddField("CompanyName", nombreEmpresa);
        form.AddField("Videos", 1);

        using (UnityWebRequest www = UnityWebRequest.Post("https://teckdes.com/ExpoVirtual/VirtualExpo/AnalyticsTablaPorEmpresa.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else{ Debug.Log(www.downloadHandler.text);}
        }

        using (UnityWebRequest www = UnityWebRequest.Post("https://teckdes.com/ExpoVirtual/VirtualExpo/Analytics.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else{ Debug.Log(www.downloadHandler.text);}
        }
    }
    #endregion
    public IEnumerator ClickFolletoDatosPhP(string nombreEmpresa, string QuienesDescargaronFolletos)
    {
        WWWForm form = new WWWForm();
        form.AddField("CompanyName", nombreEmpresa);
        form.AddField("QuienesDescargaronFolletos", QuienesDescargaronFolletos);

        using (UnityWebRequest www = UnityWebRequest.Post("https://teckdes.com/ExpoVirtual/VirtualExpo/Analytics.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else{ Debug.Log(www.downloadHandler.text);}
        }
    }
}

