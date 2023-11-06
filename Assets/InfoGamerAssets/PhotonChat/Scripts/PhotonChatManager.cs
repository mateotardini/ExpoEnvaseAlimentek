using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.XR.WSA;
using System.Collections;

public class PhotonChatManager : MonoBehaviour, IChatClientListener
{
    #region Setup
    [SerializeField] GameObject joinChatButton;
    ChatClient chatClient;
    bool isConnected;
    [SerializeField] string username, CompanyName;

    public UserInfo UserInfo;
    public SaveChats SaveChats;
    public AnalitycsTest analitycsTest;
    [SerializeField]private TMP_Text NombreChat, EmpresaTxt;
    public string Receiver, anotherUserEmpresa, anotherUserEmail;
    public string currentChatChannel;

    public GameObject ChatPanel, ChatButton, NewMessage, DeletePanel;
    public TMP_Text NombreChatButton;
    private bool _chatOpen = false, _chatDeleting = false;

    [SerializeField]private GameObject ChatAbierto, MyMessage, AnotherMessage;
    public string ChatText = "";

    public SaveChats saveChats;
    private bool _countInAnalitycs = false;


    void Start() {
        NombreChat.text = Receiver;
        NombreChatButton.text = Receiver;
        EmpresaTxt.text = anotherUserEmpresa;

        if (UserInfo.TipoDeUsuario == "Expositor" && !_countInAnalitycs) {
            CompanyName = UserInfo.Empresa;
            analitycsTest.ChatsIniciados(CompanyName);
        }
        ChatPanel.SetActive(false);

        ChatAbierto = GameObject.Find("ChatsAbiertos");
        ChatPanel.transform.SetParent(ChatAbierto.transform);
        ChatPanel.transform.localPosition = Vector2.zero;
    }

    public void SetDataChat(string anotherUser, string empresa, string email, string chatText){
        name = "Chat" + anotherUser;
        Receiver = anotherUser;
        anotherUserEmpresa = empresa;
        anotherUserEmail = email;
        chatDisplay.text = chatText;

        GetComponent<RectTransform>().pivot = Vector2.zero;
        transform.position = Vector2.zero;

        _countInAnalitycs = true;
        
        ChatConnectOnClick();
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void UsernameOnValueChange()
    {
        //username = valueIn;
        this.username = UserInfo.UserName;
    }

    public void OpenCloseChat() {
        if (_chatOpen)
        {
            NewMessage.SetActive(false);
            ChatPanel.SetActive(false);
            _chatOpen = false;
        }
        else {
            this.gameObject.transform.parent.GetComponent<ContentChats>().DeactivateAllChats();
            NewMessage.SetActive(false);
            ChatPanel.SetActive(true);
            _chatOpen = true;
        }
    }

    public void GetChatText() {
        ChatText = chatDisplay.text;
    }

    public void CloseChat() {
        ChatPanel.SetActive(false);
        ChatButton.SetActive(true);
        _chatOpen = false;
    }

    public void DeleteChatPanel() {
        if (!_chatDeleting)
        {
            DeletePanel.SetActive(true);
            _chatDeleting = true;
        }
        else
        {
            DeletePanel.SetActive(false);
            _chatDeleting = false;
        }
    }

    public void DeleteChat() {
        SaveChats.DeleteChat();
        SubmitPrivateChatOnClick();
        StartCoroutine(WaitCloseChat());
    }

    IEnumerator WaitCloseChat() {
        yield return new WaitForSeconds(0.5f);
        Destroy(ChatPanel);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

    public void ChatConnectOnClick()
    {
        isConnected = true;
        chatClient = new ChatClient(this);
        chatClient.ChatRegion = "EU";
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(UserInfo.UserName));    //Toma el nombre desde userinfo para conectar directamente.

    }

    #endregion Setup

    #region General

    [SerializeField] GameObject chatPanel;
    string privateReceiver = "";
    string currentChat;
    [SerializeField] InputField chatField;
    [SerializeField] Text chatDisplay;

    void Update()
    {
        if (isConnected)
        {
            chatClient.Service();
        }

        if (chatField.text != "" && Input.GetKey(KeyCode.Return))
        {
 //         SubmitPublicChatOnClick();
            SubmitPrivateChatOnClick();
        }
    }

    #endregion General

    #region PublicChat

    public void SubmitPublicChatOnClick()
    {
        if (privateReceiver == "" && chatField.text != "")
        {
            chatClient.PublishMessage("RegionChannel", currentChat);
            chatField.text = "";
            currentChat = "";
        }
    }

    public void TypeChatOnValueChange(string valueIn)
    {
        currentChat = valueIn;
    }

    #endregion PublicChat

    #region PrivateChat

    public void ReceiverOnValueChange()
    {
        privateReceiver = this.Receiver;
    }

    public void SubmitPrivateChatOnClick()
    {
        privateReceiver = this.Receiver;
        if (privateReceiver != "" && chatField.text != "")
        {
            chatClient.SendPrivateMessage(privateReceiver, currentChat);
            chatField.text = "";
            currentChat = "";
        }
    }

    #endregion PrivateChat

    #region Callbacks

    public void DebugReturn(DebugLevel level, string message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnChatStateChange(ChatState state)
    {
        if(state == ChatState.Uninitialized)
        {
            isConnected = false;
            joinChatButton.SetActive(true);
            chatPanel.SetActive(false);
        }

        //throw new System.NotImplementedException();
        //Debug.Log("Connected");
        //isConnected = true;
        //joinChatButton.SetActive(false);
    }

    public void OnConnected()
    {
        joinChatButton.SetActive(false);
        chatClient.Subscribe(new string[] { currentChatChannel });
        SubmitPrivateChatOnClick();
    }

    public void OnDisconnected()
    {
        isConnected = false;
        joinChatButton.SetActive(true);
        chatPanel.SetActive(false);
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        string msgs = "";
        for (int i = 0; i < senders.Length; i++)
        {
            msgs = string.Format("{0}: {1}", senders[i], messages[i]);

            //GameObject NewMessage = GameObject.Instantiate(MyMessage, Vector2.zero, Quaternion.identity, chatDisplay.transform);
            //NewMessage.transform.localPosition = Vector2.zero;
            //NewMessage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = msgs;

            chatDisplay.text += "\n" + msgs;
        }

    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        if (channelName == UserInfo.UserName + ":" + Receiver)
        {

            string msgs = "";

            string time = System.DateTime.Now.ToString("HH:mm");

            msgs = string.Format("[" + time + "] {0}: {1}", sender, message);

            if (msgs.Contains("se ha unido al chat."))
            {
                chatDisplay.text += "\n" + msgs.Remove(0,7);
                //FindOnline(Receiver);
            }
            else
                chatDisplay.text += "\n" + msgs;

            /*GameObject BubbleMessage = GameObject.Instantiate(MyMessage, Vector2.zero, Quaternion.identity);
            BubbleMessage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = msgs;
            BubbleMessage.transform.SetParent(chatDisplay.transform);
            BubbleMessage.transform.localPosition = Vector2.zero;*/

            NewMessage.SetActive(true);
            transform.SetAsLastSibling();
            saveChats.SaveChatString();
        }
        
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        chatPanel.SetActive(true);
    }

    public void OnUnsubscribed(string[] channels)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    #endregion Callbacks


    /*void FindOnline(string anotherUserName)
    {
        foreach (Player p in PhotonNetwork.PlayerListOthers)
        {
            if (p.NickName == anotherUserName)
            {
                PhotonView anotherPV = GameObject.Find(anotherUserName).GetComponent<PhotonView>();
                int anotherUSerID = anotherPV.ViewID;
                //anotherPV.RPC("SyncChat", p, PhotonNetwork.LocalPlayer.NickName, anotherUSerID);//

                //Creo una solicitud de chat al otro usuario.
                anotherPV.RPC("SyncOnline", p, PhotonNetwork.LocalPlayer.NickName, PV.ViewID);
            }
        }
    }

    [PunRPC]
    private void SyncOnline(string anotherUserName, int anotherUserID)
    {
        PhotonView.Find(anotherUserID).GameObject.Find("Chat" + anotherUserName).transform.GetChild(1).GetChild(2).gameobject.SetActive(true);
    }*/
}
