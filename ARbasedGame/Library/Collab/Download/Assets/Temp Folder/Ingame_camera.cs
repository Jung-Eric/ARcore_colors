using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingame_camera : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update

    //유저를 추적한다.
    private Vector3 distance;

    void Start()
    {
        distance = transform.position - player.transform.position;
    }

    // Update is called once per frame
    /*
    void Update()
    {
        transform.position = player.transform.position + distanse;
    }
    */

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position+ distance, 1.0f * Time.deltaTime);
        //transform.position = Vector3.Lerp(transform.position, player.transform.position, 2f * Time.deltaTime)+ new Vector3(3, 5, -30);
        //transform.position = player.transform.position + distanse;
        //transform.position = Vector3.Lerp(player.transform.position, transform.position, 2f*Time.deltaTime);
    }

}
