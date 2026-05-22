using UnityEngine;
using UnityEngine.UI;

public class ArtefactCurrentUI : MonoBehaviour
{
    [SerializeField] private ArtefactSO artefactSO;
    [SerializeField] private Image artefactImage;



    public void ArtefactCurrentSetUp(ArtefactSO artefact)
    {
        artefactSO = artefact;
        artefactImage.sprite = artefactSO.artefactSprite;
    }
    public void DeleteArtefact()
    {
        ArtefactManager.instance.DeleteArtefact(artefactSO);
    }
    public void OpenDescriptionArtefact()
    {
        ArtefactInventory.instance.OpenArtefactDescription(artefactSO);
    }
}
