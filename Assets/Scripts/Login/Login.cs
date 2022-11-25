using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Login : MonoBehaviour
{
    public TMP_InputField UsernameInput;
    public TMP_InputField PasswordInput;
    public Button LoginButton;
    public UserInfo userInfo;

    // Start is called before the first frame update
    void Start()
    {
        LoginButton.onClick.AddListener(() => {
                StartCoroutine(Main.Instance.Web.Login(UsernameInput.text, PasswordInput.text));              
        });
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(Main.Instance.Web.Login(UsernameInput.text, PasswordInput.text));
        }
    }
    public void UpdateFieldPassword()
    {
        string text = PasswordInput.text;//get text from input field
        text = text.Replace(" ", "");//fliter spaces from text
        PasswordInput.text = text;//set the text in the input field to the filtered text
    }

    public void Registrarse() {
#if UNITY_WEBGL
        Application.ExternalEval("window.open('https://teckdes.com/ExpoVirtual/Esp/acreditacion-v.html' , '_blank')");
#else
        Application.OpenURL("https://teckdes.com/ExpoVirtual/Esp/acreditacion-v.html");
#endif

    }
}
