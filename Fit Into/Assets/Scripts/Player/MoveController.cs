﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveController : MonoBehaviour
{

    [Serializable]
    public class MultiplerOverTime
    {
        [SerializeField]
        public float StartTime;
        [SerializeField]
        public float Multipler;
    }

    private List<IMoveModifier> _customModifiers = new List<IMoveModifier>();
    private MoveDirection _lastJump = MoveDirection.None;

    [SerializeField]
    private float _basicSpeed = 10f;
    [SerializeField]
    private float _basicJumpIntervalSec = 1f;

    public MultiplerOverTime[] SpeedMultiplers;
    public MultiplerOverTime[] JumpMultiplers;

    public void AddNewModifier(IMoveModifier moveModifier)
    {
        _customModifiers.Add(moveModifier);
    }

    public void ResetState()
    {
        transform.position = Vector3.zero;
        _timeFromLastJump = 0f;
        _startTime = DateTime.UtcNow;
        _currentRail = Rails.First(x => x.WorldPositionX == 0 && x.WorldPositionY == 0);
    }

    public void Move(MoveDirection moveDirection)
    {
        if (_lastJump == MoveDirection.None && _timeFromLastJump >= _basicJumpIntervalSec * SelectMultipler(JumpMultiplers))
        {
            Debug.Log("Jump to " + moveDirection);
            _lastJump = moveDirection;
        }
    }

    private void Start()
    {
        ResetState();
    }

    private void FixedUpdate()
    {
        if (GameEngine.Instance.Status == GameStatus.GameOver)
        {
            return;
        }
        transform.position = GetNewPosition();
    }

    private Vector3 GetNewPosition()
    {
        _timeFromLastJump += Time.fixedDeltaTime;
        Vector2 jump = MakeJump();
        Vector3 dest = transform.position + new Vector3(0, 0, ModifySpeed(_basicSpeed * SelectMultipler(SpeedMultiplers)) * Time.fixedDeltaTime);
        dest.x = jump.x;
        dest.y = jump.y;
        return dest;
    }

    private float ModifySpeed(float speed)
    {
        float result = speed;
        foreach (IMoveModifier moveModifier in _customModifiers.ToArray())
        {
            if (moveModifier.EndTime > DateTime.UtcNow)
            {
                result = moveModifier.Modify(result);
            }
            else
            {
                _customModifiers.Remove(moveModifier);
            }
        }
        return result;
    }

    private Vector2 MakeJump()
    {
        if (_lastJump != MoveDirection.None)
        {
            switch (_lastJump)
            {
                case MoveDirection.Left:
                    {
                        TryJumpLeft();
                        break;
                    }
                case MoveDirection.Up:
                    {
                        TryJumpUp();
                        break;
                    }
                case MoveDirection.Right:
                    {
                        TryJumpRight();
                        break;
                    }
                case MoveDirection.Down:
                    {
                        TryJumpDown();
                        break;
                    }
                default:
                    break;
            }
            _lastJump = MoveDirection.None;
        }
        return new Vector2(_currentRail.WorldPositionX, _currentRail.WorldPositionY);
    }

    private void TryJumpUp()
    {
        Rail railDown = Rails.FirstOrDefault(rail => rail.IndexY == _currentRail.IndexY - 1 && rail.IndexX == _currentRail.IndexX);
        if (railDown != null)
        {
            _timeFromLastJump = 0f;
            _currentRail = railDown;
        }
    }

    private void TryJumpDown()
    {
        Rail railUp = Rails.FirstOrDefault(rail => rail.IndexY == _currentRail.IndexY + 1 && rail.IndexX == _currentRail.IndexX);
        if (railUp != null)
        {
            _timeFromLastJump = 0f;
            _currentRail = railUp;
        }
    }

    private void TryJumpRight()
    {
        Rail railRight = Rails.FirstOrDefault(rail => rail.IndexY == _currentRail.IndexY && rail.IndexX == _currentRail.IndexX + 1);
        if (railRight != null)
        {
            _timeFromLastJump = 0f;
            _currentRail = railRight;
        }
    }

    private void TryJumpLeft()
    {
        Rail railLeft = Rails.FirstOrDefault(rail => rail.IndexY == _currentRail.IndexY && rail.IndexX == _currentRail.IndexX - 1);
        if (railLeft != null)
        {
            _timeFromLastJump = 0f;
            _currentRail = railLeft;
        }
    }

    private float SelectMultipler(IEnumerable<MultiplerOverTime> multiplers)
    {
        int totalSec = (int)(DateTime.UtcNow - _startTime).TotalSeconds;
        MultiplerOverTime last = multiplers.Last(x => x.StartTime <= totalSec);
        return last.Multipler;
    }

    private Rail _currentRail;
    private float _timeFromLastJump;
    private DateTime _startTime;
    private Rail[] _rails;
    private Rail[] Rails
    {
        get
        {
            if (null == _rails)
            {
                _rails = GameObject.FindObjectOfType<EnvironmentSettings>().RailsDefinitions;
            }
            return _rails;
        }
    }
}
