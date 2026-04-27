using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] private float distancePickup;

    [SerializeField] private LayerMask pickupLayer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            distancePickup,
            pickupLayer
        );

        foreach (var hit in hits)
        {
            IPickUp pickup = hit.GetComponent<IPickUp>();
            if (pickup != null)
            {
                pickup.InRange();
            }
        }
    }
}
