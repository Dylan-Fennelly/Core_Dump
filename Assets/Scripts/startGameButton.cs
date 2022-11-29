using UnityEngine;
using UnityEngine.SceneManagement;
namespace DefaultNamespace
{
    public class startGameButton : MonoBehaviour
    {
        public int gameStartScene;

        public void gameStart()
        {
            SceneManager.LoadScene((gameStartScene));
        }
    }
}