using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main Instance;

    public Web Web;
    public UserInfo UserInfo;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Web = GetComponent<Web>();
        UserInfo = GetComponent<UserInfo>();

        GameObject AMMRoomController = GameObject.Find("AMMRoomController");
        if(AMMRoomController != null)
            Destroy(AMMRoomController);
    }
}
