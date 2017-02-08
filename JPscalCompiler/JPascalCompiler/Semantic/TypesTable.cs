using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPascalCompiler.Semantic.Types;
using JPascalCompiler.Tree;

namespace JPascalCompiler.Semantic
{
    public class TypesTable
    {
        private Dictionary<string, BaseType> _typeTable;
        private static TypesTable _instance;

        private TypesTable()
        {
            _typeTable = new Dictionary<string, BaseType>();
            _typeTable.Add("integer", new IntType());
            _typeTable.Add("string", new StringType());
            _typeTable.Add("char", new CharType());
            _typeTable.Add("float", new FloatType());
            _typeTable.Add("boolean", new BooleanType());
            _typeTable.Add("record", new RecordType());
            _typeTable.Add("arrayexpression", new ArrayExpressionType());
            _typeTable.Add("prcedure", new ProcedureType());
            _typeTable.Add("enumeration", new EnumeratorType());
            _typeTable.Add("enumerationConstant", new EnumerationConstant());
            _typeTable.Add("arraytype", new ArrayType());

        }

         public static TypesTable Instance
         {
             get { return _instance ?? (_instance = new TypesTable()); }
         }


        public void RegisterType(string name, BaseType baseType)
        {
            if (_typeTable.ContainsKey(name))
            {
                throw new SemanticException(String.Format("Type :{0} exists.", name));
            }

            _typeTable.Add(name,baseType);
        }

        public BaseType GetType(string name)
        {
            if (_typeTable.ContainsKey(name))
            {
                return _typeTable[name];
            }

            throw new SemanticException(String.Format(@"Type :{0} doesn't exists.", name));
        }

        public string GetStringType(BaseType type)
        {
            if (_typeTable.ContainsValue(type))
            {
                return _typeTable.FirstOrDefault(d => d.Value == type).Key;
            }

            throw new SemanticException(String.Format(@"StringValue of tyep :{0} doesn't exists.", type));
        }

        public bool Contains(string name)
        {
            return _typeTable.ContainsKey(name);
        }

        public BaseType GetType(ExpressionNode expresiontype)
        {
            var id = string.Empty;

            if (expresiontype is  AccesorExpresion)
            {
                var acc = (AccesorExpresion)expresiontype;
                id = acc.AccessorId.Label.ToLower();
                BaseType type;
                if (_typeTable.TryGetValue(id, out type))
                {
                    return type;
                }
                
            }
            if (expresiontype is ArrayExpression)
            {
                id = "arrayexpression";
                BaseType type;
                if (_typeTable.TryGetValue(id, out type))
                {
                    return type;
                }
            }
            if (expresiontype is BooleanNode)
            {
                id = "bool";
                BaseType type;
                if (_typeTable.TryGetValue(id, out type))
                {
                    return type;
                }
            }
            if (expresiontype is CharNode)
            {
                id ="char";
                BaseType type;
                if (_typeTable.TryGetValue(id, out type))
                {
                    return type;
                }
            }
            if (expresiontype is FloatNode )
            {
                id = "float";
                BaseType type;
                if (_typeTable.TryGetValue(id, out type))
                {
                    return type;
                }
            }
            if (expresiontype is IdNode)
            {
                var arr = (IdNode)expresiontype;
                id = arr.Label.ToLower();
                BaseType type;
                if (_typeTable.TryGetValue(id, out type))
                {
                    return type;
                }
            }
            if (expresiontype is NumberNode)
            {
                id = "integer";
                BaseType type;
                if (_typeTable.TryGetValue(id, out type))
                {
                    return type;
                }
            }
            if (expresiontype is ParameterExpressionByReference)
            {
                var arr = (ParameterExpressionByReference)expresiontype;
                id = GetType(arr.ParameterType).ToString().ToLower();
                BaseType type;
                if (_typeTable.TryGetValue(id, out type))
                {
                    return type;
                }
            }
            if (expresiontype is StringNode)
            {
                id = "string";
                BaseType type;
                if (_typeTable.TryGetValue(id, out type))
                {
                    return type;
                }
            }
            throw new SemanticException(String.Format(@"Type :{0} doesn't exists.", id));

        }
    }
   

    internal class SemanticException : Exception
    {
        public SemanticException(string message): base (message)
        {
            

        }
    }
    
}
