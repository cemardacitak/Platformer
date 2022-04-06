using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth {get; private set;}

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float takenDamage)
    {
        currentHealth = Mathf.Clamp(currentHealth - takenDamage, 0, 100);

        if (currentHealth > 0)
        {
            //player hurt
        }
        else
        {
            //player ate dick
        }
        


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }
    }

}
