using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChloeRoomPart : HiddenPuzzlePart
{


    public override void Interact()
    {
        Debug.Log("habitacion Cloe");
        interactCorrectPart?.Invoke();

    }

    public override void Start()
    {
    }
}
