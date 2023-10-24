using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace RsGEN
{
    /**
     * Purely making this class to avoid having to drag endless scripts into the scene when introducing classes that
     * really don't need to be MonoBehaviours.
     */
    public class ProgramInitalizer : MonoBehaviour
    {
        [SerializeField] private TextAsset carDataJson;
        [SerializeField] private TextAsset trackDataJson;
        [SerializeField] private TextAsset raceSettingsDataJson;

        private async void Start()
        {
            var dataLoader = InitializeAsync<DataLoader>(carDataJson, trackDataJson, raceSettingsDataJson);
            var tasks = new List<Task>
            {
                InitializeAsync<RaceGen>(),
                InitializeAsync<RSSetup>(),
                InitializeAsync<CarListController>(),
                InitializeAsync<CarDataProcessor>(),
                dataLoader
            };
            await Task.WhenAll(tasks);

            dataLoader.Result.LoadCarData();
            dataLoader.Result.LoadTrackData();
            dataLoader.Result.LoadRaceSettings();
        }


        private async Task<T> InitializeAsync<T>(params object[] args) where T : class
        {
            T instance = null;

            await Task.Run(() => instance = (T)Activator.CreateInstance(typeof(T), args));

            return instance;
        }

        private async Task<T> InitializeAsync<T>(int delay, params object[] args) where T : class
        {
            T instance = null;

            await Task.Delay(delay);
            await Task.Run(() => instance = (T)Activator.CreateInstance(typeof(T), args));

            return instance;
        }
    }
}