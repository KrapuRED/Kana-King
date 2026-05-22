using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArtefactInventory : MonoBehaviour
{

    public static ArtefactInventory instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [Header("New Artefact")]
    [SerializeField] private Transform newArtefactSpawn;

    [Header("Current Artefact")]
    [SerializeField] private Transform currArtefactSpawn;
    [SerializeField] private GameObject currentArtefactUIPrefab;

    [Header("Description Artefact")]
    [SerializeField] private TMP_Text artefactName;
    [SerializeField] private TMP_Text artefactDescription;


    public void SetUpArtefactInventory()
    {

    }

    public void OpenArtefactDescription(ArtefactSO artefactSO)
    {
        artefactName.text = artefactSO.name;
        artefactDescription.text = artefactSO.artefactDescription;
    }

}
