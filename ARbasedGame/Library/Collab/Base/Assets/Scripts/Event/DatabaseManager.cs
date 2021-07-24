using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    static public DatabaseManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    private List<MyEvent> m_events = new List<MyEvent>();


    public void AddEvent(string name, int num)
    {
        m_events.Add(new MyEvent(name, num));
    }



    [System.Serializable]
    public class MyEvent
    {
        public string script_name;
        public int script_num;
        private bool clear;

        public MyEvent(string script, int scriptNum)
        {
            script_name = script;
            script_num = scriptNum;
            clear = false;
        }
    }
}
