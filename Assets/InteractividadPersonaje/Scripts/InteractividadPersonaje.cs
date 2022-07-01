using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using System.Runtime.InteropServices.WindowsRuntime;
using Photon.Realtime;
using UnityEngine.Networking.Types;

public class InteractividadPersonaje : MonoBehaviour
{
    private PhotonView PV;
    private ThirdPersonOrbitCamBasic camScript;
    public Transform playerCamera;

    public bool canInteract = false, openPanel = false, OnInput;
    public int OnClicksB = 0;

    public GameObject gameController;
    public ControlsTutorial scriptGameController;

    //Data de este User
    public InfoUsuario infoUsuario;
    public string Nombre, Empresa, Email;

    public SelectStand selectStand;

    public Data_Stand datastandScript;
    public AnalitycsTest analytics;

    public LayerMask playerMask, interactableMask, videoMask;
    public Renderer Mycolor;
    public Color32 currentColorRemera, currentColorPantalon, currentColorPiel;
    public int materialpiel, materialremera, materialpantalon;
    private CustomCharacter customCharacter;

    public GameObject ChatPrefab, DNIPrefab, PanelInteraccioPrefab, RequestPrefab, AvisoPrefab;

    private void Awake()
    {
        customCharacter = GameObject.Find("CustomCharacter").GetComponent<CustomCharacter>();
    }
    private void Start()
    {
        PV = GetComponent<PhotonView>();

        gameController = GameObject.Find("GameController");
        if(gameController != null)
            scriptGameController = gameController.GetComponent<ControlsTutorial>();

        Nombre = UserInfo.UserName;
        Empresa = UserInfo.Empresa;
        Email = UserInfo.Email;

        currentColorRemera = customCharacter.currentColorRemera;
        currentColorPantalon = customCharacter.currentColorPantalon;
        currentColorPiel = customCharacter.currentColorPiel;
        materialpiel = customCharacter.materialpiel;
        materialremera = customCharacter.materialremera;
        materialpantalon = customCharacter.materialpantalon;

        if (PV.IsMine)
            this.PV.RPC("SyncColor", RpcTarget.AllBuffered, currentColorRemera.r, currentColorRemera.g, currentColorRemera.b, currentColorRemera.a, 
                                                            currentColorPantalon.r, currentColorPantalon.g, currentColorPantalon.b, currentColorPantalon.a,
                                                            currentColorPiel.r, currentColorPiel.g, currentColorPiel.b ,currentColorPiel.a,materialpiel,materialremera,materialpantalon);
        if (PV.IsMine)
        {
            if (this.gameObject.layer == 10)
            {
                UserInfo.TipoDeUsuario = "Expositor";
            }
            else
                UserInfo.TipoDeUsuario = "Visitante";
            this.transform.GetChild(2).GetChild(0).gameObject.GetComponent<AudioListener>().enabled = true;
        }
        else
            Destroy(transform.GetChild(2).gameObject);
    }

    private void Update()
    {
        //Obtencion de datos del usurario clickeado//////////////////////////////////////////////////////////////////////////////////////////////
        if (Input.GetMouseButtonDown(0) && PV.IsMine)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Si clickeo sobre un usuario que sea apto y no sea yo, creo un panel de interaccion enviado el transform "hit" (osea la persona a la quien cree).
            if (Physics.Raycast(ray, out hit, 20f, playerMask))
            {
                if ((hit.transform != null) && (hit.transform != this.transform))
                {
                    CrearPanelInteraccion(hit);
                }
            }

            //Si clickeo sobre un stand me abre el panel para interactuar con este y agrega la funcionalidad de los botones.
            if (Physics.Raycast(ray, out hit, 15f, interactableMask) && scriptGameController.CheckAllClose()) {
                if ((hit.transform != null) && (hit.transform != this.transform))
                {
                    datastandScript = hit.transform.gameObject.GetComponent<Data_Stand>();
                    analytics = hit.transform.gameObject.GetComponent<AnalitycsTest>();
                    if (OnClicksB == 0)
                    {
                        OnClicksB = 1;
                    }
                    canInteract = true;
                    if (openPanel == false)
                    {
                        datastandScript.SetDataDB(scriptGameController.panelStands);
                        openPanel = true;
                    }
                    else {
                        Cerrar();
                        datastandScript.SetDataDB(scriptGameController.panelStands);
                        openPanel = true;
                    }
                }
            }

            //Si clickeo sobre una pantalla de video obtengo y ejecuto su script para abrir el embed.
            if (Physics.Raycast(ray, out hit, 15f, videoMask))
            {
                if ((hit.transform != null) && (hit.transform != this.transform))
                {
                    EmbedVideos script = hit.transform.gameObject.GetComponent<EmbedVideos>();
                    script.PlayEmbedVideo();
                }
            }
        }
    }



    //Sincronizacion de los colores de la remera, el chat y el DNI.//////////////////////////////////////////////////////////////////////////
    #region PUNRPC

    [PunRPC]
    private void SyncColor(byte rrem, byte grem, byte brem, byte arem, byte rpant, byte gpant, byte bpant, byte apant, byte rpiel, byte gpiel,byte bpiel, byte apiel,int piel, int remera,int pantalon) {
        
        Color32 syncColorPantalon = new Color32(rpant, gpant, bpant, apant);
        Color32 syncColorPiel = new Color32(rpiel, gpiel, bpiel, apiel);
        Color32 syncColorRemera = new Color32(rrem, grem, brem, arem);
        Mycolor.materials[remera].color = syncColorRemera;
        Mycolor.materials[pantalon].color = syncColorPantalon;
        Mycolor.materials[piel].color = syncColorPiel;
    }

    [PunRPC]
    private void SyncChat(string anotherUserName, string anotheUserEmpresa, string anotherUserEmail, int anotherUserID) {
        PhotonView.Find(anotherUserID).gameObject.GetComponent<InteractividadPersonaje>().CrearChat(anotherUserName, anotheUserEmpresa, anotherUserEmail);
    }
    [PunRPC]
    private void SyncDNI(string anotherUserName, string anotherName, string anotherEmpresa, string anotherEmail, string anotherTel, int anotherUserID)
    {
        PhotonView.Find(anotherUserID).gameObject.GetComponent<InteractividadPersonaje>().CrearDNI(anotherUserName, anotherName, anotherEmpresa, anotherEmail, anotherTel, anotherUserID);
    }

    [PunRPC]
    private void SyncRequest(string anotherUserName, string anotherUserEmpresa, string anotherUserEmail, int anotherUserID, int typeOfRequest, int myID) {
        PhotonView.Find(anotherUserID).gameObject.GetComponent<InteractividadPersonaje>().CrearRequest(anotherUserName, anotherUserEmpresa, anotherUserEmail, typeOfRequest, myID);
    }
    #endregion

    //Crear panel de interaccion con otros usuarios///////////////////////////////////////////////////////////////////////////////////////////
    public void CrearPanelInteraccion(RaycastHit hit) {
        GameObject PanelToSearch = GameObject.Find("PanelDeInteraccion(Clone)");
        if (PanelToSearch != null)
            Destroy(PanelToSearch);
        if (PV.IsMine)
        {
            GameObject NewPanel = GameObject.Instantiate(PanelInteraccioPrefab, new Vector3(hit.transform.position.x, hit.transform.position.y + 5f, hit.transform.position.z),
            Quaternion.LookRotation(hit.transform.position - transform.position, Vector3.down), hit.transform);
            PanelDeInteraccionScript script = NewPanel.GetComponent<PanelDeInteraccionScript>();
            script.Follow(transform);
            script.hit = hit;
            script.colisionDetected = this;
            script.infoUsuario = infoUsuario;
            script.PV = PV;
        }
    }
    #region Requests
    //Crear Request/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void CrearRequest(string anotherUserName, string anotherUserEmpresa, string anotherUserEmail, int typeOfRequest, int myID) {
        GameObject RequestToSearch = GameObject.Find("Pedido" + typeOfRequest + anotherUserName);
        if (RequestToSearch == null)
        {
            Transform NotificacionesOrder = scriptGameController.Notificaciones.transform.GetChild(3).GetChild(0).GetChild(0);
            GameObject NewRequest = GameObject.Instantiate(RequestPrefab, Vector2.zero, Quaternion.identity, NotificacionesOrder);
            PeticionScript NewRequestScript = NewRequest.GetComponent<PeticionScript>();
            if (typeOfRequest == 0)
            {
                NewRequest.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = anotherUserName + " desea invitarte a un chat privado.";
                NewRequestScript.typeOfRequest = 0;
                NewRequestScript.anotherUserName = anotherUserName;
                NewRequestScript.anotherUserEmpresa = anotherUserEmpresa;
                NewRequestScript.anotherUserEmail = anotherUserEmail;
                NewRequestScript.colisionDetected = this;
                NewRequestScript.myID = myID;
                scriptGameController.NuevasNotificaciones[1].SetActive(true);
                NewRequest.name = "Pedido" + typeOfRequest + anotherUserName;
                GameObject NewAviso = GameObject.Instantiate(AvisoPrefab, Vector2.zero, Quaternion.identity, GameObject.Find("FeedBackPos").transform);
                NewAviso.transform.localPosition = Vector2.zero;
                NewAviso.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = anotherUserName + " te ha enviado una solicitud de chat.";
                Destroy(NewAviso, 2.6f);
            }
            else
            {
                NewRequest.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = anotherUserName + " te ha enviado una tarjeta con sus datos.";
                NewRequestScript.typeOfRequest = 1;
                NewRequestScript.anotherUserName = anotherUserName;
                NewRequestScript.anotherUserEmpresa = anotherUserEmpresa;
                NewRequestScript.anotherUserEmail = anotherUserEmail;
                NewRequestScript.colisionDetected = this;
                NewRequestScript.myID = myID;
                NewRequest.name = "Pedido" + typeOfRequest + anotherUserName;
                scriptGameController.NuevasNotificaciones[1].SetActive(true);
            }
        }
    }
    #endregion

    //Crear Chat/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    #region Chats
    public void CrearChat(string anotherUserName, string anotherUserEmpresa, string anotherUserEmail) {
        GameObject ChatToSearch = GameObject.Find("Chat" + anotherUserName);
        if (ChatToSearch == null)
        {
            GameObject NewChat = GameObject.Instantiate(ChatPrefab, Vector2.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("ChatOrder").transform);
            NewChat.GetComponent<RectTransform>().pivot = Vector2.zero;
            NewChat.transform.position = Vector2.zero;
            NewChat.name = "Chat" + anotherUserName;
            PhotonChatManager NewChatScript = NewChat.GetComponent<PhotonChatManager>();
            NewChatScript.Receiver = anotherUserName;
            NewChatScript.anotherUserEmpresa = anotherUserEmpresa;
            NewChatScript.anotherUserEmail = anotherUserEmail;
            NewChatScript.ChatConnectOnClick();
            NewChat.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    #endregion

    //Crear DNI/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    #region Tarjetas
    public void CrearDNI(string anotherUserName, string anotherName, string anotherEmpresa, string anotherEmail, string anotherTel, int anotherUserID) {
        GameObject DNIToSearch = GameObject.Find("DNI" + anotherUserName);
        if (DNIToSearch == null)
        {
            Transform TarjetasOrder = scriptGameController.PanelTarjetas.transform.GetChild(3).GetChild(0).GetChild(0);
            GameObject NewDNI = GameObject.Instantiate(DNIPrefab, Vector2.zero , Quaternion.identity, TarjetasOrder);
            NewDNI.transform.localPosition = Vector2.zero;
            NewDNI.name = "DNI" + anotherUserName;
            scriptGameController.NuevasNotificaciones[0].SetActive(true);
            DNIScript script = NewDNI.GetComponent<DNIScript>();
            script.anotherUserName = anotherUserName;
            script.anotherName = anotherName;
            script.anotherEmpresa = anotherEmpresa;
            script.anotherEmail = anotherEmail;
            script.anotherTel = anotherTel;
            script.anotherUserID = anotherUserID;
        }
        else
        {
            Destroy(DNIToSearch);
            Transform TarjetasOrder = scriptGameController.PanelTarjetas.transform.GetChild(3).GetChild(0).GetChild(0);
            GameObject NewDNI = GameObject.Instantiate(DNIPrefab, Vector2.zero, Quaternion.identity, TarjetasOrder);
            NewDNI.transform.localPosition = Vector2.zero;
            NewDNI.name = "DNI" + anotherUserName;
            scriptGameController.NuevasNotificaciones[0].SetActive(true);
            DNIScript script = NewDNI.GetComponent<DNIScript>();
            script.anotherUserName = anotherUserName;
            script.anotherName = anotherName;
            script.anotherEmpresa = anotherEmpresa;
            script.anotherEmail = anotherEmail;
            script.anotherTel = anotherTel;
            script.anotherUserID = anotherUserID;
        }
        GameObject NewAviso = GameObject.Instantiate(AvisoPrefab, Vector2.zero, Quaternion.identity, GameObject.Find("FeedBackPos").transform);
        NewAviso.transform.localPosition = Vector2.zero;
        NewAviso.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = anotherUserName + " te ha enviado su tarjeta.";
        Destroy(NewAviso, 2.6f);
    }

    #endregion

    //Botones de funcionalidad del panel de los stands.//////////////////////////////////////////////////////////////////////////////////
    #region PanelStand

    void Cerrar() {
        scriptGameController.panelBotones[1].onClick.RemoveAllListeners();
        scriptGameController.panelBotones[2].onClick.RemoveAllListeners();
        scriptGameController.panelBotones[3].onClick.RemoveAllListeners();
        OnClicksB = 0;
        scriptGameController.panelStands.SetActive(false);
        openPanel = false;
    }
    #endregion
}
