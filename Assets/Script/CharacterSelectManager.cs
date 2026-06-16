using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectManager : MonoBehaviour
{
    [SerializeField] private List<PlayerSO> playerList;
    [SerializeField] private List<CharacterCardUI> cardList;

    private void Start()
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            cardList[i].Setup(playerList[i]);
        }
    }
}