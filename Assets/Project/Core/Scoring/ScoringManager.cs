using FlappyProject.Interfaces;
using System;
using System.Collections;
using UnityEngine;

namespace FlappyProject.Managers
{
    public class ScoringManager : MonoBehaviour, IManager
    {
        [SerializeField] private int _scorePerSecond;
        [SerializeField] private int _scorePenalty;
        [SerializeField] private bool _autoStart;
        public bool ShouldInitializeAtStart { get { return _autoStart; } }
        public bool HasInitiated { get; private set; }

        private ScoreData _scoreData;

        private Coroutine _scoring;
        private bool _scoringRunning;

        public void Init()
        {
            EventBus.Subscribe<GameStartedEvent>(HandleGameStarted);
            EventBus.Subscribe<PlayerCollidedEvent>(HandlePlayerDamaged);
            _scoreData = new ScoreData();
        }

        private void HandleGameStarted(GameStartedEvent @event)
        {
            _scoringRunning = true;
            _scoring = StartCoroutine(StartScoring());
        }

        private void HandlePlayerDamaged(PlayerCollidedEvent playerCollidedEvent)
        {
            int resultScore = _scoreData.CurrentScore - _scorePenalty;
            if (resultScore > 0)
            {
                RemoveScore(_scorePenalty);
            }
            else if (_scoringRunning)
            {
                _scoringRunning = false;
                Stop();
                SaveScore();
                ResetScore();
                EventBus.Publish(new PlayerDiedEvent());
                Debug.Log("Player Died");
            }
        }
        private void LoadLastScore(ScoreData scoreData)
        {
            this._scoreData = scoreData;
        }

        public void Stop()
        {
            StopCoroutine(_scoring);
            EventBus.Unsubscribe<PlayerCollidedEvent>(HandlePlayerDamaged);
            EventBus.Unsubscribe<GameStartedEvent>(HandleGameStarted);
        }

        public void UpdateManager(float deltaTime)
        {
        }

        public int GetCurrentScore()
        {
            return _scoreData.CurrentScore;
        }
        public void SetScore(int score)
        {
            _scoreData.CurrentScore = score;
            UpdateScoreText();
        }
        public void AddScore(int scoreToAdd)
        {
            _scoreData.CurrentScore += scoreToAdd;
            UpdateScoreText();
        }
        public void RemoveScore(int scoreToRemove)
        {
            _scoreData.CurrentScore -= scoreToRemove;
            UpdateScoreText();
        }

        private void UpdateScoreText()
        {
            EventBus.Publish(new ScoreChangedEvent(_scoreData.CurrentScore));
        }

        public void ResetScore()
        {
            _scoreData.CurrentScore = 0;
            UpdateScoreText();
        }
        public void SaveScore()
        {
            _scoreData.PreviousScore = _scoreData.CurrentScore;
        }

        private IEnumerator StartScoring()
        {
            while (_scoringRunning)
            {
                AddScore(_scorePerSecond);
                yield return new WaitForSeconds(1);
            }
        }
    }
}
