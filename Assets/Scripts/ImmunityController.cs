using System;
using UnityEngine;

public class ImmunityController : MonoBehaviour
{
    private IVirus virus;
    public NormalPatient Patient;

    public ImmunityController()
    {
        virus = new CoronaVirus();
        Patient = new NormalPatient(virus, Infected, Recovered, Dead);
    }

    private void Infected()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void Recovered()
    {
        GetComponent<SpriteRenderer>().color = Color.blue;
    }

    private void Dead()
    {
        GetComponent<SpriteRenderer>().color = Color.black;
    }
}