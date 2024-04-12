using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Playables;

public class ObserverBikeController : Subject
{
    public bool IsTurboOn { get; private set; }
    public float CurrentHealth { get { return health; } }

    private bool _isEngineOn;
    private ObserverHUDController _hudController;
    private ObserverCameraController _cameraController;

    [SerializeField]
    private float health = 100f;

    private void Awake()
    {
        _hudController = gameObject.AddComponent<ObserverHUDController>();
        _cameraController = (ObserverCameraController)FindObjectOfType(typeof(ObserverCameraController));
    }

    private void Start()
    {
        StartEngine();
    }

    //관찰자 연결
    private void OnEnable()
    {
        if (_hudController)
            Attach(_hudController);

        if (_cameraController)
            Attach(_cameraController);
    }

    //관찰자 해제
    private void OnDisable()
    {
        if (_hudController)
            Detach(_hudController);

        if (_cameraController)
            Detach(_cameraController);
    }

    //시작 처리한다. (NotifyObservers();)
    private void StartEngine()
    {
        _isEngineOn = true;
        NotifyObservers();
    }

    //터보토글 처리한다. (NotifyObservers();)
    public void ToggleTurbo()
    {
        if (_isEngineOn)
            IsTurboOn = !IsTurboOn;

        NotifyObservers();
    }

    //데미지를 처리한다. (NotifyObservers();)
    public void TakeDamage(float amount)
    {
        health -= amount;
        IsTurboOn = false;

        NotifyObservers();

        if (health < 0)
            Destroy(gameObject);
    }
}
