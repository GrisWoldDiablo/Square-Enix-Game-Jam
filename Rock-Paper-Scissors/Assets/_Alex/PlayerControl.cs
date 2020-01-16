using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material.SetColor("_Color", Random.ColorHSV());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerAction(string choice)
    {
        switch (choice)
        {
            case "up-start":
                StopAllCoroutines();
                StartCoroutine(Move(1));
                break;
            case "down-start":
                StopAllCoroutines();
                StartCoroutine(Move(-1));
                break;
            default:
                StopAllCoroutines();
                break;
        }
    }

    IEnumerator Move(int direction)
    {
        while (true)
        {
            transform.position += Vector3.up * direction * Time.deltaTime;
            yield return null;
        }
    }
    //void MoveCube(int id, int direction)
    //{
    //    int index = _players.IndexOf(id);
    //    if (index > 1)
    //    {
    //        index = 0;
    //    }
    //    switch (direction)
    //    {
    //        case 0:
    //        case 2:
    //            StopCoroutine(_players[id]);
    //            break;
    //        case 1:
    //            if (!_players.ContainsKey(id))
    //            {
    //                _players.Add(id, StartCoroutine(TransCube(_cubes[index],1)));
    //            }
    //            else
    //            {
    //                _players[id] = StartCoroutine(TransCube(_cubes[index], 1));
    //            }
    //            break;
    //        case 3:
    //            if (!_players.ContainsKey(id))
    //            {
    //                _players.Add(id, StartCoroutine(TransCube(_cubes[index], -1)));
    //            }
    //            else
    //            {
    //                _players[id] = StartCoroutine(TransCube(_cubes[index], -1));

    //            }
    //            break;
    //        default:
    //            break;
    //    }
    //}
}
