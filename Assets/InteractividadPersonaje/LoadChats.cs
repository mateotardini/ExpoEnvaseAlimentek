using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoadChats : MonoBehaviour
{
    public Transform TarjetasOrder;
    public GameObject ChatPrefab, DNIPrefab;
    private void Start()
    {
        LoadChatString();
        LoadDNIs();
    }

    public void LoadDNIs() {
        StartCoroutine(LoadDNIsStringPhP());
    }

    //Carga de chats desde database
    #region Carga de Chats
    public void LoadChatString()
    {
        StartCoroutine(LoadChatStringPhP());
    }

    public IEnumerator LoadChatStringPhP()
    {
        WWWForm form = new WWWForm();
        form.AddField("User", UserInfo.UserName);

        using (UnityWebRequest www = UnityWebRequest.Post("https://expovirtual.com.ar/VirtualExpo/Load-Chats.php", form))
        {
            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {                                                //Descargo todos los datos de los chats guardados en mysql.
                int numeroDeChats = int.Parse(www.downloadHandler.text.Split('|')[0]);
                if (numeroDeChats != 0)
                {
                    for (int i = 0; i < numeroDeChats; i++)
                    {   //Creo un objetco chat por chat guardado en la tabla.
                        GameObject NewChat = GameObject.Instantiate(ChatPrefab, Vector2.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("ChatOrder").transform);
                        NewChat.GetComponent<RectTransform>().pivot = Vector2.zero;
                        NewChat.transform.position = Vector2.zero;
                        PhotonChatManager NewChatScript = NewChat.GetComponent<PhotonChatManager>();
                        NewChatScript.CountInAnalitycs = true;
                        string User = www.downloadHandler.text.Split('|')[1 + i*7];
                        string Empresa = www.downloadHandler.text.Split('|')[2 + i*7];
                        string Email = www.downloadHandler.text.Split('|')[3 + i*7];
                        string AnotherUser = www.downloadHandler.text.Split('|')[4 + i*7];
                        string AnotherUserEmpresa = www.downloadHandler.text.Split('|')[5 + i*7];
                        string AnotherUserEmail = www.downloadHandler.text.Split('|')[6 + i*7];
                        string ChatText = www.downloadHandler.text.Split('|')[7 + i*7];
                        if (User == UserInfo.UserName)       //Si el dato "User" de la tavbla en mysql es mi nombre.
                        {
                            NewChat.name = "Chat" + AnotherUser;
                            NewChatScript.Receiver = AnotherUser;
                            NewChatScript.anotherUserEmpresa = AnotherUserEmpresa;
                            NewChatScript.anotherUserEmail = AnotherUserEmail;
                            NewChatScript.ChatText = ChatText;
                            NewChatScript.ChatConnectOnClick();
                            NewChat.transform.GetChild(0).gameObject.SetActive(true);

                        }
                        else if (AnotherUser == UserInfo.UserName) //Si el dato "AnotherUser" de la tavbla en mysql es mi nombre.
                        {
                            NewChat.name = "Chat" + User;
                            NewChatScript.Receiver = User;
                            NewChatScript.anotherUserEmpresa = Empresa;
                            NewChatScript.anotherUserEmail = Email;
                            NewChatScript.ChatText = ChatText;
                            NewChatScript.ChatConnectOnClick();
                            NewChat.transform.GetChild(0).gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
    }
    #endregion

    //Carga de tarjetas desde database
    #region Carga de DNIS
    IEnumerator LoadDNIsStringPhP() {
        WWWForm form = new WWWForm();
        form.AddField("UserEmail", UserInfo.Email);

        using (UnityWebRequest www = UnityWebRequest.Post("https://expovirtual.com.ar/VirtualExpo/Load-DNIs.php", form))
        {
            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {    //Descargo todos los datos de los DNIs guardados en mysql.
                int numeroDeDNIs = int.Parse(www.downloadHandler.text.Split('|')[0]);
                if (numeroDeDNIs != 0)
                {
                    for (int i = 0; i < numeroDeDNIs; i++)
                    {   //Creo un objetco DNI por chat guardado en la tabla.
                        GameObject NewDNI = GameObject.Instantiate(DNIPrefab, Vector2.zero, Quaternion.identity, TarjetasOrder);
                        NewDNI.transform.localPosition = Vector2.zero;
                        NewDNI.transform.position = Vector2.zero;
                        DNIScript NewDNIScript = NewDNI.GetComponent<DNIScript>();
                        string User = www.downloadHandler.text.Split('|')[1 + i * 6];
                        string AnotherUser = www.downloadHandler.text.Split('|')[2 + i * 6];
                        string AnotherUserName = www.downloadHandler.text.Split('|')[3 + i * 6];
                        string AnotherUserEmpresa = www.downloadHandler.text.Split('|')[4 + i * 6];
                        string AnotherUserEmail = www.downloadHandler.text.Split('|')[5 + i * 6];
                        string AnotherUserTel = www.downloadHandler.text.Split('|')[6 + i * 6];
                        NewDNIScript.anotherName = AnotherUser;
                        NewDNIScript.anotherUserName = AnotherUserName;
                        NewDNIScript.anotherEmpresa = AnotherUserEmpresa;
                        NewDNIScript.anotherEmail = AnotherUserEmail;
                        NewDNIScript.anotherTel = AnotherUserTel;
                        NewDNI.name = "DNI" + AnotherUserName;
                        NewDNIScript.SaveButton.SetActive(false);
                        NewDNIScript.Saved.SetActive(true);
                        NewDNIScript.Delete.SetActive(true);
                    }
                }
            }
        }

    }
    #endregion
}

