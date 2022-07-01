using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class NoticiasScript : MonoBehaviour
{
    public GameObject NoticiasText;

    void Start()
    {
        StartCoroutine(TextAnimation());
    }

    IEnumerator TextAnimation()
    {
        WWWForm form = new WWWForm();
        using (UnityWebRequest www = UnityWebRequest.Post("https://expovirtual.com.ar/VirtualExpo/BotFeria.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                NoticiasText.GetComponent<TextMeshProUGUI>().text = www.downloadHandler.text.Split('|')[0];
                NoticiasText.transform.parent.gameObject.SetActive(true);
                yield return new WaitForSeconds(5);
                NoticiasText.transform.parent.gameObject.SetActive(false);
                yield return new WaitForSeconds(35);
                StartCoroutine(TextAnimation());
            }
        }
    }
}
