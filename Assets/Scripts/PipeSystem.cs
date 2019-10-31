using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSystem : MonoBehaviour
{
    public GameObject pipesPrefab;
    public Vector2 spawnIntervalRange;
    public Transform yMinPipePosition, yMaxPipePosition;
    [SerializeField] private float pipeMoveSpeed = 3f; 

    private float m_spawnInterval;
    private float m_Timer;

    public Transform pipeHolder;
    public Transform spawnPoint;
    public Transform pullBackPoint;

    private List<Transform> m_spawnedPipes;
    private Queue<Transform> m_pool;

    private ScoreController m_scoreController;

    void Start()
    {
        m_Timer = 0;
        m_spawnInterval = Random.Range(spawnIntervalRange.x, spawnIntervalRange.y);
        m_spawnedPipes = new List<Transform>();
        m_pool = new Queue<Transform>(5);
        m_scoreController = ScoreController.Instance;
        pipeHolder = transform.GetChild(4);
    }

    void Update()
    {
        if(!m_scoreController.Started || m_scoreController.GameOver) return;
        m_Timer += Time.deltaTime;

        if(m_Timer > m_spawnInterval){
            Transform pipe;
            if(m_pool.Count < 10){
                pipe = Instantiate(pipesPrefab, spawnPoint.position, Quaternion.identity, pipeHolder).transform;
                m_spawnedPipes.Add(pipe);
            } else {
                pipe = m_pool.Dequeue();
                pipe.position = spawnPoint.position;
            }
            pipe.transform.position += new Vector3(0, Random.Range(yMinPipePosition.position.y, yMaxPipePosition.position.y), 0);
            m_Timer = 0;
            m_spawnInterval = Random.Range(spawnIntervalRange.x, spawnIntervalRange.y);
        }

        foreach(var pipe in m_spawnedPipes){
            pipe.position += Vector3.left * pipeMoveSpeed * Time.deltaTime;
            if(Mathf.Abs(pipe.position.x - pullBackPoint.position.x) < 0.1f){
                m_pool.Enqueue(pipe);
                pipe.position = new Vector3(0, 999, 0);
            } 
        }

    }

    public List<Transform> GetPipes(){
        return m_spawnedPipes;
    }

}
