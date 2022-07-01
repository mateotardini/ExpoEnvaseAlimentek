using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo PI;
    public TMP_Text SaludoTMP;

    public int mySelectedCharacter;

    public GameObject[] allCharacters;
    public GameObject Expositor, Visitante;

    private void OnEnable()
    {
        if (PlayerInfo.PI == null)
        {
            PlayerInfo.PI = this;
        }
        else
        {
            if (PlayerInfo.PI != this)
            {
                Destroy(PlayerInfo.PI.gameObject);
                PlayerInfo.PI = this;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("MyCharacter"))
        {
            mySelectedCharacter = PlayerPrefs.GetInt("MyCharacter");
        }
        else
        {
            if (UserInfo.Level == "1")
                mySelectedCharacter = 3;
            else if (UserInfo.Level == "0")
                mySelectedCharacter = 0;
            PlayerPrefs.SetInt("MyCharacter", mySelectedCharacter);
        }

        SaludoTMP.text = "¡Bienvenido " + UserInfo.Name + "!";

        if (UserInfo.Level == "1")
        {
            Visitante.SetActive(false);
        }
        else if (UserInfo.Level == "0")
        {
            Expositor.SetActive(false);
        }
    }
}

