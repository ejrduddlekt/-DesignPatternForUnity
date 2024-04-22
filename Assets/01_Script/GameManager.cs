using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    private DateTime _sessionStartTime;
    private DateTime _sessionEndTime;

    private void Start()
    {
        /*
            * ToDo
            *  - �÷��̾� ���̺� �ε�
            *  - ���̺갡 ������ �÷��̾ ��� ������ �����̷����Ѵ�.
            *  - �鿣�带 ȣ���ϰ� ���� ç������ ������ ��´�.
        */
        _sessionStartTime = DateTime.Now;
        Debug.Log("Game Session Start @ : " + DateTime.Now);
    }

    private void OnApplicationQuit()
    {
        _sessionEndTime = DateTime.Now;

        TimeSpan timeDifference = _sessionEndTime.Subtract(_sessionEndTime);

        Debug.Log("Game session ended @: " + DateTime.Now);
        Debug.Log("Game session lasted: " + timeDifference);
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Next Scene"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
 