using UnityEngine;


public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string Interactable = "Interactable";
    [SerializeField] private string Player = "Player";
    private int typeOfPlayerInteractable;
    private Transform _selection;

    private void Start()
    {
        if (UserInfo.TipoDeUsuario == "Expositor")
            typeOfPlayerInteractable = 9;
        else if (UserInfo.TipoDeUsuario == "Visitante")
            typeOfPlayerInteractable = 10;
    }

    private void Update()
    {
        if (_selection != null)
        {
            var cubeOutline = _selection.transform.gameObject.GetComponent<Outline>();
            if(cubeOutline != null)
                cubeOutline.enabled = false;
            _selection = null;
        }
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 15f))
        {
            var selection = hit.transform;
            if (selection.CompareTag(Interactable) || (selection.CompareTag(Player) && hit.transform.gameObject.layer == typeOfPlayerInteractable))
            {
                var cubeOutline = hit.transform.gameObject.GetComponent<Outline>();
                if(cubeOutline != null)
                    cubeOutline.enabled = true;
            }
            _selection = selection;
        }
    }
}