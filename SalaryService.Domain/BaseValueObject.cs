using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryService.Domain
{
    public abstract class BaseValueObject
    {
        protected abstract IEnumerable<object?> GetEqualityComponents();

        protected static bool EqualOperator(BaseValueObject? left, BaseValueObject? right)
        {
            if (ReferenceEquals(left, objB: null) ^ ReferenceEquals(right, objB: null)) return false;

            return ReferenceEquals(left, objB: null) || left.Equals(right);
        }

        protected static bool NotEqualOperator(BaseValueObject left, BaseValueObject right)
        {
            return !(EqualOperator(left, right));
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType()) return false;

            var other = (BaseValueObject)obj;

            return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null
                    ? x.GetHashCode()
                    : 0)
                .Aggregate((x, y) => x ^ y);
        }
    }
}
