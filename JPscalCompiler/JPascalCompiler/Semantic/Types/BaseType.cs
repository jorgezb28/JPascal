namespace JPascalCompiler.Semantic.Types
{
    public abstract class BaseType
    {
       public abstract bool IsAssignable(BaseType otherType);
       public abstract bool IsComparable(BaseType otherType);
        
    }
}
