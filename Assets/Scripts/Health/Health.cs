using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth;

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

}
