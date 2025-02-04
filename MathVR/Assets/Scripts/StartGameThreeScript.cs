using UnityEngine;

public class StartGameThreeScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("XROrigin"))
        {
            Debug.Log("XR Origin entered StartLevel trigger!");
            Transform parent = transform.parent;
            if (parent != null)
            {
                LevelThreeLogicScript levelThreeLogicScript = parent.GetComponent<LevelThreeLogicScript>();
                if (levelThreeLogicScript != null)
                {
                    levelThreeLogicScript.StartGame();
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

