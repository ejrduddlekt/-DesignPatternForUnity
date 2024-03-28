using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//command�� ��ӹ޾� �� ��ɵ��� Ŭ������ ĸ��ȭ �Ѵ�.
//���ù� ��ü�� �����ϰ�, Execute()���� Receiver�� �޼��带 Execute�Ѵ�.
public class ToggleTurbo : Command
{
    private CommandBikeController _controller;

    public ToggleTurbo(CommandBikeController controller)
    {
        _controller = controller;
    }

    public override void Execute()
    {
        _controller.ToggleTurbo();
    }
}
