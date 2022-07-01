using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomCharacter : MonoBehaviour
{
    //OrderMaterials ordermaterials;
    //MenuController menuController;
    public Renderer Mycolor;
    public Renderer[] pjRenderers;
    public GameObject PanelColores;
    public Color32 currentColorRemera;
    public Color32 currentColorPantalon;
    public Color32 currentColorPiel;
    public int materialpiel, materialremera, materialpantalon;

    private void Awake()
    {
        GameObject anothercustomCharacter = GameObject.Find("CustomCharacter");
        if (anothercustomCharacter != null && anothercustomCharacter != this.gameObject)
            Destroy(anothercustomCharacter);
        DontDestroyOnLoad(this.transform.gameObject);

        if (Mycolor == null && PanelColores != null)
        {
            for (int i = 0; i < PanelColores.transform.childCount; i++)
            {
                var child = PanelColores.transform.GetChild(i).gameObject;
                child.GetComponent<Button>().interactable = false;
            }
        }
    }
    public void OnClickCharacterPick(int wichCharacter)
    {
        switch (wichCharacter)
        {

            case 0:
                materialpiel = 0;
                materialremera = 1;
                materialpantalon = 3;
                break;
            case 1:
                materialpiel = 2;
                materialremera = 0;
                materialpantalon = 3;
                break;
            case 2:
                materialpiel = 0;
                materialremera = 1;
                materialpantalon = 3;
                break;
            case 3:
                materialpiel = 4;
                materialremera = 1;
                materialpantalon = 3;
                break;


        }
        if (PlayerInfo.PI != null)
        {
            PlayerInfo.PI.mySelectedCharacter = wichCharacter;
            PlayerPrefs.SetInt("MyCharacter", wichCharacter);
            
            switch (wichCharacter)
            {
                case 0:
                    Mycolor = pjRenderers[0];

                    break;
                case 1:
                    Mycolor = pjRenderers[1];

                    break;
                case 2:
                    Mycolor = pjRenderers[2];
                    break;
                case 3:
                    Mycolor = pjRenderers[3];
                    break;
            }
        }
        for (int i = 0; i < PanelColores.transform.childCount; i++)
        {
            var child = PanelColores.transform.GetChild(i).gameObject;
            child.GetComponent<Button>().interactable = true;
        }

    }

    // Remeras!!!!!
    #region Cambio de color Remeras
    public void ColorBlancoRemera()
    {
            Mycolor.materials[materialremera].color = new Color32(255, 255, 255, 255);
            currentColorRemera = Mycolor.materials[materialremera].color;
    }
    public void ColorNegroRemera()
    {
        Mycolor.materials[materialremera].color = new Color32(24, 24, 24, 255);
        currentColorRemera = Mycolor.materials[materialremera].color;
    }
    public void ColorGrisRemera()
    {
        Mycolor.materials[materialremera].color = new Color32(94, 94, 94, 255);
        currentColorRemera = Mycolor.materials[materialremera].color;
    }
    public void ColorNaranjaRemera()
    {
        Mycolor.materials[materialremera].color = new Color32(239, 71, 111, 255);
        currentColorRemera = Mycolor.materials[materialremera].color;
    }
    public void ColorRojoRemera()
    {
        Mycolor.materials[materialremera].color = new Color32(137, 0, 0, 255);
        currentColorRemera = Mycolor.materials[materialremera].color;
    }
    public void ColorAzulRemera()
    {
        Mycolor.materials[materialremera].color = new Color32(17, 138, 178, 255);
        currentColorRemera = Mycolor.materials[materialremera].color;
    }
    public void ColorAmarilloRemera()
    {
        Mycolor.materials[materialremera].color = new Color32(255, 209, 102, 255);
        currentColorRemera = Mycolor.materials[materialremera].color;
    }
    public void ColorVioletaRemera()
    {
        Mycolor.materials[materialremera].color = new Color32(7, 59, 76, 255);
        currentColorRemera = Mycolor.materials[materialremera].color;
    }
    public void ColorRosaRemera()
    {
        Mycolor.materials[materialremera].color = new Color32(6, 214, 160, 255);
        currentColorRemera = Mycolor.materials[materialremera].color;
    }
    #endregion

    // Pantalones!!!!
    #region Cambio color de Pantalones
    public void ColorBlancoPantalon()
    {
        Mycolor.materials[materialpantalon].color = new Color32(255, 255, 255, 255);
        currentColorPantalon = Mycolor.materials[materialpantalon].color;
    }
    public void ColorNegroPantalon()
    {
        Mycolor.materials[materialpantalon].color = new Color32(24, 24, 24, 255);
        currentColorPantalon = Mycolor.materials[materialpantalon].color;
    }
    public void ColorGrisPantalon()
    {
        Mycolor.materials[materialpantalon].color = new Color32(94, 94, 94, 255);
        currentColorPantalon = Mycolor.materials[materialpantalon].color;
    }
    public void ColorNaranjaPantalon()
    {
        Mycolor.materials[materialpantalon].color = new Color32(0, 126, 167, 255);
        currentColorPantalon = Mycolor.materials[materialpantalon].color;
    }
    public void ColorRojoPantalon()
    {
        Mycolor.materials[materialpantalon].color = new Color32(0, 52, 89, 255);
        currentColorPantalon = Mycolor.materials[materialpantalon].color;
    }
    public void ColorAmarilloPantalon()
    {
        Mycolor.materials[materialpantalon].color = new Color32(0, 168, 232, 255);
        currentColorPantalon = Mycolor.materials[materialpantalon].color;
    }
    public void ColorAzulPantalon()
    {
        Mycolor.materials[materialpantalon].color = new Color32(14, 83, 159, 255);
        currentColorPantalon = Mycolor.materials[materialpantalon].color;
    }
    public void ColorVioletaPantalon()
    {
        Mycolor.materials[materialpantalon].color = new Color32(90, 0, 204, 255);
        currentColorPantalon = Mycolor.materials[materialpantalon].color;
    }
    public void ColorRosaPantalon()
    {
        Mycolor.materials[materialpantalon].color = new Color32(159, 14, 153, 255);
        currentColorPantalon = Mycolor.materials[materialpantalon].color;
    }
    #endregion

    //Piel
    #region Cambio de color piel
    public void ColorRosadoPiel()
    {
        Mycolor.materials[materialpiel].color = new Color32(255, 212, 214, 255);
        currentColorPiel = Mycolor.materials[materialpiel].color;
    }
    public void ColorMarronPiel()
    {
        Mycolor.materials[materialpiel].color = new Color32(154, 133, 82, 255);
        currentColorPiel = Mycolor.materials[materialpiel].color;
    }
    public void ColorNaturalPiel()
    {
        Mycolor.materials[materialpiel].color = new Color32(222, 165, 134, 255);
        currentColorPiel = Mycolor.materials[materialpiel].color;
    }
    public void ColorBlancaPiel()
    {
        Mycolor.materials[materialpiel].color = new Color32(255, 255, 255, 255);
        currentColorPiel = Mycolor.materials[materialpiel].color;
    }
    #endregion
}
