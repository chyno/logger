using System;
using System.Collections.Generic;

namespace C3logging.Core
{
    public class C3loglogDetail
    {
        //public FlogDetail()
        //{
        //    Timestamp = DateTime.Now;
        //}
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
      
        // WHERE
        public string Product { get; set; }
        public string Layer { get; set; }
        public string Location { get; set; }
        public string Hostname { get; set; }

        // WHO
        public string UserName { get; set; }
        public string ServiceCenter { get; set; }
        
        // EVERYTHING ELSE
        public long? ElapsedMilliseconds { get; set; }  // only for performance entries
        public Exception Exception { get; set; }  // the exception for error logging
        public CustomException CustomException { get; set; }
        public string ReceiptNumber { get; set; }
        public string CorrelationId { get; set; } // exception shielding from server to client
        public Dictionary<string, object> AdditionalInfo { get; set; }  // everything else

    }
}
