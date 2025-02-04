using TMPro;
using UnityEngine;

public class LevelThreeTargetScript : MonoBehaviour
{
    public TextMeshPro targetText;
    public bool isCorrectAnswer = false;
    public LevelThreeLogicScript levelThreeLogicScript;
    public ParticleSystem particle;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BoxingGlove")
        {
            particle.gameObject.SetActive(true);
            gameObject.SetActive(false);
            levelThreeLogicScript.CheckAnswers();
        }
    }
}
