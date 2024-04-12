using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientObserver : MonoBehaviour
{
    private ObserverBikeController _bikeController;

    private void Start()
    {
        _bikeController = (ObserverBikeController)FindObjectOfType(typeof(ObserverBikeController));
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Damage Bike"))
            if (_bikeController)
                _bikeController.TakeDamage(15f);

        if(GUILayout.Button("Toggle Turbo"))
            if(_bikeController)
                _bikeController.ToggleTurbo();
    }
}
