namespace INDWalks.API.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }


        //Navigation Properties - When we want to add the other columns of a table to a single 
        //we need to show its navigation by creating both class reference 

        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }

    }
}
