using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSetup : MonoBehaviour
{
    private PhotonView PV;
    public GameObject myCharacter;
    public int characterValue;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            PV.RPC("RPC_AddCharacter", RpcTarget.AllBuffered, PlayerInfo.PI.mySelectedCharacter);
        }
    }

   [PunRPC]
   void RPC_AddCharacter(int wichCharacter)
    {
        characterValue = wichCharacter;
        myCharacter = Instantiate(PlayerInfo.PI.allCharacters[wichCharacter], transform.position, transform.rotation);
    }
}
