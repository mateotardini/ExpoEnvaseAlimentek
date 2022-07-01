using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertasHallPrincipal : MonoBehaviour
{
    public AnalyticsPabellones analyticsPabellones;
    public int numPabellon;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == UserInfo.UserName) {
            analyticsPabellones.CompracionDePabellonActualYAnterior(numPabellon);
        }
    }
}
