using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private string testName;
    public void Test()
    {
        Debug.Log("pp");
        VLCCManager.instance.SetUpVLCC(testName);
    }
}
