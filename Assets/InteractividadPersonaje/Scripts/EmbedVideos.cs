using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EmbedVideos : MonoBehaviour
{
    public int NumeroDeStand;
    public int NumeroDeVideo;
    AnalitycsTest analitycs;
    string QuienesDescargaronFolletos;

    private void Start()
    {
        if (NumeroDeStand > 100 && NumeroDeVideo != 0)
        {
            analitycs = transform.parent.transform.GetChild(0).gameObject.GetComponent<AnalitycsTest>();
            QuienesDescargaronFolletos = UserInfo.UserName + " | " + UserInfo.Empresa + " | " + UserInfo.Email;
        }
    }

    public void PlayEmbedVideo()
    {
        if (NumeroDeStand != 0 && NumeroDeVideo != 0)
            StartCoroutine(EmbedVideosPhP());
        if(analitycs != null)
            analitycs.ClickVideos();
    }

    [System.Obsolete]
    public IEnumerator EmbedVideosPhP()
    {
        WWWForm form = new WWWForm();

        form.AddField("NumeroDeStand", NumeroDeStand);
        form.AddField("URLVideo", "URLVideo" + NumeroDeVideo.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post("https://expovirtual.com.ar/VirtualExpo/EmbedVideos.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
#if UNITY_WEBGL
                if(NumeroDeStand != 2)
                    Application.ExternalEval("window.open('" + "https://expovirtual.com.ar/VirtualExpo/EmbedVideos.php" + "' , 'video','width=560,height=315,left=20,top=20')");
                else
                    Application.ExternalEval("window.open('" + "https://expovirtual.com.ar/VirtualExpo/EmbedVideos.php" + "' , 'video','width=560,height=896,left=20,top=20')");
#else
                Application.OpenURL("https://expovirtual.com.ar/VirtualExpo/EmbedVideosExe.php?NumeroDeStand="+NumeroDeStand.ToString()+"&URLVideo=URLVideo"+NumeroDeVideo.ToString());
#endif
            }
        }
    }
}
