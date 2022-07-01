using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovDeMapa : MonoBehaviour
{
    public int mapaNumero;
    public TMP_Text TextoPabellon;
    public GameObject PanelAllPBs;
    public GameObject[] Pabellones;
    public GameObject[] BTNsPabellones;


    // Start is called before the first frame update
    void OnEnable()
    {
        foreach (GameObject go in Pabellones)
            go.SetActive(false);
        foreach (GameObject go in BTNsPabellones)
            go.SetActive(false);
        PanelAllPBs.SetActive(true);
    }

    public void SiguienteMapa()
    {
        PanelAllPBs.SetActive(false);
        if (mapaNumero < 8)
        {
            mapaNumero++;
        }
        if (mapaNumero == 8)
        {
            mapaNumero = 1;
        }
        switch (mapaNumero)
        {
            case 1:
                TextoPabellon.text = "PABELLÓN 1";

                foreach (GameObject go in Pabellones)
                    go.SetActive(false);
                foreach (GameObject go in BTNsPabellones)
                    go.SetActive(false);

                Pabellones[0].SetActive(true);
                BTNsPabellones[0].SetActive(true);

                break;
            case 2:
                TextoPabellon.text = "PABELLÓN 2";

                foreach (GameObject go in Pabellones)
                    go.SetActive(false);
                foreach (GameObject go in BTNsPabellones)
                    go.SetActive(false);

                Pabellones[1].SetActive(true);
                BTNsPabellones[1].SetActive(true);

                break;
            case 3:
                TextoPabellon.text = "PABELLÓN 3";

                foreach (GameObject go in Pabellones)
                    go.SetActive(false);
                foreach (GameObject go in BTNsPabellones)
                    go.SetActive(false);

                Pabellones[2].SetActive(true);
                BTNsPabellones[2].SetActive(true);

                break;
            case 4:
                TextoPabellon.text = "PABELLÓN 4";

                foreach (GameObject go in Pabellones)
                    go.SetActive(false);
                foreach (GameObject go in BTNsPabellones)
                    go.SetActive(false);

                Pabellones[3].SetActive(true);
                BTNsPabellones[3].SetActive(true);

                break;
            case 5:
                TextoPabellon.text = "PABELLÓN 5a";

                foreach (GameObject go in Pabellones)
                    go.SetActive(false);
                foreach (GameObject go in BTNsPabellones)
                    go.SetActive(false);

                Pabellones[4].SetActive(true);
                BTNsPabellones[4].SetActive(true);

                break;
            case 6:
                TextoPabellon.text = "PABELLÓN 5b";

                foreach (GameObject go in Pabellones)
                    go.SetActive(false);
                foreach (GameObject go in BTNsPabellones)
                    go.SetActive(false);

                Pabellones[5].SetActive(true);
                BTNsPabellones[5].SetActive(true);

                break;
            case 7:
                TextoPabellon.text = "HALL IAE";

                foreach (GameObject go in Pabellones)
                    go.SetActive(false);
                foreach (GameObject go in BTNsPabellones)
                    go.SetActive(false);

                Pabellones[6].SetActive(true);
                BTNsPabellones[6].SetActive(true);

                break;
        }
    }
    public void AnteriorMapa()
    {
        if (mapaNumero > 0)
        {
            mapaNumero--;
        }
        if (mapaNumero == 0)
        {
            mapaNumero = 7;
        }
        switch (mapaNumero)
        {
            case 1:
                TextoPabellon.text = "PABELLÓN 1";

                foreach (GameObject go in Pabellones)
                    go.SetActive(false);
                foreach (GameObject go in BTNsPabellones)
                    go.SetActive(false);

                Pabellones[0].SetActive(true);
                BTNsPabellones[0].SetActive(true);

                break;
            case 2:
                TextoPabellon.text = "PABELLÓN 2";

                foreach (GameObject go in Pabellones)
                    go.SetActive(false);
                foreach (GameObject go in BTNsPabellones)
                    go.SetActive(false);

                Pabellones[1].SetActive(true);
                BTNsPabellones[1].SetActive(true);

                break;
            case 3:
                TextoPabellon.text = "PABELLÓN 3";

                foreach (GameObject go in Pabellones)
                    go.SetActive(false);
                foreach (GameObject go in BTNsPabellones)
                    go.SetActive(false);

                Pabellones[2].SetActive(true);
                BTNsPabellones[2].SetActive(true);

                break;
            case 4:
                TextoPabellon.text = "PABELLÓN 4";

                foreach (GameObject go in Pabellones)
                    go.SetActive(false);
                foreach (GameObject go in BTNsPabellones)
                    go.SetActive(false);

                Pabellones[3].SetActive(true);
                BTNsPabellones[3].SetActive(true);

                break;
            case 5:
                TextoPabellon.text = "PABELLÓN 5a";

                foreach (GameObject go in Pabellones)
                    go.SetActive(false);
                foreach (GameObject go in BTNsPabellones)
                    go.SetActive(false);

                Pabellones[4].SetActive(true);
                BTNsPabellones[4].SetActive(true);

                break;
            case 6:
                TextoPabellon.text = "PABELLÓN 5b";

                foreach (GameObject go in Pabellones)
                    go.SetActive(false);
                foreach (GameObject go in BTNsPabellones)
                    go.SetActive(false);

                Pabellones[5].SetActive(true);
                BTNsPabellones[5].SetActive(true);

                break;
            case 7:
                TextoPabellon.text = "HALL IAE";

                foreach (GameObject go in Pabellones)
                    go.SetActive(false);
                foreach (GameObject go in BTNsPabellones)
                    go.SetActive(false);

                Pabellones[6].SetActive(true);
                BTNsPabellones[6].SetActive(true);

                break;
        }
    }

    public void SeleccionarPabellon(int numPab) {
        switch (numPab)
        {
            case 1:
                TextoPabellon.text = "PABELLÓN 1";

                foreach (GameObject go in Pabellones)
                    go.SetActive(false);
                foreach (GameObject go in BTNsPabellones)
                    go.SetActive(false);

                Pabellones[0].SetActive(true);
                BTNsPabellones[0].SetActive(true);
                mapaNumero = 1;
                break;
            case 2:
                TextoPabellon.text = "PABELLÓN 2";

                foreach (GameObject go in Pabellones)
                    go.SetActive(false);
                foreach (GameObject go in BTNsPabellones)
                    go.SetActive(false);

                Pabellones[1].SetActive(true);
                BTNsPabellones[1].SetActive(true);
                mapaNumero = 2;
                break;
            case 3:
                TextoPabellon.text = "PABELLÓN 3";

                foreach (GameObject go in Pabellones)
                    go.SetActive(false);
                foreach (GameObject go in BTNsPabellones)
                    go.SetActive(false);

                Pabellones[2].SetActive(true);
                BTNsPabellones[2].SetActive(true);

                break;
            case 4:
                TextoPabellon.text = "PABELLÓN 4";

                foreach (GameObject go in Pabellones)
                    go.SetActive(false);
                foreach (GameObject go in BTNsPabellones)
                    go.SetActive(false);

                Pabellones[3].SetActive(true);
                BTNsPabellones[3].SetActive(true);
                mapaNumero = 4;
                break;
            case 5:
                TextoPabellon.text = "PABELLÓN 5a";

                foreach (GameObject go in Pabellones)
                    go.SetActive(false);
                foreach (GameObject go in BTNsPabellones)
                    go.SetActive(false);
                mapaNumero = 5;
                Pabellones[4].SetActive(true);
                BTNsPabellones[4].SetActive(true);

                break;
            case 6:
                TextoPabellon.text = "PABELLÓN 5b";

                foreach (GameObject go in Pabellones)
                    go.SetActive(false);
                foreach (GameObject go in BTNsPabellones)
                    go.SetActive(false);

                Pabellones[5].SetActive(true);
                BTNsPabellones[5].SetActive(true);

                break;
            case 7:
                TextoPabellon.text = "HALL IAE";

                foreach (GameObject go in Pabellones)
                    go.SetActive(false);
                foreach (GameObject go in BTNsPabellones)
                    go.SetActive(false);

                Pabellones[6].SetActive(true);
                BTNsPabellones[6].SetActive(true);
                mapaNumero = 7;
                break;
        }
        PanelAllPBs.SetActive(false);
    }
}
