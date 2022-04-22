using HomeWorkBankWorkersInterface.BankSystem.BankData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkBankWorkersInterface.BankSystem.BankWorkers
{
    interface IGetDataClient
    {
        string GetName(BankClient client);
        string GetSurName(BankClient client);
        string GetPatronymic(BankClient client);
        string GetPhoneNumber(BankClient client);
        string GetPassportSeriesNumber(BankClient client);
    }
}
