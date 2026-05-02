using UnityEngine;
using TMPro;
public class RomajiAnswerScript : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    public void SetUp(string x)
    {
        text.text = x.ToString();
    }
}
