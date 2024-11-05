using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class DayAndNightCycle : MonoBehaviour
{
    [System.Serializable]
    public struct DayAndNightMarks
    {
            [FormerlySerializedAs("timeRatio")]public float MarkLength;
            public float Intensity;
            public Color Color;
    }
    

    
    [SerializeField] private DayAndNightMarks[] _marks;
    [FormerlySerializedAs("_cycleLength")] [SerializeField] private float _cycleLengthMultiplier = 60f; // in seconds
    [SerializeField] private Light2D _light;
    
    private const float _TIME_CHECK_EPSILON = 0.999f;
    
    private float _currentCycleTime;
    private int _currentMarkIndex, _nextMarkIndex;
    
    void Start()
    {
        _currentMarkIndex = -1;
        CycleMarks();
    }

    void Update()
    {
        _currentCycleTime += Time.deltaTime / (_marks[_currentMarkIndex].MarkLength*_cycleLengthMultiplier); 
        DayAndNightMarks cur = _marks[_currentMarkIndex];
        DayAndNightMarks next = _marks[_nextMarkIndex];
        _light.color = Color.Lerp(cur.Color, next.Color, _currentCycleTime);
        _light.intensity = Mathf.Lerp(cur.Intensity, next.Intensity, _currentCycleTime);
        
        
        if(_currentCycleTime > _TIME_CHECK_EPSILON)
        { 
            CycleMarks();
        }
        // reset le currentcycletime pour éviter les erreurs de float
        // if (_currentCycleTime > 50000)
        // {
        //     _currentCycleTime = 0;
        // }
    }

    void CycleMarks()
    {
        _currentMarkIndex = (_currentMarkIndex + 1) % _marks.Length;
        _nextMarkIndex = (_currentMarkIndex + 1) % _marks.Length;
        _currentCycleTime =0;

    }
}
