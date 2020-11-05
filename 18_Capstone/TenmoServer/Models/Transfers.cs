﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Data
{
    public class Transfers
    {
        public int transfer_id { get; set; }

        public int transfer_type_id { get; set; }

        public int transfer_status_id { get; set; }

        public int MyProperty { get; set; }

        public int Amount { get; set; }

        public int? account_from { get; set; }

        public int? account_to { get; set; }

        //Add account_from/account_to?


    }
}