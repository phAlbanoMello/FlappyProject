using FlappyProject.Interfaces;
using System.Collections;
using UnityEngine;

namespace FlappyProject.Managers
{
    public class ScoringManager : MonoBehaviour, IManager
    {
        [SerializeField] private int _scorePerSecond;
        [SerializeField] private int _scorePenalty;

        private ScoreData _scoreData;
        private TempUpdateScore _tempUpdateScore;
        private Coroutine _scoring;
        private bool _scoringRunning;

        public void Init()
        {
            EventBus.Subscribe<PlayerCollidedEvent>(HandlePlayerDamaged);

            _tempUpdateScore = GetComponent<TempUpdateScore>();
            _scoreData = new ScoreData();
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

        private void UpdatePreviousScoreText()
        {
            _tempUpdateScore.SetPreviousScoreText($"Previous Score : {_scoreData.PreviousScore}");
        }

        private void LoadLastScore(ScoreData scoreData)
        {
            this._scoreData = scoreData;
        }

        public void Stop()
        {
            StopCoroutine(_scoring);
            EventBus.Unsubscribe<PlayerCollidedEvent>(HandlePlayerDamaged);
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
        }
        public void AddScore(int scoreToAdd)
        {
            _scoreData.CurrentScore += scoreToAdd;
            UpdateScoreText();
        }

        private void UpdateScoreText()
        {
            _tempUpdateScore.SetScoreText($"Score : {_scoreData.CurrentScore}");
        }

        public void RemoveScore(int scoreToRemove)
        {
            _scoreData.CurrentScore -= scoreToRemove;
            UpdateScoreText();
        }

        public void ResetScore()
        {
            _scoreData.CurrentScore = 0;
            UpdateScoreText();
        }
        public void SaveScore()
        {
            _scoreData.PreviousScore = _scoreData.CurrentScore;
            UpdatePreviousScoreText();
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
