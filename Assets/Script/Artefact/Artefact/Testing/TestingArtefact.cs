using UnityEngine;

public class TestingArtefact : MonoBehaviour
{
    public ArtefactSO x;

    public void click()
    {
        ArtefactManager.instance.OpenArtefactManager(x);
    }
}
