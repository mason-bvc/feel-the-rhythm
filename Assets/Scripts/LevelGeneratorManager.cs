using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratorManager : MonoBehaviour
{
    [SerializeField] private List<Segments>[] _segments = new List<Segments>[3];
    private GameManager.LevelDireciton currentDirection = GameManager.LevelDireciton.Up;
    private SegmentPlacer _segmentPlacer;

    private void Start()
    {
        _segmentPlacer = FindFirstObjectByType<SegmentPlacer>();
    }


    public void GenerateLevel(int neededSegments)
    {
        int direction = 0;
        while (neededSegments > 0)
        {
            neededSegments--;
            direction = Random.Range(0, _segments.Length -1);
            _segmentPlacer.PlaceSegment(_segments[direction][Random.Range(0, _segments[direction].Count -1)]);
        }
    }



}
