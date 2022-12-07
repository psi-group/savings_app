using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications
{
    public class ProductFIlterSpecification : BaseSpecification<Product>
    {
        public ProductFIlterSpecification(List<Category> categories, string? search, string? order) : base()
        {

            if (!string.IsNullOrEmpty(order))
            {
                switch (order)
                {
                    case null:
                        break;
                    case "by_id":
                        ApplyOrderBy(product => product.Id);
                        break;
                    case "by_name":
                        ApplyOrderBy(product => product.Name);
                        break;
                    case "by_price":
                        ApplyOrderBy(product => product.Price);
                        break;
                    default:
                        throw new InvalidRequestArgumentsException();
                }
            }

            ApplyCriteria(product => (String.IsNullOrEmpty(search) ||
                product.Name.Contains(search) ||
                (!String.IsNullOrEmpty(product.Description) && product.Description.Contains(search))) &&
                (categories.Count() == 0 || categories.Contains(product.Category)));
        }
    }
}
