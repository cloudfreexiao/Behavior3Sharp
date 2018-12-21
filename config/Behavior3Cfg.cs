using System;
using System.Collections.Generic;


namespace XIL.AI.Behavior3Sharp
{
    public class Behavior3NodeCfg
    {
        public string id;
        public string name;
        public string title;
        public string description;
        public List<string> children = new List<string>();
        public string child = "";
        public Dictionary<string, string> parameters = new Dictionary<string, string>();
        public Dictionary<string, string> properties = new Dictionary<string, string>();

        public int GetInt32(string key, int defaultValue)
        {
            if (this.properties.ContainsKey(key) == false)
            {
                return defaultValue;
            }

            int value = int.Parse((string)this.properties[key]);
            return value;
        }

        public string GetString(string key, string defaultValue)
        {
            if (this.properties.ContainsKey(key) == false)
            {
                return defaultValue;
            }

            return this.properties[key];
        }


    }

    public class Behavior3TreeCfg
    {
        public string root;
        public string title;
        public string description;
        public Dictionary<string, object> properties = new Dictionary<string, object>();
        public Dictionary<string, Behavior3NodeCfg> nodes = new Dictionary<string, Behavior3NodeCfg>();

        //public T GetValue<T>(string key, T defaultValue)
        //{
        //    if (this.properties.ContainsKey(key) == false)
        //    {
        //        return defaultValue;
        //    }
        //    return (T)this.properties[key];
        //}
    }

}
