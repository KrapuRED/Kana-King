using UnityEngine;
using UnityEngine.UI;

public class ArtefactNewUI : MonoBehaviour
{
    [SerializeField] private ArtefactSO artefactSO;
    [SerializeField] private Image artefactImage;



    public void ArtefactNewSetUp(ArtefactSO artefact)
    {
        artefactSO = artefact;
        artefactImage.sprite = artefactSO.artefactSprite;
    }
    public void OpenDescriptionArtefact()
    {
        ArtefactInventory.instance.OpenArtefactDescription(artefactSO);
    }
}
