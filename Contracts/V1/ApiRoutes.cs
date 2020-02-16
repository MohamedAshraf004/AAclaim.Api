namespace Acclaim.Api.V1.Contracts
{

    public class ApiRoutes
    {
        public const string Root = "api/";

        public const string Version = "v1";

        public const string Base = Root + Version + "/Acclaim/";

        public static class Acclaim
        {
            //acclaim services routes
            public const string GetAllAcclaims = Base + "GetAllAcclaims";

            public const string GetAcclaimById = Base + "GetAcclaimById/{Id}";

            public const string GetProvidedAcclaim = Base + "GetProvidedAcclaim/{acclaimId}";

            public const string GetUsersInAcclaim = Base + "GetUsersInAcclaim/{acclaimId}";

            public const string GetRelatiedAcclaimsInAcclaim = Base + "GetRelatiedAcclaimsInAcclaim/{acclaimId}";

            public const string GetUserById = Base + "GetUserById/{acclaimId}/{userId}";

            public const string Search = Base + "Search/{acclaimName}";

            public const string CreateAcclaim = Base + "CreateAcclaim";

            public const string RemoveAcclaimById = Base + "RemoveAcclaimById/{acclaimId}";

            public const string RemoveRuleFromAcclaim = Base + "RemoveRuleFromAcclaim/{acclaimId}";

            public const string UpdateAcclaim = Base + "UpdateAcclaim/{acclaimId}";

            public const string AssignIssuerToAcclaim = Base + "AssignIssuerToAcclaim/{acclaimId}";

            public const string AssignAProviderToAcclaim = Base + "AssignAProviderToAcclaim/{acclaimId}";

            public const string AssignRelatiedAcclaimToAcclaim = Base + "AssignRelatiedAcclaimToAcclaim/{acclaimId}";

            public const string AssignAcclaimTypeToAcclaim = Base + "AssignAcclaimTypeToAcclaim/{acclaimId}";

            public const string AssignAProviderTypeToAcclaim = Base + "AssignAProviderTypeToAcclaim/{acclaimId}";

            public const string AddUserToAcclaim = Base + "AddUserToAcclaim/{acclaimId}";

            public const string AddRuleToAcclaim = Base + "AddRuleToAcclaim/{acclaimId}";

            public const string DesignLogo = Base + "DesignLogo/{acclaimId}";
        }

    }
}
