namespace RVTR.Booking.DataContext
{
    /// <summary>
    /// A POCO used to specify the possible queries used for filtering/sorting
    /// records pulled from the db
    /// </summary>
    public class SearchFilterQueries
    {
        /// <summary>
        /// The expression used to filter data fetched based on a specified constraint.
        /// #### Usage
        /// `/{resource}?filter=id == 1`
        /// </summary>
        /// <value></value>
        public virtual string Filter { get; set; }

        /// <summary>
        /// Defines what property of the resource should be used to sort
        /// </summary>
        /// <value></value>
        public virtual string Sort { get; set; }
        
        /// <summary>
        /// Defines the amount of records to skip
        /// </summary>
        /// <value></value>
        public virtual string Offset { get; set; }

        /// <summary>
        /// Specifies the amount of records that should be returned
        /// </summary>
        /// <value></value>
        public virtual string Limit { get; set; }
    }
}
