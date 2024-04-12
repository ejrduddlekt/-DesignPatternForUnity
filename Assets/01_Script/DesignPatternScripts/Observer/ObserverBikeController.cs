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

    //������ ����
    private void OnEnable()
    {
        if (_hudController)
            Attach(_hudController);

        if (_cameraController)
            Attach(_cameraController);
    }

    //������ ����
    private void OnDisable()
    {
        if (_hudController)
            Detach(_hudController);

        if (_cameraController)
            Detach(_cameraController);
    }

    //���� ó���Ѵ�. (NotifyObservers();)
    private void StartEngine()
    {
        _isEngineOn = true;
        NotifyObservers();
    }

    //�ͺ���� ó���Ѵ�. (NotifyObservers();)
    public void ToggleTurbo()
    {
        if (_isEngineOn)
            IsTurboOn = !IsTurboOn;

        NotifyObservers();
    }

    //�������� ó���Ѵ�. (NotifyObservers();)
    public void TakeDamage(float amount)
    {
        health -= amount;
        IsTurboOn = false;

        NotifyObservers();

        if (health < 0)
            Destroy(gameObject);
    }
}
