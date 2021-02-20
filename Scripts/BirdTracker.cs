using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdTracker : MonoBehaviour
{
    [SerializeField] private Bird _bird;
    [SerializeField] private float _xOffset; // отступ камеры по оси X вправо

    private void Update()
    {
        transform.position = new Vector3(_bird.transform.position.x + _xOffset, transform.position.y, transform.position.z); // перемещаем камеру по оси X, относительно позиции птицы 
    }
}
