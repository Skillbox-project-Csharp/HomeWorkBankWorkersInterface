﻿using HomeWorkBankWorkersInterface.BankSystem.BankData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkBankWorkersInterface.BankSystem.BankWorkers
{
    interface ISetDataClient
    {
        bool SetName(BankClient client, string name);
        bool SetSurName(BankClient client, string surName);
        bool SetPatronymic(BankClient client, string patronymic);
        bool SetPhoneNumber(BankClient client, string phoneNumber);
        bool SetPassportSeriesNumber(BankClient client, string passportSeriesNumber);
    }
}
