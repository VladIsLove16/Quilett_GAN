using CodeBase.Services.Card;
using CodeBase.UI;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameObject reviewWindowPrefab;
    [SerializeField] private GameObject testModeWindowPrefab;
    [SerializeField] private GameObject writeModeWindowPrefab;
    [SerializeField] private GameObject settingsWindowPrefab; // Префаб окна настроек

    public override void InstallBindings()
    {
        Container.Bind<ICardManager>().To<CardService>().AsSingle();
        Container.Bind<ReviewWindow>().FromComponentInNewPrefab(reviewWindowPrefab).AsSingle();
        Container.Bind<TestModeWindow>().FromComponentInNewPrefab(testModeWindowPrefab).AsTransient();
        Container.Bind<WriteModeWindow>().FromComponentInNewPrefab(writeModeWindowPrefab).AsSingle();
        Container.Bind<SettingsWindow>().FromComponentInNewPrefab(settingsWindowPrefab).AsSingle(); // Добавляем привязку окна настроек
    }
}