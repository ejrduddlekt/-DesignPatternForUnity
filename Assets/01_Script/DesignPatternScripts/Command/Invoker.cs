using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//ȣ����
public class Invoker : MonoBehaviour
{
    private bool _isRecording;
    private bool _isReplaying;
    private float _replayTime;
    private float _recordingTime;

    //SortedList�� Ű�� ������������ ������ �ϴ� List�̴�.
    private SortedList<float, Command> _recordedCommnads = new SortedList<float, Command>();


    //Invoker�� ���ο� ����� ���� �� �� ����  _recordedCommnads�� ����� �Ѵ�.
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

    #region ���÷��� ���� ����
    public void Replay()
    {
        _replayTime = 0f;
        _isReplaying = true;

        if (_recordedCommnads.Count <= 0)
            Debug.Log("No commands to replay!");

        _recordedCommnads.Reverse();
    }

    //�Ϲ������� Update()�� ����ϳ�
    //������ �ð��� �������� ����Ǵ� FixedUpdate�� �ð��� ������������ ������ �ӵ��ʹ� ������ �۾��� �����Ѵ�.
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
