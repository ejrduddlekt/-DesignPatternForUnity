using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLeft : Command
{
    private CommandBikeController _controller;

    public TurnLeft(CommandBikeController controller)
    {
        _controller = controller;
    }

    public override void Execute()
    {
        _controller.Turn(Direction.Left);
    }
}
