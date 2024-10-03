namespace Clean.Architecture.Infrastructure.Data;

public class B2B_Company_Credit
{
  public B2B_Company_Credit(string companyId, List<B2B_Company_Extra_Field> extraFields)
  {
    this.companyId = companyId;
    this.extraFields = extraFields;
  }
  public string? companyId { get; set; }
  public List <B2B_Company_Extra_Field>? extraFields { get; set; }
}

public class B2B_Company_Extra_Field
{
  /*public B2B_Company_Extra_Field(string fieldName, string fieldValue)
  {
    this.fieldName = fieldName;
    this.fieldValue = fieldValue;
  }*/
  public string? fieldName { get; set; }
  public string? fieldValue { get; set; }
}

