using UnityEngine;

public class EmbedVideos : MonoBehaviour
{
    [SerializeField]private int NumeroDeStand;
    [SerializeField]private int NumeroDeVideo;
    AnalitycsTest analitycs;
    private string QuienesDescargaronFolletos;

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
        if (NumeroDeStand != 0 && NumeroDeVideo != 0){
            WWWForm form = new WWWForm();
            form.AddField("NumeroDeStand", NumeroDeStand);
            form.AddField("URLVideo", "URLVideo" + NumeroDeVideo.ToString());

            StartCoroutine(Main.Instance.ConnectDB("https://teckdes.com/ExpoVirtual/VirtualExpo/EmbedVideos.php", form, (data) => {
                #if UNITY_WEBGL
                if(NumeroDeStand != 2)
                    Application.ExternalEval("window.open('" + "https://teckdes.com/ExpoVirtual/VirtualExpo/EmbedVideos.php" + "' , 'video','width=560,height=315,left=20,top=20')");
                else
                    Application.ExternalEval("window.open('" + "https://teckdes.com/ExpoVirtual/VirtualExpo/EmbedVideos.php" + "' , 'video','width=560,height=896,left=20,top=20')");
                #else
                Application.OpenURL("https://teckdes.com/ExpoVirtual/VirtualExpo/EmbedVideosExe.php?NumeroDeStand="+NumeroDeStand.ToString()+"&URLVideo=URLVideo"+NumeroDeVideo.ToString());
                #endif
            }));

            if(analitycs != null){ analitycs.ClickVideos("N"); }
        }
    }
}
