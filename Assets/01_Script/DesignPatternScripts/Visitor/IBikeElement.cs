using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBikeElement
{
    void Accept(IVisitor visitor);
}
