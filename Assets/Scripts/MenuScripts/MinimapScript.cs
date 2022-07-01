using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{
    private ThirdPersonOrbitCamBasic camScript;
    public AnalyticsPabellones analyticsPabellones;
    public ThirdPersonOrbitCamBasic GetCamScript { get { return camScript; } }

    public Transform player;
    private CharacterController characterController;
    ControlsTutorial GameController;

    public GameObject ResultadosBusquedaPanel;
    public bool OnInput;

    public GameObject[] BotonesDePabellones;
    public int PabellonActual;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        characterController = player.GetComponent<CharacterController>();
        GameController = GameObject.Find("GameController").GetComponent<ControlsTutorial>();
    }

    public void CerrarMapa() {
        characterController.enabled = true;
        if (analyticsPabellones != null)
            analyticsPabellones.CompracionDePabellonActualYAnterior(PabellonActual);
        ResultadosBusquedaPanel.SetActive(false);
        GameController.mapaActive = true;
        GameController.Abrir_CerrarMapa();
        GameController.buscadorActive = true;
        GameController.Abrir_CerrarBuscador();
    }

    public void GoToStand(string numeroStand) {
        characterController.enabled = false;
        string tpName = "TP_Position" + numeroStand;
        Transform Stand = GameObject.Find(tpName).transform;
        player.transform.position = Stand.position;
        player.transform.rotation = Stand.rotation;
        PabellonActual = int.Parse(numeroStand.Substring(0,1));
        CerrarMapa();
    }

    public void IngresoHallExterior()
    {
        characterController.enabled = false;
        player.transform.position = new Vector3(Random.Range(-165.0f, -195.0f), 3.10f, Random.Range(-110.0f, -137.0f));
        player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        CerrarMapa();
    }
    public void IngresoHallIAE()
    {
        characterController.enabled = false;
        player.transform.position = new Vector3(Random.Range(-165.0f, -195.0f), 3.10f, Random.Range(-53.0f, -12.0f));
        player.transform.rotation = Quaternion.Euler(0f,0f,0f);
        CerrarMapa();
    }

}
