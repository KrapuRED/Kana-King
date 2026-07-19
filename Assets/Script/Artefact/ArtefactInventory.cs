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
    [SerializeField] private GameObject newArtefactUIPrefab;

    [Header("Current Artefact")]
    [SerializeField] private Transform currArtefactSpawn;
    [SerializeField] private GameObject currentArtefactUIPrefab;

    [Header("Description Artefact")]
    [SerializeField] private TMP_Text artefactName;
    [SerializeField] private TMP_Text artefactDescription;

    public void SetUpNewArtefact(ArtefactSO artefact)
    {
        foreach (Transform child in newArtefactSpawn)
        {
            Destroy(child.gameObject);
        }

        if (artefact != null)
        {
            GameObject y = Instantiate(newArtefactUIPrefab, newArtefactSpawn);
            y.GetComponent<ArtefactNewUI>().ArtefactNewSetUp(artefact);
        }
    }
    public void SetUpArtefactInventory()
    {
        foreach (Transform child in currArtefactSpawn)
        {
            Destroy(child.gameObject);
        }
        foreach (ArtefactData data in ArtefactDatabase.instance.AllArtefact)
        {
            if(data.artefactSO != null && data.isActivated)
            {
                GameObject y = Instantiate(currentArtefactUIPrefab, currArtefactSpawn);
                y.GetComponent<ArtefactCurrentUI>().ArtefactCurrentSetUp(data.artefactSO);
            }
        }
    }

    public void OpenArtefactDescription(ArtefactSO artefactSO)
    {
        if (artefactSO == null) return;
        artefactName.text = artefactSO.artefactName;
        artefactDescription.text = artefactSO.artefactDescription;
    }

    public void TakeNewArtefact()
    {
        ArtefactManager.instance.AddArtefact();
    }

    public void DidntTakeNewArtefact()
    {
        ArtefactManager.instance.StashArtefact();
    }

}
