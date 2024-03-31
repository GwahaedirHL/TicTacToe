using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PopupsCollectionInstaller", menuName = "Installers/PopupsCollectionInstaller")]
public class PopupsCollectionInstaller : ScriptableObjectInstaller<PopupsCollectionInstaller>
{
    public PopupsCollection popupsCollection;

    public override void InstallBindings()
    {
        Container.Bind<PopupsCollection>().FromInstance(popupsCollection).AsSingle();
    }
}