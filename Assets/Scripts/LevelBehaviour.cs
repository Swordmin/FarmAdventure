using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class LevelBehaviour : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private string _nextLevelName;
    [SerializeField] private Image _fadePanel;
    [SerializeField] private float _fadeTime;
    private void OnEnable()
    {
        _restartButton?.onClick.AddListener(Restart);
        _nextLevelButton?.onClick.AddListener(LoadNextLevel);
        SetupFadePanel();
    }

    private void OnDisable()
    {
        _restartButton?.onClick.AddListener(Restart);
        _nextLevelButton?.onClick.RemoveListener(LoadNextLevel);
    }

    private void Restart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Lose() 
    {
        Restart();
    }

    private void LoadNextLevel() 
    {
        _fadePanel.DOFade(1, _fadeTime).OnComplete(() => SceneManager.LoadScene(_nextLevelName));
    }

    private void SetupFadePanel() 
    {
        _fadePanel.color = new Color(0, 0, 0);
        _fadePanel.DOFade(0, _fadeTime);
    }

}
