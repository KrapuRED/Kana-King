using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CrateFill
{
    public GameObject itemFil;
    public float itemDropChance;
}


public class CrateScript : MonoBehaviour, IInteractable
{

    [SerializeField] private List<CrateFill> itemDrop;
    [SerializeField] private Transform playerLocation;
    bool canInteract;


    private void Awake()
    {
        // Cache the player reference ONLY ONCE when the object is first created
        if (playerLocation == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerLocation = player.transform;
            }
            else
            {
                Debug.LogWarning($"Player tag not found in scene by {gameObject.name}!");
            }
        }
    }

    private void OnEnable()
    {
        // Safe to reset state here for pooling reuse
        canInteract = true;
    }

    private void OnDisable()
    {
        canInteract = false;
    }

    public void Interact()
    {
        if(!canInteract) return;
        //CrateFill chosenItem = GetRandomCrateFill();

        //if (chosenItem != null && chosenItem.itemFil != null)
        //{
        //    Instantiate(chosenItem.itemFil, transform.position, Quaternion.identity);
        //}
        Destroy(gameObject);
    }

    // Renamed to follow standard C# naming conventions (PascalCase for methods)
    public CrateFill GetRandomCrateFill()
    {
        if (itemDrop == null || itemDrop.Count == 0) return null;

        // 2. Must initialize 'total' to 0
        float total = 0f;
        foreach (var item in itemDrop)
        {
            total += item.itemDropChance;
        }

        // 3. Roll a random number between 0 and your total weight
        float randomRoll = Random.Range(0f, total);

        foreach (var item in itemDrop)
        {
            randomRoll -= item.itemDropChance;
            if (randomRoll <= 0)
            {
                return item;
            }
        }

        // Fallback (just in case of weird floating-point precision issues)
        return itemDrop[itemDrop.Count - 1];
    }
}
