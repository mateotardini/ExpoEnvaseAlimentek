using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ControlScriptsPlayer : MonoBehaviour
{
   [SerializeField]private PhotonView myPhotonview;
    private MoveBehaviour MoveBehaviour;
    private BasicBehaviour BasicBehaviour;
    private AimBehaviourBasic AimBehaviourBasic;
    private InfoUsuario InfoUsuario;
    private ThirdPersonOrbitCamBasic ThirdPersonOrbitCamBasic;
    [SerializeField]private Camera myCamera;
    
    void Start()
    {
        myPhotonview = GetComponent<PhotonView>();
        MoveBehaviour = GetComponent<MoveBehaviour>();
        BasicBehaviour = GetComponent<BasicBehaviour>();
        AimBehaviourBasic = GetComponent<AimBehaviourBasic>();
        InfoUsuario = GetComponent<InfoUsuario>();
        ThirdPersonOrbitCamBasic = GetComponentInChildren<ThirdPersonOrbitCamBasic>();
        if (myPhotonview.IsMine == false) {
            myCamera.enabled = false;
        }
    }

    void Update()
    {
        if (myPhotonview.IsMine == false && PhotonNetwork.IsConnected == true) 
        {
            MoveBehaviour.enabled = false;
        }
    }
}
