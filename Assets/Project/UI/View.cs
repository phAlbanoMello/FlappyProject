using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] private bool _isEnabledAtStart;

    public bool IsEnabledAtStart { get => _isEnabledAtStart; }
}
