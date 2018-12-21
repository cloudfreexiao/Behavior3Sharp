/*
 * The Blackboard is the memory structure required by `BehaviorTree` and its
 * nodes. It only have 2 public methods: `set` and `get`. These methods works
 * in 3 different contexts: global, per tree, and per node per tree.
 *
 * Suppose you have two different trees controlling a single object with a
 * single blackboard, then:
 *
 * - In the global context, all nodes will access the stored information.
 * - In per tree context, only nodes sharing the same tree share the stored
 *   information.
 * - In per node per tree context, the information stored in the blackboard
 *   can only be accessed by the same node that wrote the data.
 *
 * The context is selected indirectly by the parameters provided to these
 * methods, for example:
 *
 *     // getting/setting variable in global context
 *     blackboard.set('testKey', 'value');
 *     var value = blackboard.get('testKey');
 *
 *     // getting/setting variable in per tree context
 *     blackboard.set('testKey', 'value', tree.id);
 *     var value = blackboard.get('testKey', tree.id);
 *
 *     // getting/setting variable in per node per tree context
 *     blackboard.set('testKey', 'value', tree.id, node.id);
 *     var value = blackboard.get('testKey', tree.id, node.id);
 *
 * Note: Internally, the blackboard store these memories in different
 * objects, being the global on `_baseMemory`, the per tree on `_treeMemory`
 * and the per node per tree dynamically create inside the per tree memory
 * (it is accessed via `_treeMemory[id].nodeMemory`). Avoid to use these
 * variables manually, use `get` and `set` instead.
 * 
 */


using System.Collections.Generic;


namespace XIL.AI.Behavior3Sharp
{
    public class Memory
    {
        public class MemoryItem
        {
            private object _value;
            public void SetValue(object v)
            {
                _value = v;
            }
            public T GetValue<T>()
            {
                return (T)_value;
            }
        }

        public Dictionary<string, MemoryItem> _items = new Dictionary<string, MemoryItem>();

        public void SetValue(string key, object v)
        {
            MemoryItem item;
            if (_items.ContainsKey(key) == false)
            {
                item = new MemoryItem();
                _items.Add(key, item);
            }
            else
            {
                item = _items[key];
            }
            item.SetValue(v);
        }

        public T GetValue<T>(string key, T defaultValue)
        {
            if (_items.ContainsKey(key) == false)
            {
                return defaultValue;
            }
            return _items[key].GetValue<T>();
        }
    }

    public class TreeData
    {
        public Memory nodeMemory = new Memory();
        public List<BaseNode> openNodes = new List<BaseNode>();
        public int traversalDepth = 0;
        public int traversalCycle = 0;
    }

    public class TreeMemory
    {
        public Memory _memory = new Memory();
        public TreeData _treeData = new TreeData();
        public Dictionary<string, Memory> _nodeMemory  = new Dictionary<string, Memory>();
    }

    public class Blackboard
    {
        public Memory _baseMemory;
        public Dictionary<string, TreeMemory> _treeMemory;

        public Blackboard()
        {
            this. _baseMemory = new Memory();
            this._treeMemory = new Dictionary<string, TreeMemory>();
        }

        public TreeMemory _getTreeMemory(string treeScope)
        {
            TreeMemory item;
            if (this._treeMemory.ContainsKey(treeScope) == false)
            {
                item = new TreeMemory();
                this._treeMemory.Add(treeScope, item);
            }
            else
            {
                item = this._treeMemory[treeScope];
            }
            return item;
        }

        public Memory _getNodeMemory(TreeMemory treeMemory, string nodeScope)
        {
            Memory item;
            Dictionary<string, Memory> memory = treeMemory._nodeMemory;
            if (memory.ContainsKey(nodeScope) == false)
            {
                item = new Memory();
                memory.Add(nodeScope, item);
            }
            else
            {
                item = memory[nodeScope];
            }
            return item;
        }

        public Memory _getMemory(string treeScope, string nodeScope)
        {
            Memory memory = this._baseMemory;
            if(treeScope.Length >0)
            {
                TreeMemory treeMem = this._getTreeMemory(treeScope);
                memory = treeMem._memory;
                if(nodeScope.Length >0)
                {
                    memory = this._getNodeMemory(treeMem, nodeScope);
                }
            }
            return memory;
        }

        public void Set(string key, object value, string treeScope, string nodeScope)
        {
            var memory = this._getMemory(treeScope, nodeScope);
            memory.SetValue(key, value);
        }

        public T Get<T>(string key, string treeScope, string nodeScope, T defaultvalue)
        {
            var memory = this._getMemory(treeScope, nodeScope);
            return memory.GetValue<T>(key, defaultvalue);
        }

        public void SetTree(string key, object value, string treeScope)
        {
            var memory = this._getMemory(treeScope, "");
            memory.SetValue(key, value);
        }

        public TreeData _getTreeData(string treeScope)
        {
            var treeMem = this._getTreeMemory(treeScope);
            return treeMem._treeData;
        }

    }
}
