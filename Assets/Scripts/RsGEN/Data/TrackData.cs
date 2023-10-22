using System;
using System.Collections.Generic;
using System.Linq;

namespace RsGEN.Data
{
    [Serializable]
    public struct TrackDataCollection
    {
        public TrackData[] tracks;

        public List<TrackVariant> GetAllTrackVariants()
        {
            var variants = new List<TrackVariant>();
            foreach (var track in tracks) variants.AddRange(track.variants);

            return variants;
        }

        public TrackData GetTrackFromVariant(TrackVariant variant)
        {
            foreach (var track in tracks)
                if (track.variants.Contains(variant))
                    return track;

            return TrackData.Empty;
        }
    }

    [Serializable]
    public struct TrackData
    {
        public string name;
        public string continent;
        public string country;
        public TrackVariant[] variants;

        public static TrackData Empty => new()
        {
            name = "",
            continent = "",
            country = "",
            variants = Array.Empty<TrackVariant>()
        };
    }

    [Serializable]
    public struct TrackVariant
    {
        public string name;
        public float length_km;
        public bool reverse_available;
        public bool rain_possible;
        public string[] excluded_time_settings;

        public static TrackVariant Empty => new()
        {
            name = "",
            length_km = 0f,
            reverse_available = false,
            rain_possible = false,
            excluded_time_settings = Array.Empty<string>()
        };
    }
}