using System.Collections.Generic;
using UnityEngine;

public class ArtefactManager : MonoBehaviour
{
    public static ArtefactManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [SerializeField] private ArtefactScript newArtefacts;
    [SerializeField] private List<ArtefactScript> currentArtefacts = new();

    [SerializeField] private GameObject artefactInventoryPanel;


    private void Start()
    {
        DeactivateAllArtefact();
        ActivateAllArtefact();
    }


    public void OpenArtefactManager(ArtefactScript artefact)
    {
        artefactInventoryPanel.SetActive(true);
        newArtefacts = artefact;
        ArtefactInventory.instance.SetUpArtefactInventory();
        ArtefactInventory.instance.SetUpNewArtefact(newArtefacts);
        PauseSystem.instance.AddPauseRequest();
    }

    public void AddArtefact()
    {
        if (CheckArtefactInventory())
        {
            currentArtefacts.Add(newArtefacts);
        }
        artefactInventoryPanel.SetActive(false);
        DeactivateAllArtefact();
        ActivateAllArtefact();
        PauseSystem.instance.RemovePauseRequest();
    }
    public void StashArtefact()
    {
        newArtefacts = null;
        artefactInventoryPanel.SetActive(false);
        DeactivateAllArtefact();
        ActivateAllArtefact();
        PauseSystem.instance.RemovePauseRequest();
    }


    public void DeleteArtefact(ArtefactSO artefact)
    {
        foreach(ArtefactScript x in currentArtefacts)
        {
            if (x.artefactSO == artefact)
            {
                x.ArtefactDeactivated();
                currentArtefacts.Remove(x);
                break;
            }
        }
        ArtefactInventory.instance.SetUpArtefactInventory();
    }

    public void ActivateAllArtefact()
    {
        foreach (var artefact in currentArtefacts)
        {
            artefact.ArtefactActivated();
        }
    }

    public void DeactivateAllArtefact()
    {
        foreach (var artefact in ArtefactDatabase.instance.ReturnAllArtefact())
        {
            artefact.ArtefactDeactivated();
        }
    }

    public bool CheckArtefactInventory()
    {
        return currentArtefacts.Count < 3;
    }

    public List<ArtefactScript> ReturnCurrentArtefact()
    {
        return currentArtefacts;
    }

    public ArtefactScript ReturnNewArtefact()
    {
        return newArtefacts;
    }

}