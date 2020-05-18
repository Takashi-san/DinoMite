using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	[Header("Tamanho definido pelo valor de Scale.x global")]
	[SerializeField] GameObject _prefab = null;

	[Header("Spawn Rate")]
	[Tooltip("Taxa de spawn por segundo.")]
	[SerializeField] [Min(0.01f)] float _initialSpawnRate = 0;
	[Tooltip("Tempo para aumentar a taxa de spawn em segundos. 0 = não aumenta.")]
	[SerializeField] [Min(0)] float _spawnIncreaseTime = 0;
	[Tooltip("Fator de aumento da taxa de spawn. 1 = não aumenta.")]
	[SerializeField] [Min(1)] float _spawnIncreaseRate = 1;
	[Tooltip("Limite na taxa de spawn por segundo. 0 = sem limite.")]
	[SerializeField] [Min(0)] float _spawnCap = 0;

	[Header("Spawn Number")]
	[Tooltip("Quantidade gerado por spawn. 0 = não spawna.")]
	[SerializeField] [Min(0)] int _initialNum = 0;
	[Tooltip("Tempo para aumentar a quantidade gerada por spawn em segundos. 0 = não aumenta.")]
	[SerializeField] [Min(0)] float _numIncreaseTime = 0;
	[Tooltip("Quantidade aumentada por intervalo. 0 = não aumenta.")]
	[SerializeField] [Min(0)] int _numIncreaseRate = 0;
	[Tooltip("Limite na quantidade. 0 = sem limite.")]
	[SerializeField] [Min(0)] int _numCap = 0;

	float _spawnTimer = 0;
	float _increaseTimer = 0;
	float _numIncreaseTimer = 0;
	float _spawnTime;
	float _maxSpawnTime = 1;
	int _num;
	bool _stop = false;

	void Start() {
		_spawnTime = 1 / _initialSpawnRate;
		if (_spawnCap != 0) {
			_maxSpawnTime = 1 / _spawnCap;
		}
		_num = _initialNum;

		FindObjectOfType<PlayerLife>().playerDied += StopSpawn;
	}

	void Update() {
		if (!_stop) {
			_spawnTimer += Time.deltaTime;
			_increaseTimer += Time.deltaTime;
			_numIncreaseTimer += Time.deltaTime;

			if (_spawnTimer > _spawnTime) {
				Spawn(_num);
				_spawnTimer = 0;
			}

			if ((_increaseTimer > _spawnIncreaseTime) && (_spawnIncreaseTime != 0)) {
				_spawnTime = _spawnTime / _spawnIncreaseRate;
				if (_spawnCap != 0) {
					_spawnTime = _spawnTime < _maxSpawnTime ? _maxSpawnTime : _spawnTime;
				}
				_increaseTimer = 0;
			}

			if ((_numIncreaseTimer > _numIncreaseTime) && (_numIncreaseTime != 0)) {
				_num += _numIncreaseRate;
				if (_numCap != 0) {
					_num = _num > _numCap ? _numCap : _num;
				}
				_numIncreaseTimer = 0;
			}
		}
	}

	void Spawn(int num) {
		float absX = Mathf.Abs(transform.lossyScale.x / 2f);
		float partX = (2 * absX) / (float)num;
		for (int i = 0; i < num; i++) {
			Vector3 random = new Vector3(Random.Range(-absX + partX * i + transform.position.x, -absX + partX * (i + 1) + transform.position.x), transform.position.y, 0);
			Instantiate(_prefab, random, _prefab.transform.rotation);     // adicionar object pooling depois.
		}
	}

	void StopSpawn() {
		_stop = true;
	}
}
