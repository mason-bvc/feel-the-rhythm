using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.HableCurve;

public class LevelGeneratorManager : MonoBehaviour
{
    [SerializeField] private List<Segments> _straightSegments;
    [SerializeField] private List<Segments> _leftSegments;
    [SerializeField] private List<Segments> _rightSegments;

    [SerializeField] private Segments _endSegment;

    private SegmentPlacer _segmentPlacer;

    [SerializeField] private int _segmentsToPlace;

    private void Start()
    {
        _segmentPlacer = FindFirstObjectByType<SegmentPlacer>();
        GenerateLevel(_segmentsToPlace);
    }


    public void GenerateLevel(int neededSegments)
    {
        while (neededSegments > 0)
        {
            neededSegments--;
            switch(Random.Range(1,4))
            {
                case 1:
                    _segmentPlacer.PlaceSegment(_straightSegments[Random.Range(0, _straightSegments.Count)]);
                    break;
                case 2:
                    _segmentPlacer.PlaceSegment(_leftSegments[Random.Range(0, _leftSegments.Count)]);
                    break;
                case 3:
                    _segmentPlacer.PlaceSegment(_rightSegments[Random.Range(0, _rightSegments.Count)]);
                    break;
            }
        }
        _segmentPlacer.PlaceSegment(_endSegment);
    }



}
