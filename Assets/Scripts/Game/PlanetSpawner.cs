using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game
{
    public class PlanetSpawner : MonoBehaviour
    {
        [Inject] private GameObject planetPrefab;
        [Inject] DiContainer diContainer;
        [SerializeField] private GameObject starPrefab;
        [SerializeField] private Transform rocket;

        private Coroutine planetCoroutine;
        private Coroutine starCoroutine;

        private void OnEnable()
        {
            GameLogic.OnGameStateChanged += HandleGameState;
        }
        private void OnDisable()
        {
            GameLogic.OnGameStateChanged -= HandleGameState;

            if (planetCoroutine != null) StopCoroutine(planetCoroutine);
            if (starCoroutine != null) StopCoroutine(starCoroutine);
        }

        private void HandleGameState(GameState gameState)
        {
            if (gameState == GameState.PLAYING)
            {
                planetCoroutine = StartCoroutine(SpawnPlanets());
                starCoroutine = StartCoroutine(SpawnStars());
            }
            else
            {
                if (planetCoroutine != null) StopCoroutine(planetCoroutine);
                if (starCoroutine != null) StopCoroutine(starCoroutine);
            }
        }

        IEnumerator SpawnStars()
        {
            while(true)
            {
                yield return new WaitForSeconds(5);
                float randomY = Random.Range(2, 4);
                Instantiate(starPrefab, new Vector2(Random.Range(rocket.transform.position.x - 1,
                    rocket.transform.position.x + 1) + 5, Random.Range(randomY - 5, randomY - 3f)), rocket.transform.rotation);
            }
        }

        private void SetPlanetPositionAndRotation(GameObject planet, Vector2 pos, Quaternion rot)
        {
            planet.transform.SetPositionAndRotation(pos, rot);
        }

        IEnumerator SpawnPlanets()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                float randomY = Random.Range(3, 5);
                GameObject topPlanet = diContainer.InstantiatePrefab(planetPrefab);
                SetPlanetPositionAndRotation(topPlanet, new Vector2(rocket.transform.position.x + 10, randomY), rocket.transform.rotation);
                GameObject bottomPlanet = diContainer.InstantiatePrefab(planetPrefab);

                SetPlanetPositionAndRotation(bottomPlanet, new Vector2(Random.Range(rocket.transform.position.x - 1,
                    rocket.transform.position.x + 1) + 10, Random.Range(randomY - 8, randomY - 7.5f)), rocket.transform.rotation);

                topPlanet.TryGetComponent<PlanetManager>(out PlanetManager TplanetManager);
                if (TplanetManager != null) TplanetManager.rocket = rocket;

                bottomPlanet.TryGetComponent<PlanetManager>(out PlanetManager BplanetManager);
                if (BplanetManager != null) BplanetManager.rocket = rocket;

                yield return new WaitForSeconds(2f);
            }
        }
    }
}