using UnityEngine;
using TMPro;
public class KatakanaPrefabScript : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    public void SetUp(char x)
    {
        text.text = x.ToString();
    }
}
