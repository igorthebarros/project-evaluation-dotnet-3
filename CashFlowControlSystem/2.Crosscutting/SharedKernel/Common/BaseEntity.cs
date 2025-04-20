using SharedKernel.Validation;

namespace SharedKernel.Common
{
    public class BaseEntity : IComparable<BaseEntity>
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Task<IEnumerable<ValidationErrorDetail>> ValidateAsync()
        {
            return Validator.ValidateAsync(this);
        }

        public int CompareTo(BaseEntity? other)
        {
            if (other == null)
                return 1;

            return other!.Id.CompareTo(Id);
        }
    }
}
