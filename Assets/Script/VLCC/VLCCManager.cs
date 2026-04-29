using UnityEngine;
using TMPro;

public class VLCCManager : MonoBehaviour
{
    public static VLCCManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }


    [SerializeField] private float duration = 5f;

    [SerializeField] private DataVLCC dataVLCC;

    private void Update()
    {
        if (duration > 0)
            duration -= Time.deltaTime;
    }

    public float GetDuration()
    {
        return duration;
    }
    public void SetDuration(float x, string name)
    {
        SetUpVLCC(name);
        duration = x;
    }

    public void SetUpVLCC(string name)
    {
        dataVLCC = DatabaseVLCC.instance.FindData(name);
    }


}
