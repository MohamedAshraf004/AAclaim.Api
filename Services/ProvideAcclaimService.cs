using Acclaim.Api.Contracts.V1.Requests;
using Acclaim.Api.Domain;
using Acclaim.Api.Domain.MongoDomains;
using Acclaim.Api.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Acclaim.Api.Services
{
    public class ProvideAcclaimService : IProvideAcclaimService
    {



        private readonly IMongoCollection<MongoAcclaim> _Acclaim;
        public ProvideAcclaimService(IAcclaimStoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _Acclaim = database.GetCollection<MongoAcclaim>(settings.AcclaimsCollectionName);
        }
        #region Get

        public async Task<IEnumerable<MongoAcclaim>> GetAllAcclaims()
        {
            var result = await _Acclaim.FindAsync(acclaim => true);
            var acclaims = result.ToEnumerable();
            return acclaims;
        }

        public async Task<MongoAcclaim> GetAcclaimById(string Id)
        {
            var result = await _Acclaim.FindAsync(acclaim => acclaim.Id == Id);
            var getAcclaim = await result.SingleOrDefaultAsync();
            return getAcclaim;
        }

        public async Task<IEnumerable<MongoAcclaim>> GetAcclaimByName(string acclaimName)
        {
            var result = await _Acclaim.FindAsync(acclaim => acclaim.Name.Contains(acclaimName));
            var getAcclaims = result.ToEnumerable();
            return getAcclaims;
        }

        public async Task<List<User>> GetUsersInAcclaim(string acclaimId)
        {
            var result = await _Acclaim.FindAsync(aclaim => aclaim.Id == acclaimId);
            var acclaim = await result.SingleOrDefaultAsync();
            var users = acclaim.Users;
            return users;
        }
        
        public async Task<List<RelatedAcclaims>> GetRelatiedAcclaimsInAcclaim(string acclaimId)
        {
            var result = await _Acclaim.FindAsync(aclaim => aclaim.Id == acclaimId);
            var acclaim = await result.SingleOrDefaultAsync();
            var relatiedAcclaims = acclaim.RelatedAcclaims;
            return relatiedAcclaims;
        }

        public async Task<User> GetUserById(string acclaimId, string userId)
        {
            var result = await _Acclaim.FindAsync(ac => ac.Id == acclaimId);
            var acclaim = await result.SingleOrDefaultAsync();
            var user = acclaim.Users.FirstOrDefault(u => u.Id == userId);
            return user;
        }
        public async Task<string[]> GetProvidedAcclaim(string acclaimId)
        {
            var result = await _Acclaim.FindAsync(ac => ac.Id == acclaimId);
            var acclaim = await result.SingleOrDefaultAsync();
            var acclaimProvider = Enum.GetNames(typeof(AcclaimProvider));
            return acclaimProvider;
        }


        #endregion
        #region Post
        public async Task<bool> CreateAcclaim(CreateAcclaimViewModel model)
        {
            var acclaim = new MongoAcclaim
            {
                Name = model.Name
            };
            await _Acclaim.InsertOneAsync(acclaim);
            return true;
        }
        #endregion

        #region Put

        public async Task<bool> DesignLogo(string acclaimId, string logoPath)
        {
            var result = await _Acclaim.FindAsync(aclaim => aclaim.Id == acclaimId);
            var acclaim = await result.SingleOrDefaultAsync();
            acclaim.AcclaimLogo = logoPath;
            var resultReplace = await _Acclaim.ReplaceOneAsync(aclaim => aclaim.Id == acclaimId, acclaim);
            return resultReplace.IsAcknowledged;
        }

        public async Task<bool> AssignRelatiedAcclaimToAcclaim(string acclaimId
                                            , RelatedAcclaimsViewModel relatedAcclaimsViewModel)
        {
            var Acclaim = await GetAcclaimById(acclaimId);
            var relatiedAcclaim = new RelatedAcclaims()
            {
                Id = Guid.NewGuid().ToString(),
                Name = relatedAcclaimsViewModel.Name
            };
            Acclaim.RelatedAcclaims.Add(relatiedAcclaim);
            var result = await _Acclaim.ReplaceOneAsync(acclaim => acclaim.Id == acclaimId, Acclaim);
            return result.IsAcknowledged;
        }

        public async Task<bool> AssignIssuerToAcclaim(string acclaimId, IssuerViewModel issuerViewModel)
        {
            var Acclaim = await GetAcclaimById(acclaimId);
            var issuer = new Issuer()
            {
                Id = Guid.NewGuid().ToString(),
                Name = issuerViewModel.Name
            };
            Acclaim.Issuer = issuer;
            var result = await _Acclaim.ReplaceOneAsync(acclaim => acclaim.Id == acclaimId, Acclaim);
            return result.IsAcknowledged;
        }

        public async Task<bool> AssignAProviderToAcclaim(string acclaimId, AProviderViewModel aProviderViewModel)
        {
            var Acclaim = await GetAcclaimById(acclaimId);
            var aProvider = new AProvider()
            {
                Id = Guid.NewGuid().ToString(),
                Name = aProviderViewModel.Name
            };
            Acclaim.AProvider = aProvider;
            var result = await _Acclaim.ReplaceOneAsync(acclaim => acclaim.Id == acclaimId, Acclaim);
            return result.IsAcknowledged;
        }
        public async Task<bool> AssignAProviderTypeToAcclaim(string acclaimId, AcclaimProvider acclaimProvider)
        {
            var Acclaim = await GetAcclaimById(acclaimId);

            Acclaim.AcclaimProvider = acclaimProvider;
            var result = await _Acclaim.ReplaceOneAsync(acclaim => acclaim.Id == acclaimId, Acclaim);
            return result.IsAcknowledged;
        }

        public async Task<bool> AssignAcclaimTypeToAcclaim(string acclaimId, AcclaimType acclaimType)
        {
            var Acclaim = await GetAcclaimById(acclaimId);

            Acclaim.AcclaimType = acclaimType;
            var result = await _Acclaim.ReplaceOneAsync(acclaim => acclaim.Id == acclaimId, Acclaim);
            return result.IsAcknowledged;
        }

        public async Task<bool> AddRuleToAcclaim(string acclaimId, AcclaimRuleViewModel ruleViewModel)
        {
            var Acclaim = await GetAcclaimById(acclaimId);
            var rule = new AcclaimRule()
            {
                Id = Guid.NewGuid().ToString(),
                Rule = ruleViewModel.Rule
            };
            Acclaim.AcclaimRules.Add(rule);
            var result = await _Acclaim.ReplaceOneAsync(acclaim => acclaim.Id == acclaimId, Acclaim);
            return result.IsAcknowledged;
        }

        public async Task<bool> AddUserToAcclaim(string acclaimId, AcclaimUserViewModel userViewModel)
        {
            var Acclaim = await GetAcclaimById(acclaimId);
            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Name = userViewModel.UserName,
                DateOfAcclaim = userViewModel.DateOfAcclaim
            };
            Acclaim.Users.Add(user);
            var result = await _Acclaim.ReplaceOneAsync(acclaim => acclaim.Id == acclaimId, Acclaim);
            return result.IsAcknowledged;
        }

        public async Task<bool> UpdateAcclaim(string id, MongoAcclaim updatedAcclaim)
        {
            var oldAcclaim = await GetAcclaimById(id);
            oldAcclaim.Name = updatedAcclaim.Name;
            var result = await _Acclaim.ReplaceOneAsync(acclaim => acclaim.Id == id, oldAcclaim);
            return result.IsAcknowledged;
        }


        #endregion

        #region Delete
        public async Task<bool> RemoveAcclaim(string acclaimId)
        {
            var result = await _Acclaim.DeleteOneAsync(acclaim => acclaim.Id == acclaimId);
            return result.IsAcknowledged;
        }

        public async Task<bool> RemoveRuleFromAcclaim(string acclaimId, string ruleId)
        {
            var result = await _Acclaim.FindAsync(aclaim => aclaim.Id == acclaimId);
            var acclaim = await result.SingleOrDefaultAsync();
            var deletedRule = acclaim.AcclaimRules.SingleOrDefault(r => r.Id == ruleId);
            acclaim.AcclaimRules.Remove(deletedRule);
            var resultReplace = await _Acclaim.ReplaceOneAsync(aclaim => aclaim.Id == acclaimId, acclaim);
            return resultReplace.IsAcknowledged;
        }


        #endregion

    }
}

