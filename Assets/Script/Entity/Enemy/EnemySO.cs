using UnityEngine;

public enum EnemyType
{
    Normal,
    Boss
}

[CreateAssetMenu(fileName = "New Enemy", menuName = "Scriptable Objects/Enemy Data")]
public class EnemySO : ScriptableObject
{
    public float maxHealth;
    public string enemyName;
    public int damage;
    public float speed;
    public EnemyType enemyType;
    public Sprite enemySprite;
    public GameObject enemyPrefab;
    public int exp;
}