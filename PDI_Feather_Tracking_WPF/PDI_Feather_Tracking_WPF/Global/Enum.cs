using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDI_Feather_Tracking_WPF.Global
{
    public enum MonthEnum 
    {
        A = 1, // January
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        I,
        J,
        K,
        L = 12 // December
    }

    public enum ModuleEnum
    {
        user_level = 1,
        user,
        sku_type,
        tare_weight_setting,
        incoming,
        outgoing,
        reporting,
    }

    public enum ReportTypesEnum
    {
        IncomingReport, //
        OutgoingReport,
        OnHandBalanceReport,
        ActualWeightList //
    }
}
