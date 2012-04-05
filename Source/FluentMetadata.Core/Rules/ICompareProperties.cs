namespace FluentMetadata.Rules
{
    internal interface IValidateAProperty : IClassRule
    {
        string PropertyName { get; }
    }

    internal interface ICompareProperties : IValidateAProperty
    {
        string OtherPropertyName { get; }
    }
}