using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorR1R2 : MonoBehaviour
{
    private Animator anim_Door;

    private void Start()
    {
        anim_Door = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.layer == 7) // layer 7 - enemy
        {
            anim_Door.SetBool("isOpen", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.layer == 7) // layer 7 - enemy
        {
            anim_Door.SetBool("isOpen", false);
        }
    }
}
