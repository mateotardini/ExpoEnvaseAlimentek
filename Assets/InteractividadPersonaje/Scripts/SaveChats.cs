using UnityEngine;

public class SaveChats : MonoBehaviour
{
    // Start is called before the first frame update
    public PhotonChatManager PhotonChatScript;

    public void SaveChatString() {
        PhotonChatScript.GetChatText();
        string chatText = PhotonChatScript.ChatText;
        chatText = chatText.Replace("\n" + UserInfo.UserName + ": se ha unido al chat.", "");
        chatText = chatText.Replace("\n" + PhotonChatScript.Receiver + ": se ha unido al chat.", "");

        WWWForm form = new WWWForm();
        form.AddField("User", UserInfo.UserName);
        form.AddField("Empresa", UserInfo.Empresa);
        form.AddField("Email", UserInfo.Email);
        form.AddField("AnotherUser", PhotonChatScript.Receiver);
        form.AddField("AnotherUserEmpresa", PhotonChatScript.anotherUserEmpresa);
        form.AddField("AnotherUserEmail", PhotonChatScript.anotherUserEmail);
        form.AddField("ChatText", chatText);

        StartCoroutine(Main.Instance.ConnectDB("https://teckdes.com/ExpoVirtual/VirtualExpo/Save-Chats.php", form, (data) => {}));
    }


    public void DeleteChat() {
        WWWForm form = new WWWForm();
        form.AddField("UserEmail", UserInfo.Email);
        form.AddField("AnotherUserEmail", PhotonChatScript.anotherUserEmail);
        form.AddField ("DNIorChat", "Chat");

        StartCoroutine(Main.Instance.ConnectDB("https://teckdes.com/ExpoVirtual/VirtualExpo/Delete_DNI_Chat.php", form, (data) => {}));
    }
}
