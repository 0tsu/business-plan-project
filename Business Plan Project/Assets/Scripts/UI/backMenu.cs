
using UnityEngine;
using UnityEngine.SceneManagement;

public class backMenu : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GoMenu();
    }

    public void GoMenu()
    {
        SceneManager.LoadScene(0);
    } 
}
