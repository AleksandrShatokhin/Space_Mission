using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAmmo : MonoBehaviour
{
    private int ammoInBox = 5;
    [SerializeField] private AudioClip audioPickup;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            int ammoWithPlayer = collider.gameObject.GetComponent<PlayerController>().GetMaxBullet();
            int acceptableValue = 50;
            int difference;

            if (ammoWithPlayer == acceptableValue)
            {
                return;
            }
            
            if (ammoWithPlayer < acceptableValue)
            {
                GameController.GetInstance().PlayAudio(audioPickup);

                difference = acceptableValue - ammoWithPlayer;

                if (difference < ammoInBox)
                {
                    collider.gameObject.GetComponent<PlayerController>().GetMaxBullet(difference);
                    Destroy(this.gameObject);
                }

                if (difference >= ammoInBox)
                {
                    collider.gameObject.GetComponent<PlayerController>().GetMaxBullet(ammoInBox);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
