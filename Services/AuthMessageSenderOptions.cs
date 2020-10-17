using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPW215_QuarterProject.Services
{
    public class AuthMessageSenderOptions
    {
        /// <summary>
        /// The SendGrid Account
        /// </summary>
        public string SendGridUser { get; set; }

        /// <summary>
        /// The SendGrid API Key for the project
        /// </summary>
        public string SendGridKey { get; set; }
    }
}
