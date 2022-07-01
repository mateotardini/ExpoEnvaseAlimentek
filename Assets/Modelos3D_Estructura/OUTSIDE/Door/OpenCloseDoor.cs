using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseDoor : MonoBehaviour
{
    public Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "WalkerBot" ) {

            _animator.SetBool("Open", true);
            
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player" || other.tag == "WalkerBot")
        {

            _animator.SetBool("Open", false);

        }
    }
}
