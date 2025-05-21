using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    public Vector3 center;
    public Vector3 size;
    public Vector3[] spawn_points;
    public Vector3[] final_spawn_points;
    private GameObject player;
    public AudioSource JumpScareAudio;
    public GameObject ghost;
#if UNITY_EDITOR
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(center, size);
        foreach (Vector3 spawn_point in spawn_points) {
            Gizmos.DrawSphere(spawn_point, 1f);
        }
        foreach (Vector3 spawn_point in final_spawn_points) {
            Gizmos.DrawSphere(spawn_point, 1f);
        }
    }
#endif

    List<float> time_till_spawns = new List<float>();
    List<float> time_till_final_spawns = new List<float>();
    public int min_time = 20, max_time = 50;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < spawn_points.Length; i++) {
            time_till_spawns.Add(Random.Range(min_time, max_time));
        }
        for (int i = 0; i < final_spawn_points.Length; i++) {
            time_till_final_spawns.Add(0);
        }
    }

    void Update()
    {
        Rect rect = new Rect(center - size/2, size);
        if (rect.Contains(player.transform.position)) {
            for (int i = 0; i < time_till_spawns.Count; i++) {
                time_till_spawns[i] -= Time.deltaTime;
                if (time_till_spawns[i] <= 0f) {
                    time_till_spawns[i] = Random.Range(min_time, max_time);
                    if ((player.transform.position - spawn_points[i]).magnitude > 20f) {
                        Instantiate(ghost, spawn_points[i], Quaternion.identity);
                    }
                }
            }
        } else {
            for (int i = 0; i < final_spawn_points.Count(); i++) {
                if ((player.transform.position - final_spawn_points[i]).magnitude < 7f) {
                    time_till_final_spawns[i] -= Time.deltaTime;
                    if (time_till_final_spawns[i] <= 0) {
                        Instantiate(ghost, final_spawn_points[i], Quaternion.identity);
                        Instantiate(ghost, final_spawn_points[i], Quaternion.identity);
                        Instantiate(ghost, final_spawn_points[i], Quaternion.identity);
                        Instantiate(ghost, final_spawn_points[i], Quaternion.identity);
                        time_till_final_spawns[i] = 7;
                    }
                }
            }
        }
    }
}
