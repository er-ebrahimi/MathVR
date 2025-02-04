using UnityEngine;

public class ThrowObjectScript : MonoBehaviour
{
    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public Transform[] spawnPoints;
    private float baseSpeed = 10f;
    public float speedMultiplier = 1f;

    [Header("Target Settings")]
    public Transform target;

    [Header("Throw Settings")]
    public GameObject[] Points;
    public float throwInterval = 0.1f;

    void Start()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("SpawnPoints not assigned. Please assign at least one spawn point in the Inspector.");
        }
        if (target == null)
        {
            Debug.LogError("Target not assigned. Please assign the target in the Inspector.");
        }
    }
    void Update()
    {
        if (gameObject.GetComponent<LevelSecondLogicScript>().counter > 0 && gameObject.GetComponent<LevelSecondLogicScript>().sendOn)
        {
            //throwTimer += Time.deltaTime;
            //if (throwTimer >= throwInterval)
            //{
                ThrowProjectiles();
            //}
            gameObject.GetComponent<LevelSecondLogicScript>().sendOn = false;
        }
    }

    void ThrowProjectiles()
    {
        if (projectilePrefab == null || target == null)
        {
            Debug.LogError("ProjectilePrefab or Target not assigned.");
            return;
        }
        gameObject.GetComponent<LevelSecondLogicScript>().answerTexts.Clear();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPoints[i] == null)
            {
                Debug.LogError("One of the SpawnPoints is not assigned.");
                continue;
            }
            Vector3 direction = (target.position - spawnPoints[i].position).normalized;
            //Quaternion rotation = Quaternion.LookRotation(direction);
            //GameObject projectile = Instantiate(projectilePrefab, spawnPoints[i].position, rotation);
            GameObject projectile = Instantiate(projectilePrefab, spawnPoints[i].position, Quaternion.identity);
            projectile.GetComponent<ParabolaController>().ParabolaRoot = Points[i];
            float scaledSpeed = baseSpeed * speedMultiplier;
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(direction * scaledSpeed * 100);
            }
            else
            {
                Debug.LogError("Projectile prefab does not have a Rigidbody component.");
            }
            projectile.GetComponent<LevelSecondTargerScript>().levelSecondLogicScript = gameObject.GetComponent<LevelSecondLogicScript>();
            gameObject.GetComponent<LevelSecondLogicScript>().answerTexts.Add(projectile.gameObject);
        }
        gameObject.GetComponent<LevelSecondLogicScript>().SetEquation();
    }
    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }
}
