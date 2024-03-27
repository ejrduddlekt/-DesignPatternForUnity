using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//호출자
public class Invoker : MonoBehaviour
{
    private bool _isRecording;
    private bool _isReplaying;
    private float _replayTime;
    private float _recordingTime;

    //SortedList는 키의 오름차순으로 정렬을 하는 List이다.
    private SortedList<float, Command> _recordedCommnads = new SortedList<float, Command>();


    //Invoker는 새로운 명령이 실행 될 때 마다  _recordedCommnads에 기록을 한다.
    public void ExecuteCommand(Command command)
    {
        command.Execute();

        if (_isRecording)
        {
            _recordedCommnads.Add(_recordingTime, command);
        }

        Debug.Log("Recorded Time: " + _recordingTime);
        Debug.Log("Recorded Command: " + command);
    }

    public void Record()
    {
        _recordingTime = 0f;
        _isRecording = true;
    }

    #region 리플레이 동작 구현
    public void Replay()
    {
        _replayTime = 0f;
        _isReplaying = true;

        if (_recordedCommnads.Count <= 0)
            Debug.Log("No commands to replay!");

        _recordedCommnads.Reverse();
    }

    //일반적으론 Update()를 사용하나
    //고정된 시간을 간격으로 실행되는 FixedUpdate는 시간에 종속적이지만 프레임 속도와는 무관한 작업에 유용한다.
    private void FixedUpdate()
    {
        if (_isRecording)
        {
            _recordingTime += Time.fixedDeltaTime;
        }

        if (_isReplaying)
        {
            _replayTime += Time.deltaTime;

            if (_recordedCommnads.Any())
            {
                if(Mathf.Approximately(_replayTime, _recordedCommnads.Keys[0]))
                {
                    Debug.Log("Replay Time: " + _replayTime);
                    Debug.Log("Replay Command: " + _recordedCommnads.Values[0]);

                    _recordedCommnads.Values[0].Execute();
                    _recordedCommnads.RemoveAt(0);

                    if(_recordedCommnads.Count == 0)
                    {
                        _isReplaying = false;
                    }
                }
            }
            else
            {
                _isRecording = false;
            }
        }
    }

    #endregion
}
