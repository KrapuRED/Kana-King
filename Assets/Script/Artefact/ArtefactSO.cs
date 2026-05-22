using UnityEngine;

[CreateAssetMenu(fileName = "ArtefactSO", menuName = "Scriptable Objects/ArtefactSO")]
public class ArtefactSO : ScriptableObject
{
    public string artefactName;
    public Sprite artefactSprite;
    public string artefactDescription;
}
