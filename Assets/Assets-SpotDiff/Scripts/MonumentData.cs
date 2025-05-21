using SpotTheDifference;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonumentScriptableObject", menuName = "MonumentScriptableObject/MonumentData", order = 1)]

public class MonumentData : ScriptableObject
{
    public List<MonumentModel> monumentModels;
}

[System.Serializable]
public class MonumentModel
{
    public DataModel.MounMentType _monumentType;
    public List<ModeClass> _modeClasses;
    public int _totalScore;
}

[System.Serializable]
public class ModeClass
{
    public DataModel.ModeType _modeType;
    public int _score;
    public bool isPlayed;
}