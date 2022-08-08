using UnityEngine;
using Zenject;

public class SampleSceneInstaller : MonoInstaller
{

    [SerializeField] private SceneBlockInput _sceneBlockInput;
    [SerializeField] private LevelStateMachine _levelStateMachine;
    public override void InstallBindings()
    {
        Container.BindInstance(_sceneBlockInput).AsSingle();
        Container.BindInstance(_levelStateMachine).AsSingle();
    }
}