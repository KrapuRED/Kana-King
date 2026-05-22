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

    [SerializeField] private List<ArtefactScript> currentArtefacts = new();


    private void Start()
    {
        ActivateAllArtefact();
    }

    public void AddArtefact(ArtefactScript artefact)
    {
        if (CheckArtefactInventory())
        {
            currentArtefacts.Add(artefact);
            artefact.ArtefactActivated();
        }
    }

    public void DeleteArtefact(ArtefactScript artefact)
    {
        if (currentArtefacts.Contains(artefact))
        {
            artefact.ArtefactDeactivated();
            currentArtefacts.Remove(artefact);
        }
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
        foreach (var artefact in currentArtefacts)
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



}