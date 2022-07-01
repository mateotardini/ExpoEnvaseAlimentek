using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;

public class UserInfo : MonoBehaviour
{
    public GameObject PanelMensaje;
    public string UserID; //{ get; set;}
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

    public string message;
    public bool used = false;
    public double displayTime = 2.0;
    public TMP_Text MessageLogRegInfoError;

    public void OnClick()
    {
        if (!used)
        {
            used = true;
            displayTime = 5;
            PanelMensaje.SetActive(true);
        }
    }
    void Update()
    {
        MessageLogRegInfoError.text = message;
        message = UserID; // in the UserID i have the id and the text when the userid is not found or dosent exist
//        ReceiverMailAdress = emailPassRecover.text;

        if (used && message != "")
        {

            displayTime -= Time.deltaTime;

            //and here i use the level information in string var to compare if have or not one value(if i create an new user they
            //have a number of level like 1 or 2 or 3) then when i try to loggin have to do that to show the info to the user. IF the 
            //level have a value in the UserInfo y have de function to compare the level an put ready to loggin in and jumpo to the "VS"
            
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
            if (Panelrecover.activeSelf)
            {
                if (UserID != DatosRecover && enviar != false)
                {

                    message = "Email enviado con exito revise su correo";
                    SendAnEmail();
                }
            }
        }
        else
        {
            message = ""; //Delet de ID to the next click get a new ID to show and no the last one before change (Esto se podria evitar y solo borrar el message asi no borra el id por si se necesita usar luego)
        }
        if (displayTime <= 0.0)
        {
            displayTime = 0;
            used = false;
            UserID = "";
            PanelMensaje.SetActive(false);
        }
    }

    //---------Envio de recuperacion de datos--------------
    #region Mail REcuperacion desde Unity
    public string SenderMailAdress = "todoboke@gmail.com";
    public string SenderPassword = "bocapasion12";
    private string ReceiverMailAdress;
    // public GameObject loading;
    public InputField emailPassRecover;
    public bool enviar;

    public void SendAnEmail()
    {
       
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(SenderMailAdress);
        mail.To.Add(ReceiverMailAdress);
        mail.Subject = "Hola esto es una Prueba";
        mail.Body = "Estos son los datos que quiere recuperar.";
        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com", 25);
        smtpServer.Port = 25;
        smtpServer.Credentials = new NetworkCredential(SenderMailAdress, SenderPassword) as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {

            return true;
        };
        smtpServer.Send(mail);
        print("Sent.");
       
    }
    #endregion
}
