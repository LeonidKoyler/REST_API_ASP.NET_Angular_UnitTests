using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculation.Contract.Response
{
    public class ValidationFailureResponse
    {
        public required IEnumerable<ValidationResponse> ErrorsList { get; init; }
    }

    public class ValidationResponse
    {
        public required string PropertyName { get; init; }

        public required string Message { get; init; }
    }
}
