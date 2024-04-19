using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeEngine : MonoBehaviour, IBikeElement
{
    public float turboBoost = 25f; //mph
    public float maxTurboBoost = 200f;

    private bool _isTurboOn;
    private float _defaultSpeed = 300f; //mph

    public float CurrentSpeed
    {
        get
        {
            if(_isTurboOn)
                return _defaultSpeed + turboBoost;

            return _defaultSpeed;
        }
    }

    public void ToggleTurbo()
    {
        _isTurboOn = !_isTurboOn;
    }


    public void Accept()
}
