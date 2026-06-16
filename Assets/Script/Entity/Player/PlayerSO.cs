using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player SO", menuName = "Scriptable Objects/Player SO")]
public class PlayerSO : ScriptableObject
{
    [SerializeField] private string characterName;
    [SerializeField] private Sprite characterSprite;

    [TextArea]
    [SerializeField] private string description;

    [SerializeField] private List<BaseStatData> baseStatDatas;
    [SerializeField] private GameObject playerGameObject;

    public string CharacterName => characterName;
    public Sprite CharacterSprite => characterSprite;
    public string Description => description;
    public List<BaseStatData> BaseStatDatas => baseStatDatas;
    public GameObject PlayerGameObject => playerGameObject;
}