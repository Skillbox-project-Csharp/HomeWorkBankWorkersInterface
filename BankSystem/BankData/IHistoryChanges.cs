using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkBankWorkersInterface.BankSystem.BankData
{
    internal interface IHistoryChanges
    {
        List<InfoChangeDataClient> InfoChangeDataClients { get; set;}
    }
}
