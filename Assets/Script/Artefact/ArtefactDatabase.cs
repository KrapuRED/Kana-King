using System.Collections.Generic;
using UnityEngine;

public class ArtefactDatabase : MonoBehaviour
{
    public static ArtefactDatabase instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [SerializeField] private List<ArtefactScript> allArtefact;


}
