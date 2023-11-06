using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Networking;

public class BusinessCardScript : MonoBehaviour {

    public UserInfo userInfo;
    private RectTransform rectTransform;
    PhotonView PV;

    [SerializeField]private string Empresa, Email;
    [SerializeField]private string anotherUserName, anotherName, anotherEmpresa, anotherEmail, anotherTel;
    [SerializeField]private int anotherUserID;

    [SerializeField] private GameObject AvisoPrefab, Pestaña, Saved, Delete, SaveButton;
    [SerializeField] private TMP_Text NombreTxt, EmpresaTxt, MailTxt, TelefonoTxt;

    private void Start()
    {
        rectTransform = this.gameObject.GetComponent<RectTransform>();
        PV = GameObject.Find(PhotonNetwork.LocalPlayer.NickName).GetComponent<PhotonView>();
    }

    public void SetDataBusinessCard(string anotherUserName_,string anotherName, string anotherEmpresa, string anotherMail, string anotherTel){
        gameObject.name = "DNI" + anotherUserName_;

        Empresa = UserInfo.Empresa;
        Email = UserInfo.Email;
        anotherUserName = anotherUserName_;
        NombreTxt.text = anotherName;
        EmpresaTxt.text = anotherEmpresa;
        MailTxt.text = anotherEmail;
        TelefonoTxt.text = anotherTel;

        SaveButton.SetActive(false);
        Saved.SetActive(true);
        Delete.SetActive(true);

        transform.localPosition = Vector2.zero;
        transform.position = Vector2.zero;
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
        if (!Pestaña.activeSelf)
        {
            Pestaña.SetActive(true);
            rectTransform.sizeDelta = new Vector2(305, 168);
        }
        else{
            Pestaña.SetActive(false);
            rectTransform.sizeDelta = new Vector2(305, 61);
        }
    }

    public void Cerrar() {
        Destroy(this.gameObject);
    }

    public void GuardarDNI() {
        WWWForm form = new WWWForm();
        form.AddField("UserEmail", UserInfo.Email);
        form.AddField("AnotherUser", anotherName);
        form.AddField("AnotherUserName", anotherUserName);
        form.AddField("AnotherUserEmpresa", anotherEmpresa);
        form.AddField("AnotherUserEmail", anotherEmail);
        form.AddField("AnotherUserTel", anotherTel);

        StartCoroutine(Main.Instance.ConnectDB("https://teckdes.com/ExpoVirtual/VirtualExpo/Save-DNIs.php", form, (data) => {
            Debug.Log(data);
        }));
    }

    public void EliminarDNI() {
        WWWForm form = new WWWForm();
        form.AddField("UserEmail", UserInfo.Email);
        form.AddField("AnotherUserEmail", anotherEmail);
        form.AddField("DNIorChat", "DNI");

        Debug.Log("Ejecutado");
        StartCoroutine(Main.Instance.ConnectDB("https://teckdes.com/ExpoVirtual/VirtualExpo/Delete_DNI_Chat.php", form, (data) => {
            Debug.Log(data);
            if (data == "Tarjeta eliminada."){
                Destroy(this.gameObject);
            }
        }));
    }
}
