using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    //[SerializeField] private GameObject playerPrefab;

    //public void ChoosePlayer(GameObject player)
    //{
    //    playerPrefab = player;
    //}

}
