using UnityEngine;

public class ArtefactScript : MonoBehaviour, IArtefact
{
    public ArtefactSO artefactSO;
    [SerializeField] private bool isActivated;

    public void ArtefactActivated()
    {
        if(isActivated)return;
        ArtefactActive();
        isActivated = true;
    }

    public void ArtefactDeactivated()
    {
        ArtefactDisable();
        isActivated = false;
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
