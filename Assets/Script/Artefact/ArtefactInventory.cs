using System.Collections.Generic;
using UnityEngine;

public class ArtefactInventory : MonoBehaviour
{
    [SerializeField] private List<ArtefactSO> currentArtefactSO = new();


    public void ActivateAllArtefact()
    {
        foreach(ArtefactSO artefact in currentArtefactSO)
        {
            artefact.artefactScript.ArtefactActivated();
        }
    }

    public bool CheckArtefactInventory()
    {
        if(currentArtefactSO.Count > 3)
        {
            return false;
        }
        else
            return true;
    }

}
