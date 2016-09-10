using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace web_TCC.Models
{
    public class RegisterConcatenaLogin
    {

       

        public LoginViewModel Email { get; set; }
        public LoginViewModel  Password { get; set; }
        public LoginViewModel RememberMe { get; set; }
         

        public RegisterViewModel Nome { get; set; }
        public RegisterViewModel ConfirmPassword { get; set; }
    }
}