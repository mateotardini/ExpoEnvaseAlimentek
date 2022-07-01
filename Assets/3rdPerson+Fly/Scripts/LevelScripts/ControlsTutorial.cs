using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Photon.Pun;
using System.Collections;


// This class is created for the example scene. There is no support for this script.
public class ControlsTutorial : MonoBehaviour
{
	//Boludeces para mostrar todos las solapas de chat.
	public GameObject Chats;
	public Transform startPos, finalPos;
	private bool chatsOpen = true;

	//Cosas del tutorial
	public GameObject Tutorial;
	public bool tutorialActive = false;
	public int paginaActiva = 0;

	//Cosas del buscador
	public GameObject Buscador;
	public bool buscadorActive = false;

	//Cosas de tarjetas
	public GameObject PanelTarjetas;
	public bool panelTarjetasActive = false;

	//Cosas Notificaciones
	public GameObject Notificaciones;
	public bool notificacionesActive = false;

	//Cosas Sonido
	public GameObject NotSound;
	public bool soundActive = true;

	//Cosas Mapa
	public GameObject Mapa, IrAqui, childText;
	public bool mapaActive = false;

	//Cosas Mis Datos
	public GameObject MisDatos;
	public bool misDatosActive = false;

	//Cosas Conferencias
	public GameObject Conferencias;
	public bool conferenciasActive = false;

	//Cosas Stands
	public GameObject panelStands;
	public Button[] panelBotones;

	public GameObject[] BarraPresionado, NuevasNotificaciones;
	public GameObject CartelReconectando, Recordatorio;

	public InputField chatInput;

	public GameObject Player;

	public MinimapScript mapScript;
	public InteractividadPersonaje colScript;
	public RPGMovement rpgMovementScript;
	//public BasicBehaviour behaviuorScript;
	//public AimBehaviourBasic aimScript;

    private void Start()
    {
		Player = GameObject.FindGameObjectWithTag("Player");
		colScript = Player.GetComponent<InteractividadPersonaje>();
		rpgMovementScript = Player.GetComponent<RPGMovement>();
		if (UserInfo.Level == "0") {
			Recordatorio.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Siendo visitantes solo puedes interactuar con expositores que esten dentro de su stand";
		}
		else
			Recordatorio.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Siendo expositor solo puedes interactuar con visitantes dentro de tu stand";
		Recordatorio.SetActive(true);
	}

	//Determina si se esta utilizando algun Input mediante el event system.
	private void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
			CerraPestañasAbiertas();

		if (EventSystem.current                                                                      
			&& EventSystem.current.currentSelectedGameObject
			&& (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<InputField>() || EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_InputField>()))
		{
			mapScript.OnInput = true;
			colScript.OnInput = true;
			rpgMovementScript.OnInput = true;
			//behaviuorScript.OnInput = true;
			//aimScript.OnInput = true;
			return;
		}
		else
		{
			mapScript.OnInput = false;
			colScript.OnInput = false;
			rpgMovementScript.OnInput = false;
			//behaviuorScript.OnInput = false;
			//aimScript.OnInput = false;
		}

		if (Input.GetKeyDown(KeyCode.M))
			Abrir_CerrarMapa();
		if (Input.GetKeyDown(KeyCode.C))
			CloseTutorial();
	}

	//Pestaña desplegable con todos los chats/////////////////////////////
	#region Chats
	public void OpenCloseChat() {
		if (Chats.transform.GetChild(0).GetChild(0).transform.childCount > 0)
		{
			if (!chatsOpen)
			{
				//Chats.transform.position = Vector2.MoveTowards(finalPos.position, startPos.position, 1* Time.deltaTime);
				Chats.GetComponent<Animator>().SetBool("Open", true);
				Chats.transform.GetChild(0).GetChild(0).GetComponent<ContentChats>().DeactivateAllChats();
				chatsOpen = true;
			}
			else
			{
				//Chats.transform.position= Vector2.MoveTowards(startPos.position, finalPos.position, 1* Time.deltaTime);
				Chats.transform.GetChild(0).GetChild(0).GetComponent<ContentChats>().DeactivateAllChats();
				Chats.GetComponent<Animator>().SetBool("Open", false);
				chatsOpen = false;
			}
		}
	}
    #endregion

    //Abrir_Cerrar panel de busqueda/////////////////////////////////////
    #region Busqueda
    public void Abrir_CerrarBuscador() {
		if (!buscadorActive)
		{
			CerraPestañasAbiertas();
			Buscador.SetActive(true);
			BarraPresionado[0].SetActive(true);
			buscadorActive = true;
		}
		else if (buscadorActive){
			StartCoroutine(WaitAnimations(Buscador));
			//Buscador.SetActive(false);
			BarraPresionado[0].SetActive(false);
			buscadorActive = false;
		}
	}
    #endregion

    //Tutorial y sus controles//////////////////////////////////////////
    #region Controles
    public void Controles_Movimiento() {
			Tutorial.transform.GetChild(11).gameObject.SetActive(false);
			Tutorial.transform.GetChild(10).gameObject.SetActive(true);
	}
	public void Controles_Camara()
	{
		Tutorial.transform.GetChild(11).gameObject.SetActive(true);
		Tutorial.transform.GetChild(10).gameObject.SetActive(false);
	}

	public void CloseTutorial() {
		if (tutorialActive)
		{
			Tutorial.transform.GetChild(11).gameObject.SetActive(false);
			Tutorial.transform.GetChild(10).gameObject.SetActive(true);
			StartCoroutine(WaitAnimations(Tutorial));
			//Tutorial.SetActive(false);
			tutorialActive = false;
			BarraPresionado[1].SetActive(false);
		}
		else if (!tutorialActive) {
			CerraPestañasAbiertas();
			Tutorial.transform.GetChild(10).gameObject.SetActive(true);
			Tutorial.transform.GetChild(11).gameObject.SetActive(false);
			Tutorial.SetActive(true);
			tutorialActive = true;
			BarraPresionado[1].SetActive(true);
		}

	}
    #endregion

    //Abrir_Cerrar Tarjetsas de presentacion//////////////////////////
    #region Tarjetas
    public void Abrir_CerrarTarjetas() {
		if (!panelTarjetasActive)
		{
			CerraPestañasAbiertas();
			PanelTarjetas.GetComponent<Animator>().SetTrigger("Open");
			PanelTarjetas.GetComponent<Animator>().SetBool("OpenB", true);
			BarraPresionado[2].SetActive(true);
			NuevasNotificaciones[0].SetActive(false);
			panelTarjetasActive = true;
		}
		else if (panelTarjetasActive)
		{
			PanelTarjetas.GetComponent<Animator>().SetBool("OpenB", false);
			BarraPresionado[2].SetActive(false);
			NuevasNotificaciones[0].SetActive(false);
			panelTarjetasActive = false;
		}
	}
    #endregion

    //Abrir_Cerrar Notificaciones////////////////////////////////////
    #region Notificaciones
    public void Abrir_CerrarNotificaciones() {
		if (!notificacionesActive)
		{
			CerraPestañasAbiertas();
			Notificaciones.GetComponent<Animator>().SetTrigger("Open");
			Notificaciones.GetComponent<Animator>().SetBool("OpenB", true);
			NuevasNotificaciones[1].SetActive(false);
			BarraPresionado[3].SetActive(true);
			notificacionesActive = true;
		}
		else if (notificacionesActive) {
			Notificaciones.GetComponent<Animator>().SetBool("OpenB", false);
			NuevasNotificaciones[1].SetActive(false);
			BarraPresionado[3].SetActive(false);
			notificacionesActive = false;
		}
	}
    #endregion

    //Abrir_Cerrar Mapa//////////////////////////////////////////////
    #region Mapa
    public void Abrir_CerrarMapa() {
		if (!mapaActive) {
			CerraPestañasAbiertas();
			Mapa.SetActive(true);
			BarraPresionado[4].SetActive(true);
			mapaActive = true;
		}
		else if (mapaActive) {
			IrAqui.SetActive(false);
			childText.SetActive(false);
			Mapa.SetActive(false);
			BarraPresionado[4].SetActive(false);
			mapaActive = false;
		}
	
	}
	#endregion

	//Mis Datos
	#region Mis Datos
	public void Abrir_CerrarMisDatos() {
		if (!misDatosActive)
		{
			CerraPestañasAbiertas();
			MisDatos.SetActive(true);
			BarraPresionado[5].SetActive(true);
			misDatosActive = true;
		}
		else if (misDatosActive) {
			StartCoroutine(WaitAnimations(MisDatos));
			//MisDatos.SetActive(false);
			BarraPresionado[5].SetActive(false);
			misDatosActive = false;
		}
	}
    #endregion

	//Conferencias
    #region Conferencias
    public void Abrir_CerrarConferencias()
	{
		if (!conferenciasActive)
		{
			CerraPestañasAbiertas();
			Conferencias.SetActive(true);
			BarraPresionado[6].SetActive(true);
			conferenciasActive = true;
		}
		else if (conferenciasActive)
		{
			StartCoroutine(WaitAnimations(Conferencias));
			BarraPresionado[6].SetActive(false);
			conferenciasActive = false;
		}
	}
    #endregion

    //Mutear sonido
    #region Sonido
    public void Activar_DesactivarSonido() {
		if (soundActive)
		{
			GameObject.Find("RPG Camera").transform.GetChild(0).GetComponent<AudioSource>().Pause();
			NotSound.SetActive(true);
			soundActive = false;
		}
		else if (!soundActive)
		{
			GameObject.Find("RPG Camera").transform.GetChild(0).GetComponent<AudioSource>().UnPause();
			NotSound.SetActive(false);
			soundActive = true;
		}
	}
    #endregion

    //Salir al menu principal////////////////////////////////////////
    #region Salir
    public void SalirAlMenu()
	{
		Destroy(PhotonRoom.room.gameObject);
		StartCoroutine(WaitDisconnect());
	}
	IEnumerator WaitDisconnect()
	{
		PhotonNetwork.Disconnect();
		while (PhotonNetwork.IsConnected)
			yield return null;
		UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
	}
    #endregion
    void CerraPestañasAbiertas() {
		foreach (GameObject barra in BarraPresionado)
			barra.SetActive(false);
		if(buscadorActive)
			Abrir_CerrarBuscador();
		if (tutorialActive)
			CloseTutorial();
		if (panelTarjetasActive)
			Abrir_CerrarTarjetas();
		if (notificacionesActive)
			Abrir_CerrarNotificaciones();
		if (mapaActive)
			Abrir_CerrarMapa();
		if (misDatosActive)
			Abrir_CerrarMisDatos();
		if (conferenciasActive)
			Abrir_CerrarConferencias();
	}

	public bool CheckAllClose() {
		if (!buscadorActive && !tutorialActive && !panelTarjetasActive && !notificacionesActive && !mapaActive && !misDatosActive && !conferenciasActive)
		{
			return true;
		}
		else
			return false;
	}

	IEnumerator WaitAnimations(GameObject panel) {
		panel.GetComponent<Animator>().SetTrigger("Close");
		yield return new WaitForSeconds(0.50f);
		panel.SetActive(false);
	}
}
