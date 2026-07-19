using UnityEngine;

[System.Serializable]
public enum TypeBuff
{
    RegenHPEachSecond,
    RegenHPWhenDamaging,
    DecreaseCooldownSkill,
    IncreaseDamagePercentage
}


[CreateAssetMenu(fileName = "ArtefactSO", menuName = "Scriptable Objects/ArtefactSO")]
public class ArtefactSO : ScriptableObject
{
    public string artefactName;
    public Sprite artefactSprite;
    [TextArea(3, 5)] public string artefactDescription; // Menambahkan TextArea agar lebih mudah edit di Inspector
    public TypeBuff artefactBuff;
    public int buffValue;
}
