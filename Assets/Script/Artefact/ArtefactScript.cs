using UnityEngine;

public class ArtefactScript : MonoBehaviour, IArtefact
{
    [SerializeField] private ArtefactSO artefactSO;
    [SerializeField] private bool isActivated;

    public void ArtefactActivated()
    {
        ArtefactActive();
    }

    public void ArtefactDeactivated()
    {
        ArtefactDisable();
    }

    protected virtual void ArtefactActive()
    {
        Debug.Log("Arte 1 aktif");
    }
    protected virtual void ArtefactDisable()
    {
        Debug.Log("Arte 1 ga aktif");
    }
}
