using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    public static PauseSystem instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    [SerializeField] private bool onPause;


}
