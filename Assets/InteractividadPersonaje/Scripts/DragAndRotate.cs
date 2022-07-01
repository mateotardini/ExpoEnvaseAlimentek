using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndRotate : MonoBehaviour
{
    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;
    public float angle = 5;
    public bool value;


    private void Start()
    {
        value = true;
    }

     private void OnMouseDrag()
    {
        
            float x = Input.GetAxis("Mouse X");
            // float y = Input.GetAxis("Mouse Y");
            transform.RotateAround(transform.position, new Vector3(0, 1, 0) * Time.deltaTime * -x, angle);
            // transform.RotateAround(transform.position, new Vector3(1,0,0) * Time.deltaTime *y, angle);

        

    }

    private void LateUpdate()
    {
        
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit))
            {
                if ((hit.transform != null) && (hit.transform == this.transform) && Input.GetMouseButtonDown(0))
                {
                    //OnMouseDrag();
                    value = false;
                }
                if (Input.GetMouseButtonUp(0)) {

                    value = true;
                }


            }
            else
            {
            value = true;
            }


        
        
        
        if (value == false)
        {
            OnMouseDrag();

        }
        else
        {
            transform.RotateAround(transform.position, new Vector3(0, 1, 0) * Time.deltaTime, 0.15f);
        }
        
        
        



            /*
            if (!Input.GetMouseButton(0))
            {
                angle = 0.35f;
                transform.RotateAround(transform.position, new Vector3(0, 1, 0) * Time.deltaTime, angle);
            }
            else {
                angle = 5;
            }



        if (value == true)
        {
            angle = 0.35f;
            transform.RotateAround(transform.position, new Vector3(0, 1, 0) * Time.deltaTime, angle);
        }
        else
        {
            value=false;

        }*/
        }



}

