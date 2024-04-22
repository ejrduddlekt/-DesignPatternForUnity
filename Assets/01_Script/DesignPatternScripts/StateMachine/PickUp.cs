using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public PowerUp powerUp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<VisitorBikeController>())
        {
            other.GetComponent<VisitorBikeController>().Accept(powerUp);
            Destroy(gameObject);
        }
    }
}
