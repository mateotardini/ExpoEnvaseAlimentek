using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PeticionScript : MonoBehaviour
{
    public UserInfo userInfo;
    public int typeOfRequest, myID;
    public string anotherUserName, anotherUserEmpresa,anotherUserEmail;
    [SerializeField] string myEmpresa, myEmail;
    public InteractividadPersonaje colisionDetected;

    private void Start()
    {
        myEmpresa = UserInfo.Empresa;
        myEmail = UserInfo.Email;
        if (typeOfRequest == 1)
        {
            transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Ver";
            transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Ok";
        }
    }
    public void Aceptar() {
        //Si es una solicitud de chat
        if (typeOfRequest == 0)
        {
            //Crea el chat en quien acepta el pedido.
            colisionDetected.CrearChat(anotherUserName, anotherUserEmpresa, anotherUserEmail);

            //Busca el usuario de quien inicio el pedido.
            foreach (Player p in PhotonNetwork.PlayerListOthers)
            {
                if (p.NickName == anotherUserName)
                {
                    GameObject anotherPlayer = GameObject.Find(anotherUserName);
                    PhotonView anotherPV = anotherPlayer.GetComponent<PhotonView>();
                    int anotherUSerID = anotherPV.ViewID;
                    anotherPV.RPC("SyncChat", p, PhotonNetwork.LocalPlayer.NickName, myEmpresa, myEmail, anotherUSerID);
                }
            }
            //Elimina el panel de pedido una vez determinado que hacer.
            Destroy(this.gameObject);
        }
        //Si me enviaron una tarjeta
        else if (typeOfRequest == 1) {
            GameObject.Find("GameController").GetComponent<GameController>().Abrir_CerrarTarjetas();
        }
    }

    public void Rechazar() {
        //Elimina el pedido.
        Destroy(this.gameObject);
    }

}
