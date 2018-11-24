using System;
using UnityEngine;

public struct SoilTile
{
    public Data.SoilDescription Description;

    public Plant Plant;
    public Dna Dna;

    public int WaterLevel;
    public int SunshineLevel;
    public int WindLevel;
    public int QualityLevel;
    public int Elevation;

    internal void Initialize(int sunshine, int wind)
    {
        this.SunshineLevel = sunshine;
        this.WindLevel = wind;
        this.WaterLevel = this.Description.InitialWaterLevel;
        this.QualityLevel = this.Description.InitialQualityLevel;
    }

    internal void UpdateQuality(int delta)
    {
        this.QualityLevel += delta;
        Mathf.Clamp(this.QualityLevel, 0, 100);
    }
}
