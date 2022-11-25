using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AnalyticsPabellones : MonoBehaviour
{
    public int thisPabellonActual, PabellonAnterior;

    public void CompracionDePabellonActualYAnterior(int PabellonActual) {
        if (PabellonActual != PabellonAnterior)
        {
            thisPabellonActual = PabellonActual;
            
            PabellonAnterior = PabellonActual;
            StartCoroutine(VisitasAlPabellonPhP());
        }
    }

   public IEnumerator VisitasAlPabellonPhP()
    {
        WWWForm form = new WWWForm();

        form.AddField("VisitasAlPabellon_" + thisPabellonActual.ToString(), 1);

        using (UnityWebRequest www = UnityWebRequest.Post("https://teckdes.com/ExpoVirtual/VirtualExpo/AnalyticsPrincipal.php", form))
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
}
