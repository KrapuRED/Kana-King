using UnityEngine;
using TMPro;

public class LoseScript : MonoBehaviour
{
    [SerializeField] private GameObject losePanel;
    [SerializeField] private TMP_Text loseText;

    public void LoseUISetUp()
    {
        losePanel.SetActive(true);
        loseText.text = $"Total Enemy Defeated = {EnemySpawner.instance.ReturnTotalEnemyDefeated()}";
        PauseSystem.instance.AddPauseRequest();
    }

    public void RestartGame()
    {
        PauseSystem.instance.RemovePauseRequest();
        SceneController.instance.ReloadScene();
    }

    public void ExitGame()
    {
        PauseSystem.instance.RemovePauseRequest();
        SceneController.instance.ReloadScene();
    }

}
