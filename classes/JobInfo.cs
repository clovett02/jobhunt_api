using JobHunt_API.Record;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;

namespace JobHunt_API
{
    public class JobInfo
    {

        
        ///Connection String
        string cs = @"
            server=thor.mysql;
            userid=root;
            password=mchs2009;
            database=jobhunt
        ";
        MySqlConnection con;
        /// <summary>
        /// Returns data from jobhunt database.
        /// Opens and closes database connection with constructor and
        /// destructor.
        /// </summary>
        public JobInfo()
        {
            this.con = new MySqlConnection(cs);
            this.con.Open();
        }

        private static string SafeGetString(MySqlDataReader rdr, string columnname, int columnindex)
        {
            if(!rdr.IsDBNull(columnindex))
            {
                return rdr.GetString(columnname);
            }
                
            return string.Empty;
        }
        /// <summary>
        /// Safely returns boolean value if the value in the DB is not null.
        /// If the DB value is null, this will return false.
        /// </summary>
        /// <param name="rdr"></param>
        /// <param name="columnname"></param>
        /// <param name="columnindex"></param>
        /// <returns></returns>
        private static bool SafeGetBoolean(MySqlDataReader rdr, string columnname, int columnindex)
        {
            if(!rdr.IsDBNull(columnindex))
            {
                return rdr.GetBoolean(columnname);
            }
            return false;
        }
        private static DateOnly SafeGetDate(MySqlDataReader rdr, string columnname, int columnindex)
        {
            if(!rdr.IsDBNull(columnindex))
            {
                return DateOnly.FromDateTime(rdr.GetDateTime(columnname)) ;
            }
            return DateOnly.MinValue;
        }
        private static TimeOnly SafeGetTime(MySqlDataReader rdr, string columnname, int columnindex)
        {
            if(!rdr.IsDBNull(columnindex))
            {
                return TimeOnly.FromDateTime(rdr.GetDateTime(columnname));
            }
            return TimeOnly.MinValue;
        }
        /// <summary>
        /// Returns a GetJobInfo record with date loaded from the 
        /// 'MySqlDataReader' object.
        /// </summary>
        /// <param name="rdr">MySqlDataReader object to load data from</param>
        /// <returns></returns>
        private static GetJobInfo LoadInfo(MySqlDataReader rdr)
        {
            return new GetJobInfo(
                    JobID: SafeGetString(rdr, "ID", 0),
                    CompanyName: SafeGetString(rdr, "CompanyName", 1),
                    CompanyURL: SafeGetString(rdr, "CompanyURL", 2),
                    JobTitle: SafeGetString(rdr, "JobTitle", 3),
                    JobDescription: SafeGetString(rdr, "JobDescription", 4),
                    State: SafeGetString(rdr, "State", 5),
                    City: SafeGetString(rdr, "City", 6),
                    Remote:SafeGetBoolean(rdr, "Remote", 7),
                    Hybrid: SafeGetBoolean(rdr, "Hybrid", 8),
                    Onsite: SafeGetBoolean(rdr, "Onsite", 9),
                    DatePosted: SafeGetDate(rdr, "DatePosted", 10),
                    ApplicationDate: SafeGetDate(rdr, "ApplicationDate", 11),
                    ApplicationTime: SafeGetTime(rdr, "ApplicationTime", 12),
                    ApplicationDay: SafeGetString(rdr, "ApplicationDay", 13),
                    Responded: SafeGetBoolean(rdr, "Responded", 14),
                    ResponseDate: SafeGetDate(rdr, "ResponseDate", 15),
                    ResponseTime: SafeGetTime(rdr, "ResponseTime", 16),
                    ResponseDay: SafeGetString(rdr, "ResponseDay", 17),
                    Denied: SafeGetBoolean(rdr,"Denied", 18),
                    EasyApply: SafeGetBoolean(rdr, "EasyApply", 19),
                    SiteFoundOn: SafeGetString(rdr, "SiteFoundOn", 20)
                    );
        }
        private GetJobInfo GetJob(string sql)
        {
            using var cmd = new MySqlCommand(sql, this.con);
            using MySqlDataReader rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
                return LoadInfo(rdr);
            }
            return null;
        }
        private GetJobInfo[] GetJobs(string sql)
        {
            using var cmd = new MySqlCommand(sql, this.con);
            using MySqlDataReader rdr = cmd.ExecuteReader();
            List<GetJobInfo> records = new List<GetJobInfo>();
            while(rdr.Read())
            {
                records.Add(LoadInfo(rdr));
            }
            GetJobInfo[] result = records.ToArray();
            return result;
        }
        public GetJobInfo ReturnJob(string ID)
        {
            string sql = $"SELECT * FROM jobs WHERE ID = {ID};";
            return this.GetJob(sql);
        }
        ///<summary>
        ///Returns Jobs applied to within last year
        ///</summary>
        public GetJobInfo[] ReturnJobs()
        {
            

            string sql = @"SELECT * FROM jobs 
                WHERE ApplicationDate > DATE_SUB(NOW(), INTERVAL 1 YEAR)
                ORDER BY ApplicationDate DESC;";
            return this.GetJobs(sql);
        }
        /// <summary>
        /// Returns Jobs after 'begindate' and before, and including the 'enddate'.
        /// </summary>
        /// <param name="begindate">jobs after this date will be included</param>
        /// <param name="enddate">jobs before, and including this date will be included </param>
        /// <returns></returns>
        public GetJobInfo[] ReturnJobs(string begindate, string enddate)
        {   
            
            return null;
        }

        public void InsertJob(PostJobInfo Job)
        {
            string sql = @"INSERT INTO jobs(CompanyName, JobTitle, State, City, Remote, Hybrid, Onsite, 
            ApplicationDate, ApplicationTime) 
            
            VALUES(@CompanyName, @JobTitle, @State, @City, @Remote, @Hybrid, @Onsite, 
            @ApplicationDate, @ApplicationTime);";

            using MySqlCommand cmd = new MySqlCommand(sql, this.con);

            cmd.Parameters.AddWithValue("@CompanyName", Job.CompanyName);
            cmd.Parameters.AddWithValue("@JobTitle", Job.JobTitle);
            cmd.Parameters.AddWithValue("@JobDescription", Job.JobDescription);
            cmd.Parameters.AddWithValue("@URL", Job.JobDescription);
            cmd.Parameters.AddWithValue("@State", Job.State);
            cmd.Parameters.AddWithValue("@City", Job.City);
            cmd.Parameters.AddWithValue("@Remote", Job.Remote);
            cmd.Parameters.AddWithValue("@Hybrid", Job.Hybrid);
            cmd.Parameters.AddWithValue("@Onsite", Job.Onsite);
            cmd.Parameters.AddWithValue("@ApplicationDate", Job.ApplicationDate);
            cmd.Parameters.AddWithValue("@ApplicationTime", Job.ApplicationTime);

            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        ~JobInfo()
        {
            this.con.Close();
        }

    }
}

