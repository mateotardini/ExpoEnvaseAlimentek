using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Networking;

public class DNIScript : MonoBehaviour {

    public UserInfo userInfo;
    [SerializeField] string Empresa, Email;
    public string anotherUserName, anotherName, anotherEmpresa, anotherEmail, anotherTel;
    public int anotherUserID;
    public GameObject AvisoPrefab, Pestaña, Saved, Delete, SaveButton;
    private RectTransform rectTransform;
    private bool pestañaAbrierta = false;
    public TMP_Text NombreTxt, EmpresaTxt, MailTxt, TelefonoTxt;

    public PhotonView PV;

    private void Start()
    {
        rectTransform = this.gameObject.GetComponent<RectTransform>();
        PV = GameObject.Find(PhotonNetwork.LocalPlayer.NickName).GetComponent<PhotonView>();
        Empresa = UserInfo.Empresa;
        Email = UserInfo.Email;
        NombreTxt.text = anotherName;
        EmpresaTxt.text = anotherEmpresa;
        MailTxt.text = anotherEmail;
        TelefonoTxt.text = anotherTel;
    }

    public void IniciarChat() {
        GameObject ChatToSearch = GameObject.Find("Chat" + anotherUserName);
        if (ChatToSearch == null)
        {
            //Creo un feedback avisando que se envio la invitacion.
            GameObject NewAviso = GameObject.Instantiate(AvisoPrefab, Vector2.zero, Quaternion.identity, GameObject.Find("FeedBackPos").transform);
            NewAviso.transform.localPosition = Vector2.zero;
            NewAviso.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Su solicitud de chat fue enviada, aguarde a que " + anotherUserName + " acepte.";
            Destroy(NewAviso, 2.6f);
            foreach (Player p in PhotonNetwork.PlayerListOthers)
            {
                if (p.NickName == anotherUserName)
                {
                    PhotonView anotherPV = GameObject.Find(anotherUserName).GetComponent<PhotonView>();
                    int anotherUSerID = anotherPV.ViewID;
                    print(p.NickName);
                    //Creo una solicitud de chat al otro usuario.
                    anotherPV.RPC("SyncRequest", p, PhotonNetwork.LocalPlayer.NickName, Empresa, Email, anotherUSerID, 0, PV.ViewID);
                }
            }
        }
        else
        {
            GameObject NewAviso = GameObject.Instantiate(AvisoPrefab, Vector2.zero, Quaternion.identity, GameObject.Find("FeedBackPos").transform);
            NewAviso.transform.localPosition = Vector2.zero;
            NewAviso.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Actualmente posee un chat con " + anotherUserName + ", revise su panel de chats.";
            Destroy(NewAviso, 2.6f);
        }
    }

    public void Abrir_CerrarPestaña() {
        if (!pestañaAbrierta)
        {
            Pestaña.SetActive(true);
            rectTransform.sizeDelta = new Vector2(305, 168);
            pestañaAbrierta = true;
        }
        else if (pestañaAbrierta) {
            Pestaña.SetActive(false);
            rectTransform.sizeDelta = new Vector2(305, 61);
            pestañaAbrierta = false;
        }
    }

    public void Cerrar() {
        Destroy(this.gameObject);
    }

    public void GuardarDNI() {
        StartCoroutine(SaveDNIsStringPhP());
    }

    public IEnumerator SaveDNIsStringPhP()
    {
        WWWForm form = new WWWForm();
        form.AddField("UserEmail", UserInfo.Email);
        form.AddField("AnotherUser", anotherName);
        form.AddField("AnotherUserName", anotherUserName);
        form.AddField("AnotherUserEmpresa", anotherEmpresa);
        form.AddField("AnotherUserEmail", anotherEmail);
        form.AddField("AnotherUserTel", anotherTel);

        using (UnityWebRequest www = UnityWebRequest.Post("https://expovirtual.com.ar/VirtualExpo/Save-DNIs.php", form))
        {
            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                SaveButton.SetActive(false);
                Saved.SetActive(true);
                Delete.SetActive(true);
            }
        }
    }

    public void EliminarDNI() {
        StartCoroutine(DeleteDNIPhP());
    }
    public IEnumerator DeleteDNIPhP()
    {
        WWWForm form = new WWWForm();
        form.AddField("UserEmail", UserInfo.Email);
        form.AddField("AnotherUserEmail", anotherEmail);
        form.AddField("DNIorChat", "DNI");

        using (UnityWebRequest www = UnityWebRequest.Post("https://expovirtual.com.ar/VirtualExpo/Delete_DNI_Chat.php", form))
        {
            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.downloadHandler.text == "Tarjeta eliminada.")
                    Destroy(this.gameObject);
            }
        }
    }
}
