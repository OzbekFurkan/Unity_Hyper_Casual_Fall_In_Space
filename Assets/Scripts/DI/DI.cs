using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Game;
using UI;

public class DI : MonoInstaller
{
    [SerializeField] GameObject planet;

    public override void InstallBindings()
    {
        Container.Bind<GameObject>().FromInstance(planet);
        Container.Bind<GameLogic>().FromComponentInHierarchy().AsSingle();
        Container.Bind<UIManager>().FromComponentInHierarchy().AsSingle();
    }
}
