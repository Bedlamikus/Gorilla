using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    private Joystick _joyStick;
    private bool _mooving;
    private Vector2 _direction;
    private Player _player;

    public int _playerSpeed;

    void Start()
    {
        _joyStick = FindObjectOfType<Joystick>();
        if (_joyStick == null) throw new System.Exception("Joystik not find");
        _mooving = false;
        TryFindPlayer();
    }

    void Update()
    {
        if (_joyStick.Direction == Vector2.zero)
        {
            if (_mooving == true)
            {
                _mooving = false;
                _direction = Vector2.zero;
                MoovingPlayer(_direction);
            }
            return;
        }
        _mooving = true;
        _direction = _joyStick.Direction;
        MoovingPlayer(_direction);

    }

    private void MoovingPlayer(Vector2 direction)
    {
        if (_player == null)
        {
            TryFindPlayer();
        }
        Transform tr = _player.transform;
        Vector3 position = tr.position;
        if (direction.magnitude > 0.1)
            tr.rotation = Quaternion.Euler(0, 180 * Mathf.Atan2(direction.y, -direction.x) / Mathf.PI, 0);

        Vector3 desiredMove = tr.forward * direction.magnitude;

        position.x += -desiredMove.z * _playerSpeed * Time.deltaTime;
        position.z += desiredMove.x * _playerSpeed * Time.deltaTime;

        _player.transform.SetPositionAndRotation(position, tr.rotation);

    }

    private void TryFindPlayer()
    {
        _player = FindObjectOfType<Player>();
        if (_player == null) throw new System.Exception("Player not find");
    }
}
