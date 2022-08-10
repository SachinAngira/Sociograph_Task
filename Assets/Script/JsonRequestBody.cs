using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]public class Data
{
    public List<Slideshow> slideshow ;
}

[Serializable]public class PlaceholderAliases
{
}

[Serializable]public class ResponseChannels
{
    public string voice ;
    public string frames ;
    public string shapes ;
}

[Serializable]public class JsonResponseBody
{
    public bool hide_in_customer_history ;
    public object registered_entities ;
    public string whiteboard_template ;
    public string customer_state ;
    public PlaceholderAliases placeholder_aliases ;
    public bool show_feedback ;
    public ToStateFunction to_state_function ;
    public string placeholder ;
    public bool show_in_history ;
    public Data data ;
    public bool overwrite_whiteboard ;
    public string start_timestamp ;
    public string console ;
    public string name ;
    public string title ;
    public ResponseChannels response_channels ;
    public StateOptions state_options ;
    public string response_id ;
    public string whiteboard_title ;
    public string timestamp ;
    public bool maintain_whiteboard ;
    public int wait ;
    public string type ;
    public object options ;
    public string engagement_id ;
}

[Serializable]public class Slideshow
{
    public string image ;
    public string caption ;
}

[Serializable]public class StateOptions
{
    public string cs_top_three ;
    public string cs_must_have ;
    public string cs_enquiry ;
    public string cs_mt1 ;
    public string cs_mt2 ;
    public string cs_mt3 ;
}

[Serializable]public class ToStateFunction
{
    public string function ;
}


