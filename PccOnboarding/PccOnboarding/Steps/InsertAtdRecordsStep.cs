using Microsoft.EntityFrameworkCore;
using PccOnboarding.Models.Tables;

namespace PccOnboarding;

public class InsertAtdRecordsStep
{
    public void Execute(List<PccAdtTable> adtList, Type db)
    {
        using (var context = (DbContext)Activator.CreateInstance(db))
        {
            foreach (var adt in adtList)
            {



                context.Set<PccAdtTable>().Add(adt);
                context.SaveChanges();

            }
        }
    }
}
