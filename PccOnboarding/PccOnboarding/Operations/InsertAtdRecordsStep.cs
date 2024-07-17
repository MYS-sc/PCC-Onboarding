using Microsoft.EntityFrameworkCore;
using PccOnboarding.Models.Tables;

namespace PccOnboarding.Operations;

public class InsertAtdRecordsStep
{
    public void Execute(List<PccAdtTable> adtList, DbContext context)
    {

        foreach (var adt in adtList)
        {



            context.Set<PccAdtTable>().Add(adt);
            //context.SaveChanges();

        }

    }
}
