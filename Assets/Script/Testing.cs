using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private string testName;
    [SerializeField] private float duration = 5f;
    public void Test()
    {
        Debug.Log("pp");
        VLCCManager.instance.SetDuration(duration, testName);
    }
}
