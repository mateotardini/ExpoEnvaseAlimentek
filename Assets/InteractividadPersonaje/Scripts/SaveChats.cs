using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SaveChats : MonoBehaviour
{
    // Start is called before the first frame update
    public PhotonChatManager PhotonChatScript;

    public void SaveChatString() {
        PhotonChatScript.GetChatText();
        string chatText = PhotonChatScript.ChatText;
        chatText = chatText.Replace("\n" + UserInfo.UserName + ": se ha unido al chat.", "");
        chatText = chatText.Replace("\n" + PhotonChatScript.Receiver + ": se ha unido al chat.", "");
        StartCoroutine(SaveChatStringPhP(chatText));
    }

    public IEnumerator SaveChatStringPhP(string chatText)
    {
        WWWForm form = new WWWForm();
        form.AddField("User", UserInfo.UserName);
        form.AddField("Empresa", UserInfo.Empresa);
        form.AddField("Email", UserInfo.Email);
        form.AddField("AnotherUser", PhotonChatScript.Receiver);
        form.AddField("AnotherUserEmpresa", PhotonChatScript.anotherUserEmpresa);
        form.AddField("AnotherUserEmail", PhotonChatScript.anotherUserEmail);
        form.AddField("ChatText", chatText);

        using (UnityWebRequest www = UnityWebRequest.Post("https://teckdes.com/ExpoVirtual/VirtualExpo/Save-Chats.php", form))
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

    public void DeleteChat() {
        StartCoroutine(DeleteChatPhP());
    }
    public IEnumerator DeleteChatPhP()
    {
        WWWForm form = new WWWForm();
        form.AddField("UserEmail", UserInfo.Email);
        form.AddField("AnotherUserEmail", PhotonChatScript.anotherUserEmail);
        form.AddField ("DNIorChat", "Chat");

        using (UnityWebRequest www = UnityWebRequest.Post("https://teckdes.com/ExpoVirtual/VirtualExpo/Delete_DNI_Chat.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
        }
    }
}
