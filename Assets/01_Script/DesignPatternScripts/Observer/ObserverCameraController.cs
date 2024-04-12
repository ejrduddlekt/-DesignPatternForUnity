using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverCameraController : Observer
{
    private bool _isTurboOn;
    private Vector3 _initialPosition;
    private float _shakeMagnitude = 0.1f;
    private ObserverBikeController _bikeController;

    private void OnEnable()
    {
        _initialPosition = gameObject.transform.localPosition;
    }

    //터보가 실행되면 꺼질때까지 카메라 흔들림을 구현
    private void Update()
    {
        if (_isTurboOn)
            gameObject.transform.localPosition = 
                _initialPosition + (Random.insideUnitSphere * _shakeMagnitude);
        else
            gameObject.transform.localPosition = _initialPosition;
    }

    //Notify를 전달 받으면 실행할 메서드
    public override void Notify(Subject subject)
    {
        if (!_bikeController)
            _bikeController = subject.GetComponent<ObserverBikeController>();

        if (_bikeController)
        {
            _isTurboOn = _bikeController.IsTurboOn;
        }
    }
}
