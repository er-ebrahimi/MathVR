using UnityEngine;

public class StartGameTwoScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("XROrigin"))
        {
            Transform parent = transform.parent;
            if (parent != null)
            {
                LevelSecondLogicScript levelOneLogicScript = parent.GetComponent<LevelSecondLogicScript>();
                if (levelOneLogicScript != null)
                {
                    levelOneLogicScript.StartGame();
                    gameObject.SetActive(false);
                }
                else
                {
                    print("Parent start game script not found");
                }
            }
        }
    }
}

