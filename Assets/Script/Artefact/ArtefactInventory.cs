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

    public void SetUpNewArtefact(ArtefactScript artefact)
    {
        int x = newArtefactSpawn.childCount;
        for (int i = x - 1; i >= 0; i--)
        {
            Destroy(newArtefactSpawn.GetChild(i).gameObject);
        }
        GameObject y = Instantiate(newArtefactUIPrefab, newArtefactSpawn);
        y.GetComponent<ArtefactNewUI>().ArtefactNewSetUp(artefact.artefactSO);
    }
    public void SetUpArtefactInventory()
    {
        int x = currArtefactSpawn.childCount;
        for(int i = x-1; i >= 0; i--)
        {
            Destroy(currArtefactSpawn.GetChild(i).gameObject);
        }
        foreach(ArtefactScript arte in ArtefactManager.instance.ReturnCurrentArtefact())
        {
            GameObject y = Instantiate(currentArtefactUIPrefab, currArtefactSpawn);
            y.GetComponent<ArtefactCurrentUI>().ArtefactCurrentSetUp(arte.artefactSO);
        }
    }

    public void OpenArtefactDescription(ArtefactSO artefactSO)
    {
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
