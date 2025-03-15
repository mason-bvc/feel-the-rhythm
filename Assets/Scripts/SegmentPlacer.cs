using UnityEngine;

public class SegmentPlacer : MonoBehaviour
{
    [SerializeField] private Transform _currentLocation;


    public void PlaceSegment(Segments newSegment)
    {
        GameObject placeSegment = Instantiate(newSegment.gameObject,_currentLocation.position,_currentLocation.rotation);

        if (placeSegment != null && placeSegment.GetComponent<Segments>())
        {
           _currentLocation = placeSegment.GetComponent<Segments>().GetEndObject().transform;
        }
    }
}
