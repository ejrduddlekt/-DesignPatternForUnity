using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Reciever(������)
public class CommandBikeController : MonoBehaviour
{
    private bool _isTurboOn;
    private float _distance = 1.0f;

    #region Ŀ�ǵ� Ŭ������ ȣ���ϴ� �޼��� (ĸ��ȭ)
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

    //����� �� �׽�Ʈ ����
    public void ResetPosition()
    {
        transform.position = new Vector3(0f, 0f, 0f);
    }
}
