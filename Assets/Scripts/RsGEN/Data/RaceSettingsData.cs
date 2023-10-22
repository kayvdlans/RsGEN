using System;

namespace RsGEN.Data
{
    [Serializable]
    public struct RaceSettingsData
    {
        public RaceSettingsOption race_type;
        public RaceSettingsRange lap_count;
        public RaceSettingsOption time_limit;
        public RaceSettingsRange car_count;
        public RaceSettingsRange starting_grid;
        public RaceSettingsOption start_type;
        public RaceSettingsOption rolling_start_interval;
        public RaceSettingsOption bop;
        public RaceSettingsOption boost;
        public RaceSettingsOption slipstream_strength;
        public RaceSettingsOption mechanical_damage;
        public RaceSettingsRange tyre_wear;
        public RaceSettingsRange fuel_consumption;
        public RaceSettingsRange refuel_speed;
        public RaceSettingsOption grip_reduction;
        public RaceSettingsRange nitrous_time_multiplier;
        public RaceSettingsOption weather_selection_method;
        public RaceSettingsOption preset_weather_types;
        public RaceSettingsOption equal_conditions_mode;
        public RaceSettingsOption time_of_day;
        public RaceSettingsRange variable_time_scale;
    }

    [Serializable]
    public struct RaceSettingsOption
    {
        public string name;
        public string[] options;
    }

    [Serializable]
    public struct RaceSettingsRange
    {
        public string name;
        public Range range;
        public int increment;
    }

    [Serializable]
    public struct Range
    {
        public int min;
        public int max;
    }
}