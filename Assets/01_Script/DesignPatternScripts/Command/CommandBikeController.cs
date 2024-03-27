using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Reciever(수신자)
public class CommandBikeController : MonoBehaviour
{
    private bool _isTurboOn;
    private float _distance = 1.0f;

    #region 커맨드 클래스가 호출하는 메서드 (캡슐화)
    public void ToggleTurbo()
    {
        _isTurboOn = !_isTurboOn;
        Debug.Log("Turbo Active: " + _isTurboOn.ToString());
    }

    public void Turn(Direction direction)
    {
        if (direction == Direction.Left)
            transform.Translate(Vector3.left * _distance);
        if (direction == Direction.Right)
            transform.Translate(Vector3.right * _distance);
    }
    #endregion

    //디버깅 및 테스트 목적
    public void ResetPosition()
    {
        transform.position = new Vector3(0f, 0f, 0f);
    }
}
