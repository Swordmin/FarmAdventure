using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelBehaviour : MonoBehaviour
{
    [SerializeField] private Button _restartButton;

    private void OnEnable()
    {
        _restartButton?.onClick.AddListener(Restart);
    }

    private void OnDisable()
    {
        _restartButton?.onClick.AddListener(Restart);
    }

    private void Restart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Lose() 
    {
        Restart();
    }

}
