using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PanelDeInteraccionScript : MonoBehaviour
{
    private Transform target;
    public TMP_Text NombreTxt, EmpresaTxt;

    public RaycastHit hit;
    public InteractividadPersonaje colisionDetected;
    public InfoUsuario infoUsuario;

    public GameObject AvisoPrefab;

    public PhotonView PV;


    private void Start()
    {
        InfoUsuario anotherUser = hit.transform.gameObject.GetComponent<InfoUsuario>();
        NombreTxt.text = anotherUser.NombreUser;
        EmpresaTxt.text = anotherUser.Empresa;
    }
    void Update()
    {
        //El panel siempre mira hacia el usuario que lo creo.
        transform.LookAt(target);
        transform.rotation = new Quaternion(transform.rotation.x, 0f, transform.rotation.z, 0f);

        //Destruye el panel si estoy muy lejos.
        float distance = Vector3.Distance(this.transform.position, target.position);
        if (distance >= 10f)
            Destroy(this.gameObject);

    }

    public void Follow(Transform player) {
        target = player;
    }

    public void CrearChat() {
        InfoUsuario anotherUser = hit.transform.gameObject.GetComponent<InfoUsuario>();
        string anotherUserName = anotherUser.NombreUser;

        //Si no existe un chat ya creado...
        GameObject ChatToSearch = GameObject.Find("Chat" + anotherUserName);                           
        if (ChatToSearch == null)
        {
            //colisionDetected.CrearChat(anotherUserName);//

            //Creo un feedback avisando que se envio la invitacion.
            GameObject NewAviso = GameObject.Instantiate(AvisoPrefab, Vector2.zero, Quaternion.identity, GameObject.Find("FeedBackPos").transform);
            NewAviso.transform.localPosition = Vector2.zero;
            NewAviso.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Su solicitud de chat fue enviada, aguarde a que " + anotherUserName + " acepte.";
            Destroy(NewAviso, 2.6f);
            foreach (Player p in PhotonNetwork.PlayerListOthers)
            {
                if (p.NickName == anotherUserName)
                {
                    PhotonView anotherPV = hit.transform.gameObject.GetComponent<PhotonView>();
                    int anotherUSerID = anotherPV.ViewID;
                    //anotherPV.RPC("SyncChat", p, PhotonNetwork.LocalPlayer.NickName, anotherUSerID);//

                    //Creo una solicitud de chat al otro usuario.
                    anotherPV.RPC("SyncRequest", p, PhotonNetwork.LocalPlayer.NickName, UserInfo.Empresa, UserInfo.Email, anotherUSerID, 0, PV.ViewID);
                }
            }
        }
        else
            return;
        Cerrar();
    }

    public void CrearDNI()
    {
        InfoUsuario anotherUser = hit.transform.gameObject.GetComponent<InfoUsuario>();
        string anotherUserName = anotherUser.NombreUser;
        //Creo un feedback avisando que se envio la invitacion.
        GameObject NewAviso = GameObject.Instantiate(AvisoPrefab, Vector2.zero, Quaternion.identity, GameObject.Find("FeedBackPos").transform);
        NewAviso.transform.localPosition = Vector2.zero;
        NewAviso.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Su tarjeta de presentación fue enviada a " + anotherUserName + ".";
        Destroy(NewAviso, 2.6f);
        foreach (Player p in PhotonNetwork.PlayerListOthers) { 
            if (p.NickName == anotherUserName)
            {
                PhotonView anotherPV = hit.transform.gameObject.GetComponent<PhotonView>();
                int anotherUSerID = anotherPV.ViewID;
                //Le creo una tarjeta de presentacion con mis datos al otro usuario.
                anotherPV.RPC("SyncDNI", p, UserInfo.UserName, UserInfo.Name, UserInfo.Empresa, UserInfo.Email, UserInfo.Telphone, anotherUSerID);
                anotherPV.RPC("SyncRequest", p, PhotonNetwork.LocalPlayer.NickName, UserInfo.Empresa, UserInfo.Email, anotherUSerID, 1, PV.ViewID);
            }
        }
        Cerrar();
    }

    public void Cerrar() {
        Destroy(this.gameObject);
    }
}
