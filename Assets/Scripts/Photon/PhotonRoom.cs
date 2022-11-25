using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;


public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    //Room Info
    public static PhotonRoom room;
    private PhotonView PV;

    public int currentScene;
    public int multiplayerScene;


    public GameObject myCharacter;
    public int characterValue;


    private void Awake()
    {
        if (PhotonRoom.room == null)
        {
            PhotonRoom.room = this;
        }
        else {

            if (PhotonRoom.room != this) {
                
                Destroy(PhotonRoom.room.gameObject);
                PhotonRoom.room = this;
            
            }
        
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

     

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }


    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
    }


    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            string kickPlayer = UserInfo.UserName;
            foreach (Player p in PhotonNetwork.PlayerListOthers)
            {
                if (p.NickName == kickPlayer)
                {
                    PV.RPC("SyncDisconnect", p);
                }
            }
            return;
        }
        else
        {
            string kickPlayer = UserInfo.UserName;
            foreach (Player p in PhotonNetwork.PlayerListOthers)
            {
                if (p.NickName == kickPlayer)
                {
                    PV.RPC("SyncDisconnect", p);
                }
                else
                    print("Sos el unico con esta cuenta.");
            }
        }
        PhotonNetwork.LoadLevel(multiplayerScene);
    }

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;
        if (currentScene == multiplayerScene)
        {
            {
                CreatePlayer();
            }

        }

    }
    private void CreatePlayer()
    {
        string pjSelected = "";
        switch (PlayerInfo.PI.mySelectedCharacter) {
            case 0:
               pjSelected = "Nathan";
               break;
            case 1:
                pjSelected = "Claudia";
                break;
            case 2:
                pjSelected = "Carla";
                break;
            case 3:
                pjSelected = "Erick";
                break;

        }
        Vector3 position = new Vector3(UnityEngine.Random.Range(-165.0f, -195.0f), 3.10f, UnityEngine.Random.Range(-110.0f, -137.0f));
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", pjSelected), position, Quaternion.identity, 0);

    }
    [PunRPC]
    void RPC_AddCharacter(int wichCharacter)
    {
        Vector3 position = new Vector3(UnityEngine.Random.Range(-165.0f, -195.0f), 3.10f, UnityEngine.Random.Range(-110.0f, -137.0f));
        characterValue = wichCharacter;
        myCharacter = Instantiate(PlayerInfo.PI.allCharacters[wichCharacter], position, Quaternion.identity);
    }
    [PunRPC]
    void SyncDisconnect() {
        StartCoroutine(WaitDisconnect());
    }
    IEnumerator WaitDisconnect() {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;
        SceneManager.LoadScene("Login&Register");
    }

}
