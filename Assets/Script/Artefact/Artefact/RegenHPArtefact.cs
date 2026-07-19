using System.Collections;
using UnityEngine;

public class RegenHPArtefact : ArtefactScript
{


    
    protected override void ArtefactActive()
    {
        Debug.Log("Arte regen aktif");
        //Player.instance.HealArtefactActivated();
    }
    protected override void ArtefactDisable()
    {
        Player.instance.HealArtefactDisable();
    }


}
