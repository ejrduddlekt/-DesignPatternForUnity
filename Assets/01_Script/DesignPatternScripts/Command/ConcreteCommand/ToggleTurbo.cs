using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//command를 상속받아 각 명령들을 클래스로 캡슐화 한다.
//리시버 객체를 저장하고, Execute()에서 Receiver의 메서드를 Execute한다.
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
