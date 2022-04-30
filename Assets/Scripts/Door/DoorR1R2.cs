using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorR1R2 : MonoBehaviour
{
    private Animator anim_Door;

    private AudioSource audioSourceDoor;
    [SerializeField] private AudioClip audioOpenDoor, audioCloseDoor;

    private void Start()
    {
        anim_Door = GetComponent<Animator>();
        audioSourceDoor = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.layer == (int)Layers.Enemy || other.gameObject.tag == "Friendly")
        {
            anim_Door.SetBool("isOpen", true);
            audioSourceDoor.PlayOneShot(audioOpenDoor, 0.5f);

            if (other.gameObject.tag == "Friendly" && GetComponentInParent<Transform>().name == "DoorWithTrigger")
            {
                other.gameObject.GetComponent<FriendlyManager>().SwitchTarget(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.layer == (int)Layers.Enemy || other.gameObject.tag == "Friendly")
        {
            anim_Door.SetBool("isOpen", false);
            audioSourceDoor.PlayOneShot(audioCloseDoor, 0.5f);
        }
    }
}
