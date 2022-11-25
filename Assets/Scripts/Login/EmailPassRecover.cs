using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class EmailPassRecover : MonoBehaviour
{
    public InputField EmailInput, TelefonoInput;
    public TMP_Text FeedBack;
    public GameObject MailEnviado;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendEmail();
        }
    }

    IEnumerator SendMail(string loginEmail, string telefono)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginEmail", loginEmail);
        form.AddField("Tel", telefono);

        using (UnityWebRequest www = UnityWebRequest.Post("https://teckdes.com/ExpoVirtual/VirtualExpo/PassRecover/Email-Pass-Recover.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                FeedBack.text = www.error;
            }
            else
            {
                print(www.downloadHandler.text);
                if (www.downloadHandler.text.Contains("Mail Enviado.\n"))
                {
                    Link(www.downloadHandler.text.Split('|')[1]);
                    MailEnviado.SetActive(true);
                }
                else
                    FeedBack.text = www.downloadHandler.text;
            }
        }
    }

    public void SendEmail()
    {
        StartCoroutine(SendMail(EmailInput.text, TelefonoInput.text));
    }

    public void Link(string link) {
#if UNITY_WEBGL
        Application.ExternalEval("window.open('" + link + "' , '_blank')");
#else
        Application.OpenURL(link);
#endif
    }

    public void Volver() {
        FeedBack.text = "";
    }
}
