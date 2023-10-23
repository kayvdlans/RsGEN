using UnityEngine;

namespace RsGEN
{
    /**
     * Purely making this class to avoid having to drag endless scripts into the scene when introducing classes that
     * really don't need to be MonoBehaviours.
     * 
     * If objects are dependent on others they currently require to be initalized before the dependancy.
     * We can fix this by forcing the DataLoader to wait with initialization by an artificial delay, although not ideal
     * this would fix the issue. For now not necessary though.
     */
    public class ProgramInitalizer : MonoBehaviour
    {
        [SerializeField] private TextAsset carDataJson;
        [SerializeField] private TextAsset trackDataJson;
        [SerializeField] private TextAsset raceSettingsDataJson;

        //saving them to variables for now, probably won't need them
        private DataLoader _dataLoader;
        private CarDataProcessor _carDataProcessor;
        private CarListController _carListController;
        private RaceGen _raceGen;

        private void Start()
        {
            _raceGen = new RaceGen();
            _carListController = new CarListController();
            _carDataProcessor = new CarDataProcessor();
            _dataLoader = new DataLoader(carDataJson, trackDataJson, raceSettingsDataJson);
        }
    }
}