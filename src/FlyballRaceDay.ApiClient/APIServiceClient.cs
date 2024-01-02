// <auto-generated>
//     This code was generated by Refitter.
// </auto-generated>


using Refit;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlyballRace.APIClient
{
    [System.CodeDom.Compiler.GeneratedCode("Refitter", "0.8.5.0")]
    public partial interface IFlyballRaceDayApiService
    {
        [Headers("Accept: text/plain")]
        [Get("/test")]
        Task<IApiResponse<string>> Test();

        [Post("/race")]
        Task RacePOST([Body] RaceCreate body);

        [Get("/race/schedule/{tournamentId}")]
        Task Schedule(string tournamentId);

        [Get("/race/upcoming/{tournamentId}")]
        Task Upcoming(string tournamentId);

        [Put("/race/{raceId}/done")]
        Task Done(string raceId);

        [Delete("/race/{raceId}")]
        Task RaceDELETE(string raceId);

        [Put("/race/{raceId}/{ringId}")]
        Task RacePUT(string raceId, string ringId);

        [Headers("Accept: application/json")]
        [Post("/ring")]
        Task<IApiResponse<RingView>> RingCreate([Body] RingCreate body);

        [Delete("/ring")]
        Task RingDELETE([Query] string id);

        [Get("/ring/{tournamentId}/GetRings")]
        Task GetRings(string tournamentId);

        [Put("/ring/{id}")]
        Task RingPUT(string id, [Body] RingCreate body);

        [Headers("Accept: application/json")]
        [Post("/tournament")]
        Task<IApiResponse<TournamentView>> TournamentCreate([Body] TournamentCreate body);

        [Headers("Accept: application/json")]
        [Get("/tournament")]
        Task<IApiResponse<ICollection<TournamentView>>> TournamentGetActive();

        [Headers("Accept: application/json")]
        [Get("/tournament/{id}")]
        Task<IApiResponse<TournamentView>> TournamentGetById(string id);

        [Headers("Accept: application/json")]
        [Put("/tournament/{id}")]
        Task<IApiResponse<TournamentView>> TournamentUpdate(string id, [Body] TournamentCreate body);

        [Delete("/tournament/{id}")]
        Task TournamentDelete(string id);


    }
}


//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v10.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

#pragma warning disable 108 // Disable "CS0108 '{derivedDto}.ToJson()' hides inherited member '{dtoBase}.ToJson()'. Use the new keyword if hiding was intended."
#pragma warning disable 114 // Disable "CS0114 '{derivedDto}.RaisePropertyChanged(String)' hides inherited member 'dtoBase.RaisePropertyChanged(String)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword."
#pragma warning disable 472 // Disable "CS0472 The result of the expression is always 'false' since a value of type 'Int32' is never equal to 'null' of type 'Int32?'
#pragma warning disable 612 // Disable "CS0612 '...' is obsolete"
#pragma warning disable 1573 // Disable "CS1573 Parameter '...' has no matching param tag in the XML comment for ...
#pragma warning disable 1591 // Disable "CS1591 Missing XML comment for publicly visible type or member ..."
#pragma warning disable 8073 // Disable "CS8073 The result of the expression is always 'false' since a value of type 'T' is never equal to 'null' of type 'T?'"
#pragma warning disable 3016 // Disable "CS3016 Arrays as attribute arguments is not CLS-compliant"
#pragma warning disable 8603 // Disable "CS8603 Possible null reference return"
#pragma warning disable 8604 // Disable "CS8604 Possible null reference argument for parameter"

namespace FlyballRace.APIClient
{
    using System = global::System;

    

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v10.0.0.0))")]
    public partial class RaceCreate
    {

        [JsonPropertyName("tournamentId")]

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]   
        public string TournamentId { get; set; }

        [JsonPropertyName("raceNumber")]

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]   
        public int RaceNumber { get; set; }

        [JsonPropertyName("leftLaneTeam")]

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]   
        public string LeftLaneTeam { get; set; }

        [JsonPropertyName("rightLaneTeam")]

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]   
        public string RightLaneTeam { get; set; }

        [JsonPropertyName("format")]

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]   
        public string Format { get; set; }

        [JsonPropertyName("division")]

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]   
        public string Division { get; set; }

        [JsonPropertyName("breakout")]

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]   
        public string Breakout { get; set; }

        [JsonPropertyName("ringId")]

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]   
        public string RingId { get; set; }

        [JsonPropertyName("done")]

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]   
        public bool Done { get; set; }

        [JsonPropertyName("isBreak")]

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]   
        public bool IsBreak { get; set; }

        [JsonPropertyName("breakTimeInMinutes")]

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]   
        public int BreakTimeInMinutes { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v10.0.0.0))")]
    public partial class RingCreate
    {

        [JsonPropertyName("tournamentId")]

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]   
        public string TournamentId { get; set; }

        [JsonPropertyName("name")]

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]   
        public string Name { get; set; }

        [JsonPropertyName("color")]

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]   
        public string Color { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v10.0.0.0))")]
    public partial class RingView
    {

        [JsonPropertyName("tournamentId")]

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]   
        public string TournamentId { get; set; }

        [JsonPropertyName("name")]

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]   
        public string Name { get; set; }

        [JsonPropertyName("color")]

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]   
        public string Color { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v10.0.0.0))")]
    public partial class TournamentCreate
    {

        [JsonPropertyName("eventName")]

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]   
        public string EventName { get; set; }

        [JsonPropertyName("startDate")]

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]   
        public System.DateTimeOffset StartDate { get; set; }

        [JsonPropertyName("endDate")]

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]   
        public System.DateTimeOffset EndDate { get; set; }

        [JsonPropertyName("numberOfRings")]

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]   
        public int NumberOfRings { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v10.0.0.0))")]
    public partial class TournamentView
    {

        [JsonPropertyName("id")]

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]   
        public string Id { get; set; }

        [JsonPropertyName("eventName")]

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]   
        public string EventName { get; set; }

        [JsonPropertyName("startDate")]

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]   
        public System.DateTimeOffset StartDate { get; set; }

        [JsonPropertyName("endDate")]

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]   
        public System.DateTimeOffset EndDate { get; set; }

        [JsonPropertyName("numberOfRings")]

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]   
        public int NumberOfRings { get; set; }

    }


}

#pragma warning restore  108
#pragma warning restore  114
#pragma warning restore  472
#pragma warning restore  612
#pragma warning restore 1573
#pragma warning restore 1591
#pragma warning restore 8073
#pragma warning restore 3016
#pragma warning restore 8603
#pragma warning restore 8604