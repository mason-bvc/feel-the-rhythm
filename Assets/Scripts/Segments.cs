using System;
using UnityEngine;


[Serializable]
public class Segments : MonoBehaviour
{
    [SerializeField] private GameObject endObject;

    public GameObject GetEndObject()
    {  return endObject; }
}
