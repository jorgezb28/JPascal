using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPascalCompiler.Semantic.Types;

namespace JPascalCompiler.Semantic
{
    public class SymbolTable
    {
        private Dictionary<string, BaseType> _symboltable;
        private static SymbolTable _instance;


        private SymbolTable()
        {
            _symboltable = new Dictionary<string, BaseType>();

        }

        public static SymbolTable Instance
        {
            get { return _instance ?? (_instance = new SymbolTable()); }
        }


        public void DeclareVariable(string name, string typeName)
        {
            if (_symboltable.ContainsKey(name))
            {
                throw new SemanticException(String.Format("Variable  :{0} exists.", name));
            }

            if(TypesTable.Instance.Contains(name))
                throw new SemanticException(String.Format("  :{0} iz a taippp.", name));

            _symboltable.Add(name, TypesTable.Instance.GetType(typeName));
        }

        public BaseType GetVariable(string name)
        {
            if (_symboltable.ContainsKey(name))
            {
                return _symboltable[name];
            }

            throw new SemanticException(String.Format("Variable :{0} doesn't exists.", name));
        }

        public bool Contains(string name)
        {
            return _symboltable.ContainsKey(name);
        }


        public void DeclareVariable(string value, string typeName, List<int> dimensions)
        {
            if (dimensions.Count == 0)
            {
                DeclareVariable(value, typeName);                
            }
            else
            {
                //var type = TypesTable.Instance.GetType(typeName);
                //dimensions.Reverse(0, dimensions.Count);
                //foreach (var dimension in dimensions)
                //{

                //    type = new ArrayType(dimension, type);

                //}
                //if (_symboltable.ContainsKey(value))
                //{
                //    throw new SemanticException($"Variable  :{value} exists.");
                //}

                //if (TypesTable.Instance.Contains(value))
                //    throw new SemanticException($"  :{value} iz a taippp.");

                //_symboltable.Add(value, type);
            }
        }


    }
}
