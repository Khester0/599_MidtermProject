using UnityEngine;

public class healthpickup : MonoBehaviour
{
    public float healthRestore  = 10 ;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageTaken damageTaken = collision.GetComponent<DamageTaken>();

        if(damageTaken && damageTaken.Health < damageTaken.MaxHealth)
        {
            bool wasHealed = damageTaken.Heal(healthRestore);

            if(wasHealed)
            {
                Destroy(gameObject);
            }
            
        }
    }
}

