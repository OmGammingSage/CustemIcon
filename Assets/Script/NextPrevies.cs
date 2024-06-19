using System;
using UnityEngine;

public class NextPrevie : MonoBehaviour
{
    internal int _CurrentIndex = 0;

    internal int _PriviesIndex = 0;

    [SerializeField] private Transform _ParentOfChangeImage;

    public virtual void ChangeImage(int index)
    {
        if (index == 1)
        {
            _CurrentIndex++;
            if (_ParentOfChangeImage.childCount <= _CurrentIndex)
            {
                _CurrentIndex = 0;
            }

            _ParentOfChangeImage.GetChild(_PriviesIndex).gameObject.SetActive(false);
            _ParentOfChangeImage.GetChild(_CurrentIndex).gameObject.SetActive(true);
        }
        else
        {
            _CurrentIndex--;
            if (_CurrentIndex <= -1)
            {
                _CurrentIndex = _ParentOfChangeImage.childCount-1;
            }

            _ParentOfChangeImage.GetChild(_PriviesIndex).gameObject.SetActive(false);
            _ParentOfChangeImage.GetChild(_CurrentIndex).gameObject.SetActive(true);
        }

        _PriviesIndex = _CurrentIndex;
    }
    
}