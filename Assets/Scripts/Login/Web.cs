using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Web : MonoBehaviour
{
    public GameObject UsuarioCreadoPanel, RegistrerPanel;
    public Login LoginScript;

    [SerializeField] private TMP_Text ErrorTMP, ErrorRegistroTMP, SuccessfulTMP;
    [SerializeField] private Toggle RecordarLogueo;


    private string EmailLogin, PasswordLogin;
    private bool _remember;
    
    void Start()
    {
        ResetFields();
        FindLogin();
    }

    public void OnInputClick() {
        ResetFields();
    }


    /*
     Comment: Limpia los text en los TMP_Text de la escena.
     Pre:
     Post:
    */
    void ResetFields(){
        ErrorTMP.text = "";
        ErrorRegistroTMP.text = "";
        SuccessfulTMP.text = "";
    }

    /*
     Comment: Muestra en pantalla para el usuario, mediante un TMP_Text, el error que se haya generado al loguear.
     Pre: Recibe el string del error ha mostrar.
     Post: Modifica el text del ErrorTMP(TMP_Text) para que sea el error enviado.
    */
    public void ErrorDisplay(string error){
        ErrorTMP.text = error;
    }

    #region AutoLogin
    /*
     Comment: Guarda en PlayerPrefs los datos de logueo del usuario si este lo desea, mediante la activacion de un Toggle.
     Pre: Recibe el mail y la contraseña(ambos string) para ser guradados en PlayerPrefs.
     Post: Guarda en PlayerPrefs los datos, o los elimina en caso de que le usaurio no lo desee.
    */
    public void Remember()
    {
        _remember = RecordarLogueo.isOn;
        if (_remember)
        {
            PlayerPrefs.SetString("email", LoginScript.UsernameInput.text);
            PlayerPrefs.SetString("pass", LoginScript.PasswordInput.text);
            PlayerPrefs.SetInt("remember", 1);
        }
        else
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void FindLogin()
    {
        if (PlayerPrefs.HasKey("email"))
        {
            LoginScript.UsernameInput.text = PlayerPrefs.GetString("email");
            LoginScript.PasswordInput.text = PlayerPrefs.GetString("pass");
            StartCoroutine(Login(LoginScript.UsernameInput.text, LoginScript.PasswordInput.text));
        }
        else
        {
            LoginScript.UsernameInput.text = "";
            LoginScript.PasswordInput.text = "";
        }
    }
    #endregion

    /*
     Comment: Recibe los parametros para el logueo: usename y password (string), ejecuta ConnectDB y, con el return de la data,
     guarda la infoprmacion del susuario en UserInfo, instanciadolo.
     Pre: Recibe usename y password (string) desde los inputs de la escena.
     Post: Setea la informacion de UserInfo mediante la ddevolucion de la data desde la DataBase.
    */
    public IEnumerator Login(string username, string password)
    {
        Remember();
        ResetFields();
        if (username != "" && password != "")
        {
            WWWForm form = new WWWForm();
            form.AddField("loginUser", username);
            form.AddField("loginPass", password);

            StartCoroutine(Main.Instance.ConnectDB("https://teckdes.com/ExpoVirtual/VirtualExpo/LoginINJnewdb.php", form, (data) => {
                if (data == "Usuario o mail inexistente." || data == "Contraseña incorrecta."){
                    ErrorDisplay(data);
                }
                else{
                    SuccessfulTMP.text = "Logueando... Aguarde.";
                    Main.Instance.UserInfo.SetInfo(data.Split('|')[7], password, data.Split('|')[5]);
                    Main.Instance.UserInfo.SetID(data.Split('|')[0]);
                    Main.Instance.UserInfo.SetLevel(data.Split('|')[1]);
                    Main.Instance.UserInfo.SetEmail(data.Split('|')[2], data.Split('|')[6]);
                    Main.Instance.UserInfo.SetEmpresa(data.Split('|')[3]);
                    Main.Instance.UserInfo.SetPuesto(data.Split('|')[4]);
                }
            }));
        }
        else
        {
            ErrorDisplay("Complete todos los campos.");
        }
        
        yield return null;
    }
    
    /*
     Comment: Recibe los parametros para el registro: usename, password, nombre y apellid, email, empresa y puesto (string), ejecuta ConnectDB y, con el return de la data,
     guarda la infoprmacion del susuario en UserInfo, instanciadolo.
     Pre: Recibe usename, password, nombre y apellid, email, empresa y puesto (string) desde los inputs de la escena.
     Post: Conectna con la base de datos para la creacion del registro y devulve a la pantalla principal para el logueo.
    */
    public IEnumerator RegisterUser(string username, string nombreapellido, string password,string email,string empresa,string puesto)
    {
        ResetFields();
        if (username != "" && nombreapellido != "" && password != "" && email != "" && empresa != "" && puesto != "")
        {
            WWWForm form = new WWWForm();
            form.AddField("loginUser", username);
            form.AddField("loginNombreApellido", nombreapellido);
            form.AddField("loginPass", password);
            form.AddField("loginEmail", email);
            form.AddField("loginEmpresa", empresa);
            form.AddField("loginPuesto", puesto);

            StartCoroutine(Main.Instance.ConnectDB("https://teckdes.com/ExpoVirtual/VirtualExpo/RegisterUser.php", form, (data) => {
                Debug.Log(data);
                
                if (data == "Usuario Creado Vuelve y Loguea"){
                    Main.Instance.UserInfo.SetID(data);
                    StartCoroutine(UsuariosRegistradosPhP());
                    RegistrerPanel.SetActive(false);
                    UsuarioCreadoPanel.SetActive(true);
                    ErrorRegistroTMP.text = "";
                }
                else{
                    ErrorDisplay(data);
                }
            }));
        }
        else {
            ErrorRegistroTMP.text = "Complete todos los campos.";
        }
        yield return null;
    }
    
    public IEnumerator EmailPassRecover(string email) {
        ResetFields();
        WWWForm form = new WWWForm();
        form.AddField("loginEmail", email);

        StartCoroutine(Main.Instance.ConnectDB("https://teckdes.com/ExpoVirtual/VirtualExpo/Email-Pass-Recover.php", form, (data) => {
            Debug.Log(data);        
            
            Main.Instance.UserInfo.SetID(data.Split('|')[0]);
            Main.Instance.UserInfo.SetDataRecover(data);
        }));
        
        yield return null;
    }

    public IEnumerator UsuariosRegistradosPhP()
    {
        WWWForm form = new WWWForm();
        form.AddField("UsuariosRegistrados", 1);

        StartCoroutine(Main.Instance.ConnectDB("https://teckdes.com/ExpoVirtual/VirtualExpo/AnalyticsPrincipal.php", form, (data) => {
                Debug.Log(data);
        }));
        yield return null;
    }
}
