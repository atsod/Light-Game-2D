using System.Collections;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<Enemy>() != null)
        {
            // Добавить анимацию смерти
            StartCoroutine(SafeReloadSceneAfterDeath(2f));
        }
    }

    private IEnumerator SafeReloadSceneAfterDeath(float seconds)
    {
        LevelController.StopLevelTime();

        yield return new WaitForSeconds(seconds);

        SceneLoader.ReloadScene();
    }
}
