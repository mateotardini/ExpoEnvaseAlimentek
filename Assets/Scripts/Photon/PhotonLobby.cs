using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public UserInfo userInfo;

    public static PhotonLobby lobby;
    RoomInfo[] rooms;
    
    public GameObject battleButton;
    public GameObject cancelButton;

    public GameObject ReconnectingPanel;
    public Button siguienteButton;

    private void Awake()
    {
        lobby = this; //Create the singleton, lives withing the main manu scene.
    }


    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        //InvokeRepeating("CheckConnection", 3, 3);//Connect to Master photon server.
    }

    void CheckConnection()
    {
        if (!PhotonNetwork.IsConnectedAndReady)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                print("No hay internet.");
                ReconnectingPanel.SetActive(true);
                siguienteButton.interactable = false;
                StartCoroutine(DisconnectAfterNoNET());
            }
            StartCoroutine(WaitReconnect());
        }
    }

    IEnumerator DisconnectAfterNoNET()
    {
        CancelInvoke("CheckConnection");
        while (PhotonNetwork.IsConnected)
            yield return null;
    }

    IEnumerator WaitReconnect()
    {
        PhotonNetwork.Reconnect();
        while (!PhotonNetwork.IsConnected)
        {
            
            print("Reconectando...");
            yield return null;
        }
        print("Reconectado.");
        ReconnectingPanel.SetActive(false);
        siguienteButton.interactable = true;
        InvokeRepeating("CheckConnection", 0, 3);
    }

    public override void OnConnectedToMaster() {

        Debug.Log("PLayer has connected to the Photon master server");
        ReconnectingPanel.SetActive(false);
        siguienteButton.interactable = true;
        battleButton.SetActive(true);
        battleButton.GetComponent<Button>().interactable = true;
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void OnBattleButtonClicked() {

        //battleButton.SetActive(false);
        //cancelButton.SetActive(true);
        battleButton.transform.GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().text = "Cargando...";
        StartCoroutine(SesionIniciadaPhP());
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("NO ROOM TO JOIN!!!");
        CreateRoom();
        
    }

    void CreateRoom() {

        int randomRoomName = 1;//UnityEngine.Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 0,
        };
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);
        Debug.Log("room creada numero" + randomRoomName);

    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("NO CREATED ROOM!!!");
        CreateRoom();

    }

    public void OnCancelButtonClicked() {
        
        battleButton.SetActive(true);
        cancelButton.SetActive(false);
        PhotonNetwork.LeaveRoom();
    }

    public IEnumerator SesionIniciadaPhP()
    {
        WWWForm form = new WWWForm();
        form.AddField("SesionesIniciadas", 1);

        using (UnityWebRequest www = UnityWebRequest.Post("https://expovirtual.com.ar/VirtualExpo/AnalyticsPrincipal.php", form))
        {
            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {

            }
        }
    }

    #region LogOut
    public void LogOut() {
        PlayerPrefs.DeleteAll();
        StartCoroutine(WaitDisconnect());
    }
    IEnumerator WaitDisconnect() {
        PhotonNetwork.Disconnect();
        PlayerPrefs.DeleteAll();
        while (PhotonNetwork.IsConnected)
            yield return null;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Login&Register");
    }
    #endregion
}
