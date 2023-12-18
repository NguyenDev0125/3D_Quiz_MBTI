using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    void Start()
    {
        string js = "{\"statusCode\":200,\"isSuccess\":true,\"errorMessage\":null,\"result\":{\"id\":11,\"name\":\"The Executive\",\"code\":\"ESTJ\",\"description\":\"Excellent administrators, unsurpassed at managing things – or people.\",\"mbtI_Departments\":null}}\r\n\r\n";
        RS rs = JsonConvert.DeserializeObject<RP>(js).result;
        Debug.Log(rs.id);

    }

}
