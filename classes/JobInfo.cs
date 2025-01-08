using JobHunt_API.Record;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;

namespace JobHunt_API
{
    public class JobInfo
    {
        /// <summary>
        /// Returns data from jobhunt database
        /// </summary>
        /// <param name="rdr">Reader for open</param>
        
        ///Connection String
        string cs = @"
            server=thor.mysql;
            userid=root;
            password=mchs2009;
            database=jobhunt
        ";
        MySqlConnection con;
        public JobInfo()
        {
            this.con = new MySqlConnection(cs);
            this.con.Open();
        }

        public static string SafeGetString(MySqlDataReader rdr, string columnname, int columnindex)
        {
            if(!rdr.IsDBNull(columnindex))
            {
                return rdr.GetString(columnname);
            }
                
            return string.Empty;
        }
        public static bool SafeGetBoolean(MySqlDataReader rdr, string columnname, int columnindex)
        {
            if(!rdr.IsDBNull(columnindex))
            {
                return rdr.GetBoolean(columnname);
            }
            return false;
        }
        public static DateTime SafeGetDate(MySqlDataReader rdr, string columnname, int columnindex)
        {
            if(!rdr.IsDBNull(columnindex))
            {
                return rdr.GetDateTime(columnname);
            }
            return DateTime.MinValue;
        }
    
        private GetJobInfo GetJob(string sql)
        {
            using var cmd = new MySqlCommand(sql, this.con);
            using MySqlDataReader rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
                return new GetJobInfo(
                    JobID: rdr.GetString("ID"),
                    CompanyName: rdr.GetString("CompanyName"),
                    JobTitle: rdr.GetString("JobTitle"),
                    JobDescription: SafeGetString(rdr, "JobDescription", 4),
                    State: rdr.GetString("State"),
                    City:rdr.GetString("City"),
                    Remote:rdr.GetBoolean("Remote"),
                    Hybrid:rdr.GetBoolean("Hybrid"),
                    Onsite:rdr.GetBoolean("Onsite"),
                    ApplicationDate: rdr.GetDateTime("ApplicationDate"),
                    ApplicationTime: rdr.GetDateTime("ApplicationTime"),
                    Responded: SafeGetBoolean(rdr, "Responded", 12),
                    ResponseDate: SafeGetDate(rdr, "ResponseDate", 13),
                    ResponseTime: SafeGetDate(rdr, "ResponseTime", 15),
                    Denied: SafeGetBoolean(rdr,"Denied", 16)
                    );
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
                records.Add(new GetJobInfo(
                    JobID: rdr.GetString("ID"),
                    CompanyName: rdr.GetString("CompanyName"),
                    JobTitle: rdr.GetString("JobTitle"),
                    JobDescription: SafeGetString(rdr, "JobDescription", 4),
                    State: rdr.GetString("State"),
                    City:rdr.GetString("City"),
                    Remote:rdr.GetBoolean("Remote"),
                    Hybrid:rdr.GetBoolean("Hybrid"),
                    Onsite:rdr.GetBoolean("Onsite"),
                    ApplicationDate: rdr.GetDateTime("ApplicationDate"),
                    ApplicationTime: rdr.GetDateTime("ApplicationTime"),
                    Responded: SafeGetBoolean(rdr, "Responded", 12),
                    ResponseDate: SafeGetDate(rdr, "ResponseDate", 13),
                    ResponseTime: SafeGetDate(rdr, "ResponseTime", 15),
                    Denied: SafeGetBoolean(rdr,"Denied", 16)
                    ));
            }
            GetJobInfo[] result = records.ToArray();
            return result;
        }
        public GetJobInfo ReturnJob(string ID)
        {
            string sql = $"SELECT * FROM jobs WHERE ID = {ID};";
            return this.GetJob(sql);
        }
        public GetJobInfo[] ReturnJobs()
        {
            ///<summary>
            ///Returns Jobs applied to within last year
            ///</summary>

            string sql = $"SELECT * FROM jobs WHERE ApplicationDate > DATE_SUB(NOW(), INTERVAL 1 YEAR);";
            return this.GetJobs(sql);
        }
        public GetJobInfo[] ReturnJobs(string begindate, string enddate)
        {
            ///<summary>
            ///Returns Jobs between begin and end dates
            ///</summary>
            /// <param name="begindate">jobs after this date will be included</param>
            /// <param name="enddate">jobs before, and including this date will be included </param>

            return null;
        }

        public void InsertJob(PostJobInfo Job)
        {
            string sql = @"INSERT INTO jobs(CompanyName, JobTitle, URL, State, City, Remote, Hybrid, Onsite, 
            ApplicationDate, ApplicationTime) 
            
            VALUES(@CompanyName, @JobTitle, @URL, @State, @City, @Remote, @Hybrid, @Onsite, 
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