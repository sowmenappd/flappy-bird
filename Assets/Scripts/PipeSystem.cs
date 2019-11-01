using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSystem : MonoBehaviour
{
    public GameObject pipesPrefab;
    public Vector2 spawnIntervalRange;
    public Transform yMinPipePosition, yMaxPipePosition;
    public Vector2 pipeGapDeltaRange;
    [SerializeField] private float pipeMoveSpeed = 3f; 

    private float m_spawnInterval;
    private float m_Timer;

    public Transform pipeHolder;
    public Transform spawnPoint;
    public Transform pullBackPoint;

    private float m_pipeGapBaseMagnitude;

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
        m_pipeGapBaseMagnitude = pipesPrefab.transform.GetChild(0).localPosition.y - pipesPrefab.transform.GetChild(1).localPosition.y;
    }

    void FixedUpdate()
    {
        if(!m_scoreController.Started || m_scoreController.GameOver) return;
        m_Timer += Time.deltaTime;

        if(m_Timer > m_spawnInterval){
            Transform pipe;
            if(m_spawnedPipes.Count < 10){
                pipe = Instantiate(pipesPrefab, spawnPoint.position, Quaternion.identity, pipeHolder).transform;
                m_spawnedPipes.Add(pipe);
            } else {
                pipe = m_pool.Dequeue();
                pipe.position = spawnPoint.position;
            }
            pipe.transform.position += new Vector3(0, Random.Range(yMinPipePosition.position.y, yMaxPipePosition.position.y), 0);
            SetPipeOpeningGap(pipe);
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

    private void SetPipeOpeningGap(Transform pipe){
        Transform upper = pipe.GetChild(0);
        Transform lower = pipe.GetChild(1);
        BoxCollider2D pointCollider = pipe.GetChild(2).GetComponent<BoxCollider2D>();

        float baseGap = upper.localPosition.y - lower.localPosition.y;
        float halfDelta = Random.Range(pipeGapDeltaRange.x, pipeGapDeltaRange.y);
        upper.localPosition += (Vector3) Vector2.up * halfDelta;
        lower.localPosition -= (Vector3) Vector2.up * halfDelta;
        pointCollider.size = new Vector2(pointCollider.size.x, pointCollider.size.y + 2 * halfDelta);
    }

}
