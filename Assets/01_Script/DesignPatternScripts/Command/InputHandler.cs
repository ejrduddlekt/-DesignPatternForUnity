using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private Invoker _invoker;
    private bool _isReplaying;
    private bool _isRecording;
    private CommandBikeController _commandBikeController;
    private Command _buttonA, _buttonD, _buttonW;

    private void Start()
    {
        _invoker = gameObject.AddComponent<Invoker>();
        _commandBikeController = FindObjectOfType<CommandBikeController>();

        _buttonA = new TurnLeft(_commandBikeController);
        _buttonD = new TurnRight(_commandBikeController);
        _buttonW = new ToggleTurbo(_commandBikeController);
    }

    private void Update()
    {
        if (!_isReplaying && _isRecording)
        {
            if (Input.GetKeyUp(KeyCode.A))
                _invoker.ExecuteCommand(_buttonA);
            if (Input.GetKeyUp(KeyCode.D))
                _invoker.ExecuteCommand(_buttonD);
            if (Input.GetKeyUp(KeyCode.W))
                _invoker.ExecuteCommand(_buttonW);
        }
    }

    //테스트 용
    private void OnGUI()
    {
        if (GUILayout.Button("Start Recording"))
        {
            _commandBikeController.ResetPosition();
            _isReplaying = false;
            _isRecording = true;
            _invoker.Record();
        }

        if (GUILayout.Button("Stop Recording"))
        {
            _commandBikeController.ResetPosition();
            _isRecording = false;
        }

        if (!_isRecording)
        {
            if (GUILayout.Button("Start Replay"))
            {
                _commandBikeController.ResetPosition();
                _isRecording = false;
                _isReplaying = true;
                _invoker.Replay();
            }
        }
    }
}
