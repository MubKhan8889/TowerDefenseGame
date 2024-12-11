struct MapWonData
{
    public string MapName { get; set; }
    public bool EasyWin { get; set; }
    public bool NormalWin { get; set; }
    public bool HardWin { get; set; }

    public MapWonData(string setMapName, bool setEasyWin, bool setNormalWin, bool setHardWin)
    {
        MapName = setMapName;
        EasyWin = setEasyWin;
        NormalWin = setNormalWin;
        HardWin = setHardWin;
    }
}