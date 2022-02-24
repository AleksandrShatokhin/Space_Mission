using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishZoneForFriendly : MonoBehaviour
{
    [SerializeField] private byte counterFriendly;
    private byte lastFriendly = 2;

    private void Start()
    {
        counterFriendly = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Friendly")
        {
            if (counterFriendly != lastFriendly)
            {
                Destroy(other.gameObject);
                counterFriendly += 1;
            }
            else
            {
                Destroy(other.gameObject);
                GameController.GetInstance().WinPlayer();
            }
        }
    }
}
