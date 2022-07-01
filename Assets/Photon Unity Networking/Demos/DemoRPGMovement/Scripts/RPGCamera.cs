using UnityEngine;
using System.Collections;

public class RPGCamera : MonoBehaviour
{
   
    public Transform Target;

    public float MaximumDistance;
    public float MinimumDistance;

    public float ScrollModifier;
    public float TurnModifier;

    Transform m_CameraTransform;

    Vector3 m_LookAtPoint;
    Vector3 m_LocalForwardVector;
    public float m_Distance;

    void Start()
    {
        m_CameraTransform = transform.GetChild(0);
        m_LocalForwardVector = m_CameraTransform.forward;

        m_Distance = -m_CameraTransform.localPosition.z / m_CameraTransform.forward.z;
        m_Distance = Mathf.Clamp( m_Distance, MinimumDistance, MaximumDistance );
        m_LookAtPoint = m_CameraTransform.localPosition + m_LocalForwardVector * m_Distance;
    }

    void LateUpdate()
    {
        UpdateDistance();
        UpdateZoom();
        UpdatePosition();
        UpdateRotation();
    }

    void UpdateDistance()
    {
        if (UserInfo.onScroll == false)
            m_Distance = Mathf.Clamp( m_Distance - Input.GetAxis( "Mouse ScrollWheel" ) * ScrollModifier, MinimumDistance, MaximumDistance );
    }

    void UpdateZoom()
    {
        m_CameraTransform.localPosition = m_LookAtPoint - m_LocalForwardVector * m_Distance;

        if (Input.GetButtonUp("Fire3") && UserInfo.onScroll == false)
        {
            if (m_Distance < 3.5)
            {
                m_Distance = MaximumDistance;
            }
            else
            {
                m_Distance = MinimumDistance;
            }

        }

    }

    void UpdatePosition()
    {
        if( Target == null )
        {
            return;
        }

        transform.position = Target.transform.position;
    }

    void UpdateRotation()
    {

        if ( Input.GetButton("Fire2") )
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * TurnModifier, 0);
        }

        /*if ((Input.GetMouseButton(1) || Input.GetButton("Fire2")) && Target != null) { 

            Target.rotation = Quaternion.Euler( 0, transform.rotation.eulerAngles.y, 0 );
        }*/
    }
   
}
