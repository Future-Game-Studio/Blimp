using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainIsle : UIController
{
    public override void UpdateAll()
    {
        throw new System.NotImplementedException();
    }

    public void ExitFromMainIsle()
    {
        GameManager._instance.ChangeGameMode(GameMode.InSpace);
    }

}
