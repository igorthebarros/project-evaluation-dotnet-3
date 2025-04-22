namespace Domain.Enums
{
    public enum TaxType
    {
        None = 0,
        VAT, // Value Added Tax Number - Europe’s way of identifying companies for tax and cross-border sales.
        EIN, // Employer Identification Number - USA’s federal tax ID for companies.
        CNPJ // Cadastro Nacional da Pessoa Jurídica - Brazil’s equivalent of a company tax ID.
    }
}
