using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.Entity
{
    public enum ReturnStatus
    {
        Pending = 1,
        Colsed = 2,
        Sent = 3
    }

    public enum taxratesflg
    {
        one_cat = 0,
        group_cat
    }

    public enum regstatuscode
    {
        new_reg = 1,
        re_reg,
        re_enter,
        trans, expired,
        exp_npret,
        exp_credit
    }

    public enum fillingcode
    {
        sales = 0,
        table,
        sale_table
    }

    public enum returnCode
    {
        مبيعات=1,
        صفري=2
    }
}
