using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class LevelSecondTargerScript : MonoBehaviour
{
    public TextMeshPro targetText;
    public bool isCorrectAnswer = false;
    public LevelSecondLogicScript levelSecondLogicScript;
    public GameObject cutBarrelPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sword")
        {
            if (isCorrectAnswer)
            {
                levelSecondLogicScript.sendOn = true;
                levelSecondLogicScript.database.GetComponent<DataBaseScript>().levelTwoCorrectAnswer++;
                cutBarrelPrefab.GetComponent<CutBarrelScript>().explosion.gameObject.SetActive(false);
                cutBarrelPrefab.GetComponent<CutBarrelScript>().smoke.gameObject.SetActive(true);
                cutBarrelPrefab.GetComponent<CutBarrelScript>().smoke.Play();
            }
            else
            {
                levelSecondLogicScript.counter = levelSecondLogicScript.rounds;
                cutBarrelPrefab.GetComponent<CutBarrelScript>().smoke.gameObject.SetActive(false);
                cutBarrelPrefab.GetComponent<CutBarrelScript>().explosion.gameObject.SetActive(true);
                cutBarrelPrefab.GetComponent<CutBarrelScript>().explosion.Play();
            }
            Instantiate(cutBarrelPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
