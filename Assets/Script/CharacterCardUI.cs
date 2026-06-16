using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Button chooseButton;

    private PlayerSO playerSO;

    public void Setup(PlayerSO playerData)
    {
        playerSO = playerData;

        nameText.text = playerSO.CharacterName;
        characterImage.sprite = playerSO.CharacterSprite;
        descriptionText.text = playerSO.Description;

        chooseButton.onClick.RemoveAllListeners();
        chooseButton.onClick.AddListener(OnChooseCharacter);
    }

    private void OnChooseCharacter()
    {
        Debug.Log("Selected : " + playerSO.CharacterName);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}