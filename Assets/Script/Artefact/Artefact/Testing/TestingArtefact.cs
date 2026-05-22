using UnityEngine;

public class TestingArtefact : MonoBehaviour
{
    public ArtefactScript x;

    public void click()
    {
        ArtefactManager.instance.OpenArtefactManager(x);
    }
}
