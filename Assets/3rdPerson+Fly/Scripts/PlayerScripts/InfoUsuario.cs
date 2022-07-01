using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;

public class InfoUsuario : MonoBehaviour
{    
    //public GameObject Character;
    public TextMesh tm3D;
    public GameObject MePoint;
    PhotonView myPhotonview;
    public string NombreUser;
    public string Nombre;
    public string Email;
    public string Telphone;
    public string Empresa;
    public string Puesto;
    public string nickName;

    // Start is called before the first frame update
    private void Awake()
    {
        myPhotonview = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (myPhotonview.IsMine)
        {
            PhotonNetwork.NickName = UserInfo.UserName;
            this.name = UserInfo.UserName;
            if (tm3D != null)
                tm3D.text = this.name;
            NombreUser = UserInfo.UserName;
            Nombre = UserInfo.Name;
            Empresa = UserInfo.Empresa;
            Puesto = UserInfo.Puesto;
            Email = UserInfo.Email;
            Telphone = UserInfo.Telphone;
            MePoint.SetActive(true);

            this.myPhotonview.RPC("SyncData", RpcTarget.AllBuffered, NombreUser, Empresa, Puesto, Email, Nombre, Telphone);
        }
        else
        {
            foreach (Player p in PhotonNetwork.PlayerListOthers)
            {
                nickName = p.NickName;
            }
        }
    }

    public void ReSendData(string newMail, string newTel) {
        this.myPhotonview.RPC("SyncData", RpcTarget.AllBuffered, NombreUser, Empresa, Puesto, newMail, Nombre, newTel);
        print("Reenviada");
    }

    [PunRPC]
    private void SyncData(string nombre, string empresa, string puesto, string email, string nombreCompleto, string telphone) {
        NombreUser = nombre;
        Nombre = nombreCompleto;
        Empresa = empresa;
        Puesto = puesto;
        Email = email;
        Telphone = telphone;
        foreach (Player p in PhotonNetwork.PlayerListOthers)
        {
            this.name = NombreUser;
        }
    }

}
