namespace RsGEN
{
    public struct RSPreset
    {
        public enum PresetType
        {
            DistanceKM,
            DistanceMiles,
            Endurance
        }

        public string Name { get; private set; }
        public PresetType Type { get; private set; }
        public int Distance { get; private set; }
        public string EnduranceOption { get; }

        public static RSPreset[] DefaultPresets { get; } =
        {
            new()
            {
                Name = "Short Race",
                Type = PresetType.DistanceKM,
                Distance = 25
            },
            new()
            {
                Name = "Medium Race",
                Type = PresetType.DistanceKM,
                Distance = 75
            },
            new()
            {
                Name = "Long Race",
                Type = PresetType.DistanceKM,
                Distance = 150
            },
            new()
            {
                Name = "Grand Prix",
                Type = PresetType.DistanceKM,
                Distance = 300
            }
        };
    }
}