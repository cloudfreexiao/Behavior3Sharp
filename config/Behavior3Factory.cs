using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace XIL.AI.Behavior3Sharp
{
    public class Behavior3Factory
    {
        public static Behavior3Factory singleton = new Behavior3Factory();

        private  Dictionary<string, System.Type> nodes = new Dictionary<string, Type>();

        public Behavior3Factory()
        {
            Initialize();
        }

        private void Initialize()
        {
            //actions
            this.nodes.Add("Error", typeof(Error));
            this.nodes.Add("Failer", typeof(Failer));
            this.nodes.Add("Runner", typeof(Runner));
            this.nodes.Add("Succeeder", typeof(Succeeder));
            this.nodes.Add("Wait", typeof(Wait));
            this.nodes.Add("Log", typeof(Log));

            //composites
            this.nodes.Add("MemPriority", typeof(MemPriority));
            this.nodes.Add("MemSequence", typeof(MemSequence));
            this.nodes.Add("Priority", typeof(Priority));
            this.nodes.Add("Sequence", typeof(Sequence));

            //decorators
            this.nodes.Add("Inverter", typeof(Inverter));
            this.nodes.Add("Limiter", typeof(Limiter));
            this.nodes.Add("MaxTime", typeof(MaxTime));
            this.nodes.Add("Repeater", typeof(Repeater));
            this.nodes.Add("RepeatUntilFailure", typeof(RepeatUntilFailure));
            this.nodes.Add("RepeatUntilSuccess", typeof(RepeatUntilSuccess));
        }

        public  BehaviorTree BuildBehavior3TreeFromConfig(string path)
        {
            Behavior3TreeCfg cfg = LoadBehavior3TreeCfg(path);
            var tree = new BehaviorTree();
            tree.Initialize();
            tree.Load(cfg);
            return tree;
        }

        public  Behavior3TreeCfg LoadBehavior3TreeCfg(string path)
        {
            var res = new Behavior3TreeCfg();
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                res = JsonConvert.DeserializeObject<Behavior3TreeCfg>(json);
            }
            return res;
        }

        public  BaseNode CreateBehavior3Instance(string classname)
        {
            if (this.nodes.ContainsKey(classname))
            {
                System.Type t = this.nodes[classname];
                BaseNode nodes = Activator.CreateInstance(t) as BaseNode;
                return nodes;
            }
            return null;
        }

    }
}
