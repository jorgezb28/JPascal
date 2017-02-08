namespace JPascalCompiler.Semantic.Types
{
    class FloatType:BaseType
    {
        public override bool IsAssignable(BaseType otherType)
        {
            return otherType is FloatType;
        }

        public override bool IsComparable(BaseType otheType)
        {
            return otheType is FloatType;
        }
    }
}