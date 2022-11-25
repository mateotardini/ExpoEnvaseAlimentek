using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Web : MonoBehaviour
{
    public GameObject UsuarioCreadoPanel, RegistrerPanel;
    public TMP_Text ErrorTMP, ErrorRegistroTMP, SuccessfulTMP;
    public string EmailLogin, PasswordLogin;

    public Login LoginScript;
    bool Recordar;
    public Toggle RecordarLogueo;

    void Start()
    {
        ErrorTMP.text = "";
        ErrorRegistroTMP.text = "";
        SuccessfulTMP.text = "";
        FindLogin();
    }

    public void OnInputClick() {
        ErrorTMP.text = "";
        ErrorRegistroTMP.text = "";
        SuccessfulTMP.text = "";
    }

    #region AutoLogin
    public void Remember()
    {
        Recordar = RecordarLogueo.isOn;
        if (Recordar)
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

    public IEnumerator Login(string username, string password)
    {
        Remember();
        if (username != "" && password != "")
        {
            WWWForm form = new WWWForm();
            form.AddField("loginUser", username);
            form.AddField("loginPass", password);

            using (UnityWebRequest www = UnityWebRequest.Post("https://teckdes.com/ExpoVirtual/VirtualExpo/LoginINJnewdb.php", form))
            {
                yield return www.SendWebRequest();


                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                    SuccessfulTMP.text = "";
                    ErrorTMP.text = www.error;
                }
                else
                {
                    //Debug.Log(www.downloadHandler.text);
                    if (www.downloadHandler.text == "Usuario o mail inexistente." || www.downloadHandler.text == "Contraseña incorrecta.")
                        ErrorTMP.text = www.downloadHandler.text;
                    else
                    {
                        ErrorTMP.text = "";
                        SuccessfulTMP.text = "Logueando... Aguarde.";
                        Main.Instance.UserInfo.SetInfo(www.downloadHandler.text.Split('|')[7], password, www.downloadHandler.text.Split('|')[5]);
                        Main.Instance.UserInfo.SetID(www.downloadHandler.text.Split('|')[0]);
                        Main.Instance.UserInfo.SetLevel(www.downloadHandler.text.Split('|')[1]);
                        Main.Instance.UserInfo.SetEmail(www.downloadHandler.text.Split('|')[2], www.downloadHandler.text.Split('|')[6]);
                        Main.Instance.UserInfo.SetEmpresa(www.downloadHandler.text.Split('|')[3]);
                        Main.Instance.UserInfo.SetPuesto(www.downloadHandler.text.Split('|')[4]);
                    }
                }
            }
        }
        else
        {
            ErrorTMP.text = "Complete todos los campos.";
        }
    }

    public IEnumerator RegisterUser(string username, string nombreapellido, string password,string email,string empresa,string puesto)
    {
        if (username != "" && nombreapellido != "" && password != "" && email != "" && empresa != "" && puesto != "")
        {
            WWWForm form = new WWWForm();
            form.AddField("loginUser", username);
            form.AddField("loginNombreApellido", nombreapellido);
            form.AddField("loginPass", password);
            form.AddField("loginEmail", email);
            form.AddField("loginEmpresa", empresa);
            form.AddField("loginPuesto", puesto);


            using (UnityWebRequest www = UnityWebRequest.Post("https://teckdes.com/ExpoVirtual/VirtualExpo/RegisterUser.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                    ErrorRegistroTMP.text = www.error;
                }
                else
                {
                    Debug.Log(www.downloadHandler.text);
                    if (www.downloadHandler.text == "Usuario Creado Vuelve y Loguea")
                    {
                        Main.Instance.UserInfo.SetID(www.downloadHandler.text);
                        StartCoroutine(UsuariosRegistradosPhP());
                        RegistrerPanel.SetActive(false);
                        UsuarioCreadoPanel.SetActive(true);
                        ErrorRegistroTMP.text = "";
                    }
                    else
                        ErrorRegistroTMP.text = www.downloadHandler.text;
                }
            }
        }
        else {
            ErrorRegistroTMP.text = "Complete todos los campos.";
        }
    }
    
    public IEnumerator EmailPassRecover(string email) {

        WWWForm form = new WWWForm();
        form.AddField("loginEmail", email);

        using (UnityWebRequest www = UnityWebRequest.Post("https://teckdes.com/ExpoVirtual/VirtualExpo/Email-Pass-Recover.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Main.Instance.UserInfo.SetID(www.downloadHandler.text.Split('|')[0]);
                Main.Instance.UserInfo.SetDataRecover(www.downloadHandler.text);
            }
        }
    }
    public IEnumerator UsuariosRegistradosPhP()
    {
        WWWForm form = new WWWForm();
        form.AddField("UsuariosRegistrados", 1);

        using (UnityWebRequest www = UnityWebRequest.Post("https://teckdes.com/ExpoVirtual/VirtualExpo/AnalyticsPrincipal.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
}
