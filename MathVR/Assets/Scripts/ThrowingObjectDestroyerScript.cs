using UnityEngine;

public class ThrowingObjectDestroyerScript : MonoBehaviour
{
    public LevelSecondLogicScript levelSecondLogicScript;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ThrowingObject")
        {
            if (other.GetComponent<LevelSecondTargerScript>().isCorrectAnswer)
            {
                levelSecondLogicScript.counter = levelSecondLogicScript.rounds;
                levelSecondLogicScript.sendOn = true;
            }
            Destroy(other.gameObject);
        }
    }
}
