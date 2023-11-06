using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;

public class UserInfo : MonoBehaviour
{
    public GameObject PanelMensaje;
    [SerializeField] private string UserID;
    public static string UserName;
    public static string Name;
    private string UserPassword;
    public static string Level;
    public static string NombreApellido;
    public static string Email;
    public static string Telphone;
    public static string Empresa;
    public static string Puesto;
    public static string TipoDeUsuario;
    public string DatosRecover;
    public GameObject Panelrecover;
    public TMP_Text ErrorTMP, SuccessfulTMP;

    public static bool onScroll;

    private PhotonView PV;


    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    public void SetInfo(string username, string userpassword, string name) {
        UserName = username;
        UserPassword = userpassword;
        Name = name;
    }
    public void SetID(string id) {
        UserID = id;
    }


    //Carga de nivel con acreditacion de usuario
    #region Acreditacion
    public void SetLevel(string level)
    {
        Level = level;
        if (Level != null) 
        {
              
          Invoke("LoadScene",3.0f);
            
        }

    }
    public void LoadScene()
    {
        
       SceneManager.LoadSceneAsync("Menu");
        
    }

    public void SetNombreApellido(string nombreapellido)
    {
        NombreApellido = nombreapellido;
    }
    public void SetEmail(string email, string tel) 
    {
        Email = email;
        Telphone = tel;
    }
    public void SetEmpresa(string empresa)
    {
        Empresa = empresa;
    }
    public void SetPuesto(string puesto)
    {
        Puesto = puesto;
    }
    public void SetDataRecover(string datosrecuperados)
    {
        DatosRecover = datosrecuperados;
    }
    #endregion

    //----------------------------------------------------------------------------------------------------

    private string message;
    private bool _used = false;
    private double displayTime = 2.0;
    [SerializeField] private TMP_Text MessageLogRegInfoError;

    public void OnClick()
    {
        if (!_used)
        {
            _used = true;
            displayTime = 5;
            PanelMensaje.SetActive(true);
        }
    }
    void Update()
    {
        MessageLogRegInfoError.text = message;
        message = UserID;

        if (_used && message != "")
        {
            displayTime -= Time.deltaTime;
            
            if (string.IsNullOrEmpty(Level))
            {
                message = UserID;
                ErrorTMP.text = UserID;
            }
            else
            {
                SuccessfulTMP.text = "Logueando... Aguarde.";
                message = "LOGUEANDO AGUARDE";
            }
        }
        else
        {
            message = ""; 
        }
        if (displayTime <= 0.0)
        {
            displayTime = 0;
            _used = false;
            UserID = "";
            PanelMensaje.SetActive(false);
        }
    }
}
