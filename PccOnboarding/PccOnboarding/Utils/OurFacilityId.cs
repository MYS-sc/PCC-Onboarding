using Microsoft.EntityFrameworkCore;
using PccOnboarding.Context;

namespace PccOnboarding.Utils;

public class OurFacilityId
{

    public async Task<int?> Get(string orgId, string state, int pccFacId)
    {
        // get the right state Database
        var dbContext = await GetContext.Get(state);

        using (var context = (DbContext)Activator.CreateInstance(dbContext))
        {
            //quering the database for our facility id 
            var response = context.Set<PccFacilitiesTable>().FirstOrDefault(f => f.OrgUuid == orgId && f.PccId == pccFacId);
            return response != null ? response.FacilityId : null;
        }

    }

}
