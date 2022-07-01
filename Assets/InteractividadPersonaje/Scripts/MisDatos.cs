using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using Photon.Pun;

public class MisDatos : MonoBehaviour
{
    public TMP_Text NameTMP, UsernameTMP, EmpresaTMP, EmailTMP, TelTMP;
    public TMP_InputField NewEmail, NewTel;
    public GameObject[] Botones;
    // Start is called before the first frame update
    void Start()
    {
        UsernameTMP.text = UserInfo.UserName;
        NameTMP.text = UserInfo.Name;
        EmpresaTMP.text = UserInfo.Empresa;
        EmailTMP.text = UserInfo.Email;
        TelTMP.text = UserInfo.Telphone;
    }

    public void Abrir_Analiticas() {
        Application.ExternalEval("window.open('" + "https://expovirtual.com.ar/VirtualExpo/Web/index.html" + "' , '_blank')");
    }

    public void ChangeData() {
        if (string.IsNullOrEmpty(NewTel.text))
            NewTel.text = TelTMP.text;
        if (string.IsNullOrEmpty(NewEmail.text))
            NewEmail.text = EmailTMP.text;
        StartCoroutine(ChangeDataPhP());
    }

    public IEnumerator ChangeDataPhP()
    {
        WWWForm form = new WWWForm();
        form.AddField("Email", UserInfo.Email);
        form.AddField("newEmail", NewEmail.text);
        form.AddField("newTel", NewTel.text);

        using (UnityWebRequest www = UnityWebRequest.Post("https://expovirtual.com.ar/VirtualExpo/ChangeUserData.php", form))
        {
            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.downloadHandler.text.Contains("Disponible"))
                {
                    if (!string.IsNullOrEmpty(NewTel.text))
                        UserInfo.Telphone = TelTMP.text;
                    if (!string.IsNullOrEmpty(NewEmail.text))
                        UserInfo.Email = NewEmail.text;
                    EmailTMP.text = UserInfo.Email;
                    TelTMP.text = UserInfo.Telphone;

                    foreach (GameObject boton in Botones)
                        boton.SetActive(false);
                    NewEmail.gameObject.SetActive(false);
                    NewTel.gameObject.SetActive(false);
                    EmailTMP.gameObject.SetActive(true);
                    TelTMP.gameObject.SetActive(true);
                }
                else if (www.downloadHandler.text.Contains("No disponible")) { 
                }
                GameObject myPlayer = GameObject.Find(UserInfo.UserName);
                myPlayer.GetComponent<InfoUsuario>().ReSendData(UserInfo.Email, UserInfo.Telphone);
            }
        }
    }
}
