using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ControlScriptsPlayer : MonoBehaviour
{
   [SerializeField] PhotonView myPhotonview;
    MoveBehaviour MoveBehaviour;
    BasicBehaviour BasicBehaviour;
    AimBehaviourBasic AimBehaviourBasic;
    InfoUsuario InfoUsuario;
    ThirdPersonOrbitCamBasic ThirdPersonOrbitCamBasic;
    [SerializeField]Camera myCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        myPhotonview = GetComponent<PhotonView>();
        MoveBehaviour = GetComponent<MoveBehaviour>();
        BasicBehaviour = GetComponent<BasicBehaviour>();
        AimBehaviourBasic = GetComponent<AimBehaviourBasic>();
        InfoUsuario = GetComponent<InfoUsuario>();
        ThirdPersonOrbitCamBasic = GetComponentInChildren<ThirdPersonOrbitCamBasic>();
        //ThirdPersonOrbitCamBasic = GetComponent<ThirdPersonOrbitCamBasic>();
        //myCamera = Camera.main;
        if (myPhotonview.IsMine == false) {
            myCamera.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (myPhotonview.IsMine == false && PhotonNetwork.IsConnected == true) 
        {
            MoveBehaviour.enabled = false;
            //BasicBehaviour.enabled = false;
            //AimBehaviourBasic.enabled = false;
            //InfoUsuario.enabled = false;
            //ThirdPersonOrbitCamBasic.enabled = false;
            
        }
    }
}
