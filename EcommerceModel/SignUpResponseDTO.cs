using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceModel
{
    public class SignUpResponseDTO
    {
        public bool IsRegisterationSuccessful {  get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
