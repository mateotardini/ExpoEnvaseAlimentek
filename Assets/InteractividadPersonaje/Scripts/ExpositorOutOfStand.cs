using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpositorOutOfStand : MonoBehaviour
{
    public InteractividadPersonaje colisionDetected;
    public LayerMask InStandAnotherLayer, OutStandAnotherLayer;
    public PhotonView PV;
    private void Start()
    {
        colisionDetected = this.gameObject.GetComponent<InteractividadPersonaje>();
        PV = this.gameObject.GetComponent<PhotonView>();
    }
    private void OnTriggerEnter(Collider col)
    {
        if (PV.IsMine && col.name.Contains(UserInfo.Empresa)) {
            colisionDetected.playerMask = InStandAnotherLayer;
            this.gameObject.layer = 10;
            print("Dentro");
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (PV.IsMine && col.name.Contains(UserInfo.Empresa))
        {
            colisionDetected.playerMask = OutStandAnotherLayer;
            this.gameObject.layer = 9;
            print("Fuera");
        }
    }
}
