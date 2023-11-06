using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Main : MonoBehaviour
{
    public static Main Instance;
    public Web Web;
    public UserInfo UserInfo;

    void Awake()
    {
        Instance = this;
        Web = GetComponent<Web>();
        UserInfo = GetComponent<UserInfo>();

        GameObject AMMRoomController = GameObject.Find("AMMRoomController");
        if(AMMRoomController != null)
            Destroy(AMMRoomController);
    }

    
    /*
     Comment: Conecta con la Database mediante el link asignado y con los datos form.
     Pre: Recibe link al php (string) y el form (WWWForm).
     Post: Devuelve la data (www.downloadHandler.text) en formato string para el uso que se desee.
    */
    public IEnumerator ConnectDB(string link, WWWForm form, System.Action<string> data){
        using (UnityWebRequest www = UnityWebRequest.Post(link, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Web.ErrorDisplay(www.error);
                Debug.Log(www.error);
            }
            else
            {
                data(www.downloadHandler.text);
            }
        }
    }
}
