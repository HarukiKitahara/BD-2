using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Core
{
    /// <summary>
    /// ������Ϸ��ʱ�����ţ�������ִ��
    /// </summary>
    public class GameTimeManager : MonoBehaviourSingletonBase<GameTimeManager>
    {
        /// <summary>
        /// ����Ϸ��ʼ�����ھ���������Ϸ��ʱ��(s)
        /// </summary>
        public float TotalGameTime { get; private set; }
        public float DeltaGameTime { get; private set; }
        /// <summary>
        /// Ĭ��һ���٣�����ʵʱ��1s = ��Ϸ����1s��
        /// </summary>
        private float _gameSpeed = 3600f;      // ��������3600����
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
            // Ŀ��֡��60
            Application.targetFrameRate = 60;
            _lastGameMinute = GameTime.Minute;
            _lastGameHour = GameTime.Hour;
        }
        private void Update()
        {
            if (IsPaused) return;
            DeltaGameTime = _gameSpeed * Time.deltaTime;
            //deltaGameTime = Mathf.Clamp(Time.deltaTime * gameSpeed, 0f, 10f);    // ����һ��Tick̫����Ϸʱ�䣬������������ɢҪ�����¡�
            TotalGameTime += DeltaGameTime;

            updateEveryTickEvent?.Invoke();

            if (GameTime.Second != _lastGameSecond)  // ������Ƿ�ı�
            {
                updateEverySecondEvent?.Invoke();
                _lastGameSecond = GameTime.Second;
            }
            if (GameTime.Minute != _lastGameMinute)  // �������Ƿ�ı�
            {
                updateEveryMinuteEvent?.Invoke();
                _lastGameMinute = GameTime.Minute;
            }
            if (GameTime.Hour != _lastGameHour)  // ���Сʱ�Ƿ�ı�
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
