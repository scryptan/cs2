using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PatientSpawner : MonoBehaviour
{
    [SerializeField] private GameObject patientPrefab;

    [SerializeField] private int patientCount;
    [SerializeField] private Transform leftTopBound;

    [SerializeField] private Transform rightBound;
    private List<GameObject> patients;
    private List<Patient> immunities;
    [SerializeField] private float radius = 0.3f;
    void Start()
    {
        patients = new List<GameObject>();
        immunities = new List<Patient>();
        var minX = leftTopBound.position.x;
        var maxY = leftTopBound.position.y;
        var minY = rightBound.position.y;
        var maxX = rightBound.position.x;
        for (int i = 0; i < patientCount; i++)
        {
            var go = Instantiate(patientPrefab,
                    new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY)), Quaternion.identity);
            patients.Add(go);
            go.GetComponent<IActorMover>().SetBound(leftTopBound.position, rightBound.position);
            immunities.Add(go.GetComponent<ImmunityController>().Patient);
            if (Random.Range(0, 100) < 3)
                go.GetComponent<ImmunityController>().Patient.IsInfected = true;
        }


        
        StartCoroutine(Move());
        StartCoroutine(TryToInfect());
        StartCoroutine(TryToRecover());
        StartCoroutine(TryNotToDie());
    }
    
    IEnumerator Move()
    {
        var _movers = patients.Where(x => !x.GetComponent<ImmunityController>().Patient.HasDied).Select(x => x.GetComponent<IActorMover>());
        while (true)
        {
            foreach (var mover in _movers)
            {
                mover?.SetVelocityVector(new Vector2(Random.Range(-1f, 1), Random.Range(-1f, 1)));
            }
            yield return new WaitForSeconds(1);
        }
    }
    
    IEnumerator TryToInfect()
    {
        while (true)
        {
            for (int i = 0; i < patients.Count; i++)
            {
                for (int j = i + 1; j < patients.Count; j++)
                {
                    if (Vector3.Distance(patients[i].transform.position, patients[j].transform.position) < radius && (immunities[i].IsInfected || immunities[j].IsInfected))
                    {
                            immunities[i].Infect();
                            immunities[j].Infect();
                    }

                    yield return null;
                }
            }
            yield return new WaitForSeconds(2);
        }
    }
    
    IEnumerator TryToRecover()
    {
        while (true)
        {
            foreach (var immunity in immunities)
            {
                if (immunity.IsInfected && !immunity.HasDied)
                    immunity.Recovery();
                yield return null;
            }
            yield return new WaitForSeconds(2);
        }
    }

    IEnumerator TryNotToDie()
    {
        while (true)
        {
            foreach (var patient in patients)
            {
                var patientImmunity = patient?.GetComponent<ImmunityController>().Patient;
                if (patientImmunity.IsInfected)              
                    patientImmunity.WillDie();
                if (patientImmunity.HasDied)
                    patient?.GetComponent<IActorMover>().SetVelocityVector(new Vector2(0, 0));
                yield return null;
            }
            yield return new WaitForSeconds(2);
        }
    }
}