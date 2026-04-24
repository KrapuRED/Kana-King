using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] private float distancePickup;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, distancePickup);
        if (hit != null)
        {
            IPickUp pickup = hit.GetComponent<IPickUp>();
            if(pickup != null)
            {
                pickup.Do();
            }
        }
    }
}
