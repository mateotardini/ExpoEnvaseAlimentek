using System.Collections;
using UnityEngine;

public class LoadChats : MonoBehaviour
{
    [SerializeField]private Transform TarjetasOrder;
    [SerializeField]private GameObject ChatPrefab, BussinesCardPrefab;
    private void Start()
    {
        StartCoroutine(LoadChatsDB());
        StartCoroutine(LoadBusinessCardsDB());
    }

    #region Carga de Chats
    /*
     Comment:  Llama a la funcion ConnectDB para obtener la informacion de la base de datos sobre los chats que el usurio ha generado en sus sesiones anteriores.
     Pre: Se ejecuta con el Start.
     Post: Crea un GO desde un prefab de chat por cada chat que haya generado el usuario.
    */
    public IEnumerator LoadChatsDB()
    {
        WWWForm form = new WWWForm();
        form.AddField("User", UserInfo.UserName);

        StartCoroutine(Main.Instance.ConnectDB("https://teckdes.com/ExpoVirtual/VirtualExpo/Load-Chats.php", form, (data) => {
            int numeroDeChats = int.Parse(data.Split('|')[0]);
            if (numeroDeChats != 0)
            {
                for (int i = 0; i < numeroDeChats; i++)
                {   //Creo un objetco chat por chat guardado en la tabla.
                    GameObject NewChat = GameObject.Instantiate(ChatPrefab, Vector2.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("ChatOrder").transform);
                    PhotonChatManager NewChatScript = NewChat.GetComponent<PhotonChatManager>();

                    string User = data.Split('|')[1 + i*7];
                    string Empresa = data.Split('|')[2 + i*7];
                    string Email =data.Split('|')[3 + i*7];
                    string AnotherUser = data.Split('|')[4 + i*7];
                    string AnotherUserEmpresa = data.Split('|')[5 + i*7];
                    string AnotherUserEmail = data.Split('|')[6 + i*7];
                    string ChatText = data.Split('|')[7 + i*7];

                    if (User == UserInfo.UserName)       //Si el dato "User" de la tabla en mysql es mi nombre.
                    {
                        NewChatScript.SetDataChat(AnotherUser, AnotherUserEmpresa, AnotherUserEmail, ChatText);
                    }
                    else if (AnotherUser == UserInfo.UserName) //Si el dato "AnotherUser" de la tavbla en mysql es mi nombre.
                    {
                        NewChatScript.SetDataChat(User, Empresa, Email, ChatText);
                    }
                }
            }
        }));
        yield return null;
    }
    #endregion


    #region Carga de BusinessCards
    
    /*
     Comment:  Llama a la funcion ConnectDB para obtener la informacion de la base de datos sobre las tarjetas de presentacion que el usurio ha 
     generado en sus sesiones anteriores.
     Pre: Se ejecuta con el Start.
     Post: Crea un GO desde un prefab de DNIPrefab por cada tarjeta que haya generado el usuario.
    */
    IEnumerator LoadBusinessCardsDB() {
        WWWForm form = new WWWForm();
        form.AddField("UserEmail", UserInfo.Email);

        StartCoroutine(Main.Instance.ConnectDB("https://teckdes.com/ExpoVirtual/VirtualExpo/Load-DNIs.php", form, (data) => {
            int numBusinessCard = int.Parse(data.Split('|')[0]);
            if (numBusinessCard != 0)
            {
                for (int i = 0; i < numBusinessCard; i++)
                {   
                    GameObject newBusinessCard = GameObject.Instantiate(BussinesCardPrefab, Vector2.zero, Quaternion.identity, TarjetasOrder);
                    BusinessCardScript newBusinessCardScript = newBusinessCard.GetComponent<BusinessCardScript>();
                    newBusinessCardScript.SetDataBusinessCard(data.Split('|')[2 + i * 6], data.Split('|')[3 + i * 6],
                     data.Split('|')[4 + i * 6], data.Split('|')[5 + i * 6],data.Split('|')[6 + i * 6]);
                }
            }
        }));
        yield return null;
    }
    #endregion
}

