using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentChats : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject newMessage;
    public bool IsANewMessage = false;

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount > 0)
        {
            IsANewMessage = false;
            for (int i = 0; i < transform.childCount; ++i)
            {
                if (transform.GetChild(i).GetChild(0).GetChild(2).gameObject.activeSelf)
                {
                    IsANewMessage = true;
                    break;
                }
            }
            if (IsANewMessage)
                newMessage.SetActive(true);
            else
                newMessage.SetActive(false);
        }
        else
            newMessage.SetActive(false);

    }

    public void DeactivateAllChats() {
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.GetComponent<PhotonChatManager>().CloseChat();
        }
    }
}
