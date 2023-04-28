using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Core
{
    /// <summary>
    /// 管理游戏内时间流逝，最优先执行
    /// </summary>
    public class GameTimeManager : MonoBehaviourSingletonBase<GameTimeManager>
    {
        /// <summary>
        /// 从游戏开始到现在经过的总游戏内时间(s)
        /// </summary>
        public float TotalGameTime { get; private set; }
        public float DeltaGameTime { get; private set; }
        /// <summary>
        /// 默认一倍速，即现实时间1s = 游戏世界1s。
        /// </summary>
        private float _gameSpeed = 3600f;      // 测试先用3600倍速
        public bool IsPaused { get; private set; }

        private int _lastGameMinute;
        private int _lastGameHour;
        private int _lastGameSecond;
        //public float DeltaTime { get => 1 / gameSpeed; }
        //public DateTime GameStartTime = new DateTime(2020, 11, 02, 10, 30, 0);
        private readonly DateTime _gameStartTime = new(1453, 5, 29, 0, 0, 0);
        public DateTime GameTime { get => _gameStartTime.AddSeconds(TotalGameTime); }

        public Action updateEveryTickEvent;
        public Action gamePauseEvent;
        public Action gameUnpauseEvent;
        public Action gameSpeedSetEvent;
        public Action updateEverySecondEvent;
        public Action updateEveryMinuteEvent;
        public Action updateEveryHourEvent;

        private void Start()
        {
            // 目标帧率60
            Application.targetFrameRate = 60;
            _lastGameMinute = GameTime.Minute;
            _lastGameHour = GameTime.Hour;
        }
        private void Update()
        {
            if (IsPaused) return;
            DeltaGameTime = _gameSpeed * Time.deltaTime;
            //deltaGameTime = Mathf.Clamp(Time.deltaTime * gameSpeed, 0f, 10f);    // 不许一次Tick太长游戏时间，否则连续变离散要出大事。
            TotalGameTime += DeltaGameTime;

            updateEveryTickEvent?.Invoke();

            if (GameTime.Second != _lastGameSecond)  // 检测秒是否改变
            {
                updateEverySecondEvent?.Invoke();
                _lastGameSecond = GameTime.Second;
            }
            if (GameTime.Minute != _lastGameMinute)  // 检测分钟是否改变
            {
                updateEveryMinuteEvent?.Invoke();
                _lastGameMinute = GameTime.Minute;
            }
            if (GameTime.Hour != _lastGameHour)  // 检测小时是否改变
            {
                updateEveryHourEvent?.Invoke();
                _lastGameHour = GameTime.Hour;
            }
        }

        public void SetGameSpeed(float speed)
        {
            _gameSpeed = Mathf.Clamp(speed, 1f, 600f);
            gameSpeedSetEvent?.Invoke();
        }
        public float GetGameSpeed()
        {
            return _gameSpeed;
        }

        public void PauseGame()
        {
            if (IsPaused) return;
            IsPaused = true;
            gamePauseEvent?.Invoke();
        }
        public void UnpauseGame()
        {
            if (!IsPaused) return;
            IsPaused = false;
            gameUnpauseEvent?.Invoke();
        }
        public void SetPauseState(bool state)
        {
            if (state != IsPaused) FlipGamePauseState();
        }
        public void FlipGamePauseState()
        {
            if (IsPaused) UnpauseGame(); else PauseGame();
        }
        public float GetDayProgress()
        {
            return (float)GameTime.TimeOfDay.Ticks / TimeSpan.TicksPerDay;
        }
    }
}
