using UnityEngine;

public class SegmentPlacer : MonoBehaviour
{
    private Transform _currentLocation;
    [SerializeField] private Segments _startSegment;

    private void Start()
    {
        GameObject placeSegment = Instantiate(_startSegment.gameObject, new Vector3(0,0,0), new Quaternion(0,0,0,0));
        _currentLocation = placeSegment.GetComponent<Segments>().GetEndObject().transform;
    }

    public void PlaceSegment(Segments newSegment)
    {
        GameObject placeSegment = Instantiate(newSegment.gameObject,_currentLocation.position,_currentLocation.rotation);

        if (placeSegment != null && placeSegment.GetComponent<Segments>())
        {

           _currentLocation = placeSegment.GetComponent<Segments>().GetEndObject().transform;
        }
    }
}
