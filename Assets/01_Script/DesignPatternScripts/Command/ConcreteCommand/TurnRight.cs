using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnRight : Command
{
    private CommandBikeController _controller;

    public TurnRight(CommandBikeController controller)
    {
        _controller = controller;
    }

    public override void Execute()
    {
        _controller.Turn(Direction.Right);
    }
}
