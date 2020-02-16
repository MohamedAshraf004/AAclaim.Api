
using Acclaim.Api.Contracts.V1.Requests;
using Acclaim.Api.Domain.MongoDomains;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acclaim.Api.Services
{
    public interface IProvideAcclaimService
    {
        //get
        Task<IEnumerable<MongoAcclaim>> GetAllAcclaims();
        Task<MongoAcclaim> GetAcclaimById(string acclaimId);
        Task<IEnumerable<MongoAcclaim>> GetAcclaimByName(string acclaimName);
        Task<List<User>> GetUsersInAcclaim(string acclaimId);
        Task<List<RelatedAcclaims>> GetRelatiedAcclaimsInAcclaim(string acclaimId);
        Task<User> GetUserById(string acclaimId, string userId);
        Task<string[]> GetProvidedAcclaim(string acclaimId);
        //create
        Task<bool> CreateAcclaim(CreateAcclaimViewModel model);

        //update
        Task<bool> AssignRelatiedAcclaimToAcclaim(string acclaimId, RelatedAcclaimsViewModel relatedAcclaimsViewModel);
        Task<bool> AssignIssuerToAcclaim(string acclaimId, IssuerViewModel issuerViewModel);
        Task<bool> AssignAProviderToAcclaim(string acclaimId, AProviderViewModel aProviderViewModel);
        Task<bool> AssignAProviderTypeToAcclaim(string acclaimId, AcclaimProvider acclaimProvider);

        Task<bool> AssignAcclaimTypeToAcclaim(string acclaimId, AcclaimType acclaimType);

        Task<bool> AddRuleToAcclaim(string acclaimId, AcclaimRuleViewModel rule);
        Task<bool> AddUserToAcclaim(string acclaimId, AcclaimUserViewModel user);
        Task<bool> DesignLogo(string acclaimId, string logoPath);
       
        Task<bool> UpdateAcclaim(string acclaimId, MongoAcclaim updatedAcclaim);

        //remove
        Task<bool> RemoveAcclaim(string acclaimId);
        Task<bool> RemoveRuleFromAcclaim(string acclaimId, string ruleId);




    }
}
