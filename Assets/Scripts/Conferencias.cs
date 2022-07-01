using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Conferencias : MonoBehaviour
{
    public string Sala;
    public TMP_Text NombreConf, Hora, Online;
    public Color[] colors;
    
    private void Start()
    {
        StartCoroutine(ConferenciasData());
    }
    IEnumerator ConferenciasData()
    {
        WWWForm form = new WWWForm();
        form.AddField("Sala", Sala);
        using (UnityWebRequest www = UnityWebRequest.Post("https://expovirtual.com.ar/VirtualExpo/ConferenciasFeria.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.downloadHandler.text == "No hay conferencias programadas para hoy.")
                {
                    NombreConf.text = www.downloadHandler.text.Split('|')[0];
                    Online.text = "Off line";
                    Online.color = colors[1];
                    this.gameObject.GetComponent<Button>().interactable = false;
                }
                else
                {
                    NombreConf.text = www.downloadHandler.text.Split('|')[1] + " | " + www.downloadHandler.text.Split('|')[0];
                    Online.text = "On line";
                    Online.color = colors[0];
                    this.gameObject.GetComponent<Button>().interactable = true;
                }
                yield return new WaitForSeconds(60);
                StartCoroutine(ConferenciasData());
            }
        }
    }
}
