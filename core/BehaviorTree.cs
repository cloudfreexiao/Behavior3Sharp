/**
 * The BehaviorTree class, as the name implies, represents the Behavior Tree
 * structure.
 *
 * There are two ways to construct a Behavior Tree: by manually setting the
 * root node, or by loading it from a data structure (which can be loaded
 * from a JSON). Both methods are shown in the examples below and better
 * explained in the user guide.
 *
 * The tick method must be called periodically, in order to send the tick
 * signal to all nodes in the tree, starting from the root. The method
 * `BehaviorTree.tick` receives a target object and a blackboard as
 * parameters. The target object can be anything: a game agent, a system, a
 * DOM object, etc. This target is not used by any piece of Behavior3JS,
 * i.e., the target object will only be used by custom nodes.
 *
 * The blackboard is obligatory and must be an instance of `Blackboard`. This
 * requirement is necessary due to the fact that neither `BehaviorTree` or
 * any node will store the execution variables in its own object (e.g., the
 * BT does not store the target, information about opened nodes or number of
 * times the tree was called). But because of this, you only need a single
 * tree instance to control multiple (maybe hundreds) objects.
 *
 * Manual construction of a Behavior Tree
 * --------------------------------------
 *
 *     var tree = new b3.BehaviorTree();
 *
 *     tree.root = new b3.Sequence({children:[
 *       new b3.Priority({children:[
 *         new MyCustomNode(),
 *         new MyCustomNode()
 *       ]}),
 *       ...
 *     ]});
 *
 *
 * Loading a Behavior Tree from data structure
 * -------------------------------------------
 *
 *     var tree = new b3.BehaviorTree();
 *
 *     tree.load({
 *       'title'       : 'Behavior Tree title'
 *       'description' : 'My description'
 *       'root'        : 'node-id-1'
 *       'nodes'       : {
 *         'node-id-1' : {
 *           'name'        : 'Priority', // this is the node type
 *           'title'       : 'Root Node',
 *           'description' : 'Description',
 *           'children'    : ['node-id-2', 'node-id-3'],
 *         },
 *         ...
 *       }
 *     })
 *
 *
 * @module b3
 * @class BehaviorTree
 **/



using System;
using System.Collections.Generic;


namespace XIL.AI.Behavior3Sharp
{
    public class BehaviorTree
    {
        public string id;
        private string title;
        private string description;
        private Dictionary<string, object> properties;
        private BaseNode root;
        private object debug;
        private Behavior3TreeCfg dumpinfo;

        public void Initialize()
        {
            this.id = B3Functions.CreateUUID();
            this.title = "The behavior tree";
            this.description = "Default description";
            this.properties = new Dictionary<string, object>();
            this.root = null;
            this.debug = null;
        }

        public void SetDebug(object debug)
        {
            this.debug = debug;
        }

        /**
         * This method dump the current BT into a data structure.
         *
         * Note: This method does not record the current node parameters. Thus,
         * it may not be compatible with load for now.
         *
         * @method dump
         * @return {Object} A data object representing this tree.
        **/
        Behavior3TreeCfg dump()
        {
            return this.dumpinfo;
        }

        /**
         * This method dump the current BT into a data structure.
         *
         * Note: This method does not record the current node parameters. Thus,
         * it may not be compatible with load for now.
         *
         * @method inspect
         * @return {Object} A data object representing this tree.
         **/
        public void inspect()
        {

        }

        /**
         * This method loads a Behavior Tree from a data structure, populating this
         * object with the provided data. Notice that, the data structure must
         * follow the format specified by Behavior3JS. Consult the guide to know
         * more about this format.
         *
         * You probably want to use custom nodes in your BTs, thus, you need to
         * provide the `names` object, in which this method can find the nodes by
         * `names[NODE_NAME]`. This variable can be a namespace or a dictionary,
         * as long as this method can find the node by its name, for example:
         *
         *     //json
         *     ...
         *     'node1': {
         *       'name': MyCustomNode,
         *       'title': ...
         *     }
         *     ...
         *
         *     //code
         *     var bt = new b3.BehaviorTree();
         *     bt.load(data, {'MyCustomNode':MyCustomNode})
         *
         *
         * @method load
         * @param {Object} data The data structure representing a Behavior Tree.
         * @param {Object} [names] A namespace or dict containing custom nodes.
         **/

        public void Load(Behavior3TreeCfg data)
        {
            this.title = data.title;
            this.description = data.description;
            this.properties = data.properties;
            this.dumpinfo = data;

            Dictionary<string, BaseNode> nodes = new Dictionary<string, BaseNode>();
            Behavior3NodeCfg spec;
            BaseNode node;
            foreach (KeyValuePair<string, Behavior3NodeCfg> kv in data.nodes)
            {
                spec = kv.Value;
                node = Behavior3Factory.singleton.CreateBehavior3Instance(spec.name);
                if(node == null)
                {
                    throw new Exception("BehaviorTree.load: Invalid node name:" + spec.name);
                }
                node.Initialize(spec);
                nodes[spec.id] = node;
            }

            foreach (KeyValuePair<string, Behavior3NodeCfg> kv in data.nodes)
            {
                spec = kv.Value;
                node = nodes[spec.id];
                var category = node.GetCategory();
                if (category == Constants.COMPOSITE && spec.children.Count >0)
                {
                    for(int i=0; i< spec.children.Count; i++)
                    {
                        Composite comp = (Composite)node;
                        comp.AddChild(nodes[spec.children[i]]);
                    }
                } 
                else if(category == Constants.DECORATOR && spec.child.Length >0)
                {
                    Decorator dec = (Decorator)node;
                    dec.SetChild(nodes[spec.child]);
                }
            }

            this.root = nodes[data.root];
        }

        /**
        * Propagates the tick signal through the tree, starting from the root.
        *
        * This method receives a target object of any type (Object, Array,
        * DOMElement, whatever) and a `Blackboard` instance. The target object has
        * no use at all for all Behavior3JS components, but surely is important
        * for custom nodes. The blackboard instance is used by the tree and nodes
        * to store execution variables (e.g., last node running) and is obligatory
        * to be a `Blackboard` instance (or an object with the same interface).
        *
        * Internally, this method creates a Tick object, which will store the
        * target and the blackboard objects.
        *
        * Note: BehaviorTree stores a list of open nodes from last tick, if these
        * nodes weren't called after the current tick, this method will close them
        * automatically.
        *
        * @method tick
        * @param {Object} target A target object.
        * @param {Blackboard} blackboard An instance of blackboard object.
        * @return {Constant} The tick signal state.
        **/

        public B3Status Tick(object target, Blackboard blackboard)
        {
            if (blackboard == null)
            {
                throw new Exception("The blackboard parameter is obligatory and must be an instance of b3.Blackboard");
            }

            var tick = new Tick();
            tick.Initialize(this, blackboard, debug, target);

            /* TICK NODE*/
            var status = this.root._execute(tick);

            /* CLOSE NODES FROM LAST TICK, IF NEEDED */
            var lastOpenNodes = blackboard._getTreeData(this.id).openNodes;
            var currOpenNodes = tick._openNodes;

            int start = 0;
            int i = 0;
            for(i=0; i< Math.Min(lastOpenNodes.Count, currOpenNodes.Count); i++)
            {
                start = i + 1;
                if (lastOpenNodes[i] != currOpenNodes[i])
                {
                    break;
                }
            }

            // close the nodes
            for (i= lastOpenNodes.Count - 1; i >= start; i--)
            {
                lastOpenNodes[i]._close(tick);
            }

            /* POPULATE BLACKBOARD */
            blackboard.Set("openNodes", currOpenNodes, this.id, "");
            blackboard.Set("nodeCount", tick._nodeCount, this.id, "");
            return status;
        }


    }
}
