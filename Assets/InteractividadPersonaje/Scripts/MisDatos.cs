using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MisDatos : MonoBehaviour
{
    [SerializeField] private TMP_Text NameTMP, UsernameTMP, EmpresaTMP, EmailTMP, TelTMP;
    [SerializeField] private TMP_InputField NewEmail, NewTel;
    [SerializeField] private GameObject[] Botones;
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
        Application.ExternalEval("window.open('" + "https://teckdes.com/ExpoVirtual/VirtualExpo/Web/index.html" + "' , '_blank')");
    }

    public void ChangeData() {
        if (string.IsNullOrEmpty(NewTel.text))
            NewTel.text = TelTMP.text;
        if (string.IsNullOrEmpty(NewEmail.text))
            NewEmail.text = EmailTMP.text;

        WWWForm form = new WWWForm();
        form.AddField("Email", UserInfo.Email);
        form.AddField("newEmail", NewEmail.text);
        form.AddField("newTel", NewTel.text);

        StartCoroutine(Main.Instance.ConnectDB("https://teckdes.com/ExpoVirtual/VirtualExpo/Load-Chats.php", form, (data) => {
            if (data.Contains("Disponible"))
            {
                if (!string.IsNullOrEmpty(NewTel.text))
                    UserInfo.Telphone = TelTMP.text;
                if (!string.IsNullOrEmpty(NewEmail.text))
                    UserInfo.Email = NewEmail.text;
                EmailTMP.text = UserInfo.Email;
                TelTMP.text = UserInfo.Telphone;

                foreach (GameObject boton in Botones){ boton.SetActive(false); }

                NewEmail.gameObject.SetActive(false);
                NewTel.gameObject.SetActive(false);
                EmailTMP.gameObject.SetActive(true);
                TelTMP.gameObject.SetActive(true);
            }

            GameObject myPlayer = GameObject.Find(UserInfo.UserName);
            myPlayer.GetComponent<InfoUsuario>().ReSendData(UserInfo.Email, UserInfo.Telphone);
        }));
    }
}
