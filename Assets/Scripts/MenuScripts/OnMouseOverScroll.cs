using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouseOverScroll : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //Desactivo el zoom en camara (utiliza la rueda del raton) para utilizar el scroll de UI.
    public void OnPointerEnter(PointerEventData eventData)
    {
        UserInfo.onScroll = true;
    }
    //Reactivo el zoom en camara (utiliza la rueda del raton).

    public void OnPointerExit(PointerEventData eventData)
    {
        UserInfo.onScroll = false;
    }
}
