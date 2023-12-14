using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class PlayerSettings
{
    [Header("Target Actor")]
    [SerializeField] private GameObject _playerGameObject;
    [Header("Collision Detection")]
    [SerializeField] private LayerMask _hazardLayer;
    [Header("Actions")]
    [SerializeField] private InputAction _jump;
    [Header("Movement")]
    [SerializeField] private MovementData _movementData;

    public GameObject PlayerGameObject { get => _playerGameObject; private set { } }
    public LayerMask HazardLayer { get => _hazardLayer; private set { } }
    public InputAction Jump { get => _jump; private set { } }
    public MovementData MovementData { get => _movementData; private set { } }
}
