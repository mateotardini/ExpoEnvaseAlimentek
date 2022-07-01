using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    public InputField UsernameInput;
    public InputField NombreApellidoInput;
    public InputField PasswordInput;
    public InputField EmailInput;
    public InputField EmpresaInput;
    public InputField PuestoInput;
    public Button RegisterButton;

    void Start()
    {
        RegisterButton.onClick.AddListener(() => {
            StartCoroutine(Main.Instance.Web.RegisterUser(UsernameInput.text,NombreApellidoInput.text, PasswordInput.text,EmailInput.text,EmpresaInput.text,PuestoInput.text));
        });
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            StartCoroutine(Main.Instance.Web.RegisterUser(UsernameInput.text, NombreApellidoInput.text, PasswordInput.text, EmailInput.text, EmpresaInput.text, PuestoInput.text));
    }
    public void UpdateFieldPassword()
    {
        string text = PasswordInput.text;//get text from input field
        text = text.Replace(" ", "");//fliter spaces from text
        PasswordInput.text = text;//set the text in the input field to the filtered text
    }
    public void UpdateFieldEmail()
    {
        string text = EmailInput.text;//get text from input field
        text = text.Replace(" ", "");//fliter spaces from text
        EmailInput.text = text;//set the text in the input field to the filtered text
    }
}
