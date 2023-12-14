using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlappyProject.Interfaces;
using System;
using UnityEngine.SocialPlatforms.Impl;

namespace FlappyProject.Managers
{
    public class ScoringManager : MonoBehaviour, IManager
    {
        private ScoreData scoreData;
        [SerializeField] private int scorePerSecond;
        [SerializeField] private int scorePenalty;

        private TempUpdateScore _tempUpdateScore;

        private Coroutine _scoring;
        private bool _scoringRunning;

        public void Init()
        {
            _tempUpdateScore = GetComponent<TempUpdateScore>();
            scoreData = new ScoreData();
            _scoringRunning = true;
            _scoring = StartCoroutine(StartScoring());
            UpdatePreviousScoreText();
        }

        public void SetPlayerDiedCallback(Action<LayerMask> callback)
        {
            callback += HandlePlayerDamaged;
        }

        public Action<LayerMask> GetPlayerDamagedHandler(){
            return HandlePlayerDamaged;
        }

        private void HandlePlayerDamaged(LayerMask layer)
        {
            int resultScore = scoreData.CurrentScore - scorePenalty;
            if (resultScore > 0)
            {
                RemoveScore(scorePenalty);
            }
            else
            {
                _scoringRunning = false;
                Stop();
                SaveScore();
                UpdatePreviousScoreText();
                ResetScore();
                Debug.Log("Player Died");
            }

        }

        private void UpdatePreviousScoreText()
        {
            _tempUpdateScore.SetPreviousScoreText($"Previous Score : {scoreData.PreviousScore}");
        }

        private void LoadLastScore(ScoreData scoreData)
        {
            this.scoreData = scoreData;
        }

        public void Stop()
        {
            StopCoroutine(_scoring);
        }

        public void UpdateManager(float deltaTime)
        {
        }

        public int GetCurrentScore()
        {
            return scoreData.CurrentScore;
        }
        public void SetScore(int score)
        {
            scoreData.CurrentScore = score;
        }
        public void AddScore(int scoreToAdd)
        {
            scoreData.CurrentScore += scoreToAdd;
            _tempUpdateScore.SetScoreText($"Score : {scoreData.CurrentScore}");
        }
        public void RemoveScore(int scoreToRemove)
        {
            scoreData.CurrentScore -= scoreToRemove;
        }

        public void ResetScore()
        {
            scoreData.CurrentScore = 0;
        }
        public void SaveScore()
        {
            scoreData.PreviousScore = scoreData.CurrentScore;
        }

        private IEnumerator StartScoring()
        {
            while (_scoringRunning)
            {
                AddScore(scorePerSecond);
                yield return new WaitForSeconds(1);
            }
        }

        void OnGUI()
        {
            GUI.Label(new Rect(10, 10, Screen.width, Screen.height), "Score: " + scoreData.CurrentScore);
        }
    }
}
