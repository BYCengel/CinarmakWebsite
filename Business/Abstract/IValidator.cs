using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IValidator<T>
    {
        /*
         Interface for Business Logic. Implemented by all concrete business classes.
         */

        public string ErrorMessage { get; set; }

        bool Validation(T entity);
    }
}
