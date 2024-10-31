using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayAndNightCycle : MonoBehaviour
{
    [System.Serializable]
    public struct DayAndNightMarks
    {
            public float timeRatio;
            public float Intensity;
            public Color Color;
    }
    

    
    [SerializeField] private DayAndNightMarks[] _marks;
    [SerializeField] private float _cycleLength = 24f; // in seconds
    [SerializeField] private Light2D _light;
    
    private const float _TIME_CHECK_EPSILON = 0.1f;
    
    private float _currentCycleTime;
    private float _markTimeDifference;
    private float _currentMarkTime, _nextMarkTime;
    private int _currentMarkIndex, _nextMarkIndex;
    
    void Start()
    {
        _currentMarkIndex = -1;
        CycleMarks();
    }

    void Update()
    {
        _currentCycleTime = (_currentCycleTime + Time.deltaTime) % _cycleLength;
        
        float t = (_currentCycleTime - _currentMarkTime) / _markTimeDifference;
        DayAndNightMarks cur = _marks[_currentMarkIndex];
        DayAndNightMarks next = _marks[_nextMarkIndex];
        _light.color = Color.Lerp(cur.Color, next.Color, t);
        _light.intensity = Mathf.Lerp(cur.Intensity, next.Intensity, t);
        
        
        if(Mathf.Abs(_currentCycleTime - _currentMarkTime) < _TIME_CHECK_EPSILON)
        { 
            _light.color = next.Color;
            _light.intensity = next.Intensity;
            
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
        _currentMarkTime = _marks[_currentMarkIndex].timeRatio * _cycleLength;
        _nextMarkTime = (_currentMarkIndex + 1) % _marks.Length;
        _markTimeDifference = _nextMarkTime - _currentMarkTime;
        if (_markTimeDifference < 0)
        {
            _markTimeDifference += _cycleLength;
        }
    }
}
