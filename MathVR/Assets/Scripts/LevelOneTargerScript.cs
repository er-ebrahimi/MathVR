using TMPro;
using UnityEngine;

public class LevelOneTargerScript : MonoBehaviour
{
    public TextMeshPro targetText;
    public bool isCorrectAnswer = false;
    public LevelOneLogicScript levelOneLogicScript;
    [SerializeField] private GameObject vfx;
    private ParticleSystem vfxParticleSystem;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (vfx != null)
        {
            vfxParticleSystem = vfx.GetComponent<ParticleSystem>();
            vfx.SetActive(false);  // Ensure VFX is initially inactive
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Axe")
        {
            Destroy(collision.gameObject);
            if (!isCorrectAnswer && levelOneLogicScript.IsGameStarted())
            {
                levelOneLogicScript.counter = levelOneLogicScript.rounds;
                levelOneLogicScript.CreateAxe();
            }
            if (isCorrectAnswer)
            {
                levelOneLogicScript.database.GetComponent<DataBaseScript>().levelOneCorrectAnswer++;
            }
            audioSource.Play();
            vfx.SetActive(true);
            vfxParticleSystem.Play();
            levelOneLogicScript.SetEquation();
        }
    }
}
