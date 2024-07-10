using Microsoft.EntityFrameworkCore;
using PccOnboarding.Context;
using PccOnboarding.Models.Tables;


namespace PccWebhook.Utils;

public class OurFacilityId
{
    public async Task<int?> Get(string orgId, string state, int pccFacId, DbContext context)
    {
        // get the right state Database



        //quering the database for our facility id 
        var response = context.Set<PccFacilitiesTable>().FirstOrDefault(f => f.OrgUuid == orgId && f.PccId == pccFacId);
        return response != null ? response.FacilityId : null;


    }

}
